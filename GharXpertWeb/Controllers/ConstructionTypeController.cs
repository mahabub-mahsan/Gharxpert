using AutoMapper;
using Gharxpert_Utility;
using GharXpertWeb.Models;
using GharXpertWeb.Models.Dto;
using GharXpertWeb.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GharXpertWeb.Controllers
{

    public class ConstructionTypeController : Controller
    {
        private readonly IConstructionTypeService _constructionTypeService;
        private readonly IMapper _mapper;
        public ConstructionTypeController(IConstructionTypeService constructionTypeService, IMapper mapper)
        {
            _constructionTypeService = constructionTypeService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexConstructionType()
        {
            List<ConstructionTypeDTO> list = new();
            var response = await _constructionTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ConstructionTypeDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }


        public async Task<IActionResult> CreateConstructionType()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConstructionType(ConstructionTypeCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _constructionTypeService.CreateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "ConstructionType created successfully";
                    return RedirectToAction(nameof(IndexConstructionType));
                }
            }
            TempData["error"] = "error encountered.";
            return View(model);
        }

        public async Task<IActionResult> UpdateConstructionType(int constructionTypeId)
        {
            var response = await _constructionTypeService.GetAsync<APIResponse>(constructionTypeId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                ConstructionTypeDTO model = JsonConvert.DeserializeObject<ConstructionTypeDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<ConstructionTypeUpdateDTO>(model));
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateConstructionType(ConstructionTypeUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = "ConstructionType updated successfully";
                var response = await _constructionTypeService.UpdateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexConstructionType));
                }
            }
            TempData["error"] = "error encountered.";
            return View(model);
        }

        public async Task<IActionResult> DeleteConstructionType(int constructionTypeId)
        {
            var response = await _constructionTypeService.GetAsync<APIResponse>(constructionTypeId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                ConstructionTypeDTO model = JsonConvert.DeserializeObject<ConstructionTypeDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConstructionType(ConstructionTypeDTO model)
        {

            var response = await _constructionTypeService.DeleteAsync<APIResponse>(model.Cno, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "ConstructionType deleted successfully";
                return RedirectToAction(nameof(IndexConstructionType));
            }
            TempData["error"] = "error encountered.";
            return View(model);
        }
    }
}
