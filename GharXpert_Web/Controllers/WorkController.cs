using AutoMapper;
using Gharxpert_Utility;
using GharXpert_Web.Models;
using GharXpert_Web.Models.Dto;
using GharXpert_Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;

namespace GharXpert_Web.Controllers
{
    public class WorkController : Controller
    {
        private readonly IWorkService _workService;
        private readonly IMapper _mapper;
        public WorkController(IWorkService workService, IMapper mapper)
        {
            _workService = workService;
            _mapper = mapper;
        }
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
        public async Task<IActionResult> CreateWork(WorkCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _workService.CreateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "Work created successfully";
                    return RedirectToAction(nameof(IndexWork));
                }
            }
            TempData["error"] = "error encountered.";
            return View(model);
        }

        //[Authorize(Roles = "admin")]
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

        //[Authorize(Roles = "admin")]
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
            }
            TempData["error"] = "error encountered.";
            return View(model);
        }

        //[Authorize(Roles = "admin")]
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

        //[Authorize(Roles = "admin")]
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
