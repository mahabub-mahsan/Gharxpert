using AutoMapper;
using Gharxpert_Utility;
using GharXpertWeb.Models;
using GharXpertWeb.Models.Dto;
using GharXpertWeb.Models.VM;
using GharXpertWeb.Service;
using GharXpertWeb.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace GharXpertWeb.Controllers
{
    public class WorkTypeController : Controller
    {

        private readonly IWorkTypeService _workTypeService;
        private readonly IWorkService _workService;
        private readonly IMapper _mapper;

        public WorkTypeController(IWorkTypeService workTypeService, IMapper mapper, IWorkService workService)
        {
            _workTypeService = workTypeService;
            _mapper = mapper;
            _workService = workService;
        }

        public async Task<IActionResult> IndexWorkType()
        {
            List<WorkTypeDTO> list = new();
            var response = await _workTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<WorkTypeDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        public async Task<IActionResult> CreateWorkType()
        {
            WorkTypeCreateVM workTypeVM = new();
            var response = await _workService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                workTypeVM.WorkList = JsonConvert.DeserializeObject<List<WorkDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.WorkName,
                        Value = i.Id.ToString()
                    }); ;
            }
            return View(workTypeVM);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWorkType(WorkTypeCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _workTypeService.CreateAsync<APIResponse>(model.WorkType, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexWorkType));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _workService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.WorkList = JsonConvert.DeserializeObject<List<WorkDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.WorkName,
                        Value = i.Id.ToString()
                    }); ;
            }

            return View(model);
        }

        public async Task<IActionResult> UpdateWorkType(int workTypeId)
        {
            WorkTypeUpdateVM workTypeVM = new();
            var response = await _workTypeService.GetAsync<APIResponse>(workTypeId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                WorkTypeDTO model = JsonConvert.DeserializeObject<WorkTypeDTO>(Convert.ToString(response.Result));
                workTypeVM.WorkType = _mapper.Map<WorkTypeUpdateDTO>(model);
            }

            response = await _workService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                workTypeVM.WorkList = JsonConvert.DeserializeObject<List<WorkDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.WorkName,
                        Value = i.Id.ToString()
                    });
                return View(workTypeVM);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateWorkType (WorkTypeUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _workTypeService.UpdateAsync<APIResponse>(model.WorkType, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexWorkType));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var resp = await _workService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.WorkList = JsonConvert.DeserializeObject<List<WorkDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.WorkName,
                        Value = i.Id.ToString()
                    });
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteWorkType(int workTypeId)
        {
            WorkTypeDeleteVM workTypeVM = new();
            var response = await _workTypeService.GetAsync<APIResponse>(workTypeId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                WorkTypeDTO model = JsonConvert.DeserializeObject<WorkTypeDTO>(Convert.ToString(response.Result));
                workTypeVM.WorkType = model;
            }

            response = await _workService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                workTypeVM.WorkList = JsonConvert.DeserializeObject<List<WorkDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.WorkName,
                        Value = i.Id.ToString()
                    });
                return View(workTypeVM);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWorkType(WorkTypeDeleteVM model)
        {

            var response = await _workTypeService.DeleteAsync<APIResponse>(model.WorkType.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexWorkType));
            }

            return View(model);
        }

    }
}
