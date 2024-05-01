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

    public class ContactOurExpertController : Controller
    {
        private readonly IContactOurExpertService _contactOurExpertService;
        private readonly IMapper _mapper;
        public ContactOurExpertController(IContactOurExpertService contactOurExpertService, IMapper mapper)
        {
            _contactOurExpertService = contactOurExpertService;
            _mapper = mapper;
        }
        public async Task<IActionResult> IndexContactOurExpert()
        {
            List<ContactOurExpertDTO> list = new();
            var response = await _contactOurExpertService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ContactOurExpertDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }


        public async Task<IActionResult> CreateContactOurExpert()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateContactOurExpert(ContactOurExpertCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _contactOurExpertService.CreateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["Success"] = "ContactOurExpert created successfully";
                    return RedirectToAction(nameof(IndexContactOurExpert));
                }
            }
            TempData["error"] = "error encountered.";
            return View(model);
        }

        public async Task<IActionResult> UpdateContactOurExpert(int contactOurExpertrId)
        {
            var response = await _contactOurExpertService.GetAsync<APIResponse>(contactOurExpertrId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                ContactOurExpertDTO model = JsonConvert.DeserializeObject<ContactOurExpertDTO>(Convert.ToString(response.Result));
                return View(_mapper.Map<ContactOurExpertUpdateDTO>(model));
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateContactOurExpert(ContactOurExpertUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                TempData["Success"] = "ContactOurExpert updated successfully";
                var response = await _contactOurExpertService.UpdateAsync<APIResponse>(model, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexContactOurExpert));
                }
            }
            TempData["error"] = "error encountered.";
            return View(model);
        }

        public async Task<IActionResult> DeleteContactOurExpert(int contactOurExpertId)
        {
            var response = await _contactOurExpertService.GetAsync<APIResponse>(contactOurExpertId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                ContactOurExpertDTO model = JsonConvert.DeserializeObject<ContactOurExpertDTO>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteContactOurExpert(ContactOurExpertDTO model)
        {

            var response = await _contactOurExpertService.DeleteAsync<APIResponse>(model.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                TempData["Success"] = "ContactOurExpert deleted successfully";
                return RedirectToAction(nameof(IndexContactOurExpert));
            }
            TempData["error"] = "error encountered.";
            return View(model);
        }
    }
}
