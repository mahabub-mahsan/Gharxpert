using AutoMapper;
using Gharxpert_Utility;
using GharXpertWeb.Models;
using GharXpertWeb.Models.Dto;
using GharXpertWeb.Models.VM;
using GharXpertWeb.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace GharXpertWeb.Controllers
{
    public class WorkController : Controller
    {
        private readonly IWorkService _workService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public WorkController(IWorkService workService, IMapper mapper, IAuthService authService) 
        {
            _workService = workService;
            _authService = authService;
            _mapper = mapper;
        }
        //public async Task<IActionResult> IndexWork()
        //{
        //    List<WorkDTO> list = new();
        //    var response = await _workService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
        //    if (response != null && response.IsSuccess)
        //    {
        //        list = JsonConvert.DeserializeObject<List<WorkDTO>>(Convert.ToString(response.Result));
        //    }

        //    return View(list);
        //}

        public async Task<IActionResult> IndexWork()
        {
            List<WorkDTO> list = new();
            var response = await _workService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<WorkDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateWork()
        {
            return View();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWork(WorkCreateVM model)
        {
            //if (ModelState.IsValid)
            //{
            //    var response = await _workService.CreateAsync<APIResponse>(model.Work, HttpContext.Session.GetString(SD.SessionToken));
            //    if (response != null && response.IsSuccess)
            //    {
            //        TempData["Success"] = "Work created successfully";
            //        return RedirectToAction(nameof(IndexWork));
            //    }
            //}
            //TempData["error"] = "error encountered.";

            if (ModelState.IsValid)
            {
                var response = await _workService.CreateAsync<APIResponse>(model.Work, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Work created successfully";
                    return RedirectToAction(nameof(IndexWork));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> UpdateWork(int workId)
        {
            var response = await _workService.GetAsync<APIResponse>(workId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                WorkDTO model = JsonConvert.DeserializeObject<WorkDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<WorkUpdateDTO>(model));
            }
            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateWork(WorkUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = "Work updated successfully";
                var response = await _workService.UpdateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexWork));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            TempData["error"] = "error encountered.";
            return View(model);
        }

        public async Task<IActionResult> DeleteWork(int workId)
        {
            var response = await _workService.GetAsync<APIResponse>(workId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                WorkDTO model = JsonConvert.DeserializeObject<WorkDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteWork(WorkDTO model)
        {

            var response = await _workService.DeleteAsync<APIResponse>(model.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "Work deleted successfully";
                return RedirectToAction(nameof(IndexWork));
            }
            TempData["error"] = "error encountered.";
            return View(model);
        }

    }
}
