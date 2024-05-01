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
    public class ServiceChargesController : Controller
    {
        private readonly IWorkService _workService;
        private readonly IWorkTypeService _workTypeService;
        private readonly IServiceChargesService _serviceChargesService;
        private readonly IConstructionTypeService _constructionTypeService;
        private readonly IMapper _mapper;

        public ServiceChargesController(IServiceChargesService serviceChargesService, IWorkTypeService workTypeService, IWorkService workService,
            IConstructionTypeService constructionTypeService, IMapper mapper)
        {
            _workService = workService;
            _workTypeService = workTypeService;
            _mapper = mapper;
            _serviceChargesService = serviceChargesService;
            _constructionTypeService = constructionTypeService;
        }

        //public async Task<IActionResult> IndexServiceCharges()
        //{
        //    List<ServiceChargesDTO> list = new();
        //    var response = await _serviceChargesService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
        //    if (response != null && response.IsSuccess)
        //    {
        //        list = JsonConvert.DeserializeObject<List<ServiceChargesDTO>>(Convert.ToString(response.Result));
        //    }

        //    return View(list);
        //}

        public async Task<IActionResult> IndexServiceCharges()
        {
            List<ServiceChargesDTO> list = new();
            var response = await _serviceChargesService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ServiceChargesDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        public async Task<IActionResult> CreateServiceCharges()
        {
            ServiceChargesCreateVM serviceChargesVM = new();
            var response = await _workTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                serviceChargesVM.WorkTypeList = JsonConvert.DeserializeObject<List<WorkTypeDTO>>
                    (Convert.ToString(response.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Work_Type,
                        Value = i.Id.ToString()
                    }); ;
            }

            var res = await _workService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (res != null && res.IsSuccess)
            {
                serviceChargesVM.WorkList = JsonConvert.DeserializeObject<List<WorkDTO>>
                    (Convert.ToString(res.Result)).Select(i => new SelectListItem
                    {
                        Text = i.WorkName,
                        Value = i.Id.ToString()
                    }); ;
            }

            var re = await _constructionTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (re != null && res.IsSuccess)
            {
                serviceChargesVM.ConstructionTypeList = JsonConvert.DeserializeObject<List<ConstructionTypeDTO>>
                    (Convert.ToString(re.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Ctype,
                        Value = i.Cno.ToString()
                    }); ;
            }
            return View(serviceChargesVM);
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateServiceCharges(ServiceChargesCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _serviceChargesService.CreateAsync<APIResponse>(model.ServiceCharges, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexServiceCharges));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _workTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.WorkTypeList = JsonConvert.DeserializeObject<List<WorkTypeDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Work_Type,
                        Value = i.Id.ToString()
                    }); ;
            }

            var res = await _workService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (res != null && res.IsSuccess)
            {
                model.WorkList = JsonConvert.DeserializeObject<List<WorkDTO>>
                    (Convert.ToString(res.Result)).Select(i => new SelectListItem
                    {
                        Text = i.WorkName,
                        Value = i.Id.ToString()
                    }); ;
            }

            var re = await _constructionTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (re != null && re.IsSuccess)
            {
                model.ConstructionTypeList = JsonConvert.DeserializeObject<List<ConstructionTypeDTO>>
                    (Convert.ToString(re.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Ctype,
                        Value = i.Cno.ToString()
                    }); ;
            }

            return View(model);
        }

        public async Task<IActionResult> UpdateServiceCharges(int serviceChargesId)
        {
            ServiceChargesUpdateVM serviceChargesVM = new();
            var response = await _serviceChargesService.GetAsync<APIResponse>(serviceChargesId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                ServiceChargesDTO model = JsonConvert.DeserializeObject<ServiceChargesDTO>(Convert.ToString(response.Result));
                serviceChargesVM.ServiceCharges = _mapper.Map<ServiceChargesUpdateDTO>(model);
            }
            if (response != null && response.IsSuccess)
            {


                response = await _workTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    serviceChargesVM.WorkTypeList = JsonConvert.DeserializeObject<List<WorkTypeDTO>>
                        (Convert.ToString(response.Result)).Select(i => new SelectListItem
                        {
                            Text = i.Work_Type,
                            Value = i.Id.ToString()
                        });
                }

                response = await _workService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    serviceChargesVM.WorkList = JsonConvert.DeserializeObject<List<WorkDTO>>
                        (Convert.ToString(response.Result)).Select(i => new SelectListItem
                        {
                            Text = i.WorkName,
                            Value = i.Id.ToString()
                        });
                }

                response = await _constructionTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    serviceChargesVM.ConstructionTypeList = JsonConvert.DeserializeObject<List<ConstructionTypeDTO>>
                        (Convert.ToString(response.Result)).Select(i => new SelectListItem
                        {
                            Text = i.Ctype,
                            Value = i.Cno.ToString()
                        });

                }
                return View(serviceChargesVM);
            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateServiceCharges(ServiceChargesUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _serviceChargesService.UpdateAsync<APIResponse>(model.ServiceCharges, HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexServiceCharges));
                }
                else
                {
                    if (response.ErrorMessages.Count > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }
            var resp = await _workTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (resp != null && resp.IsSuccess)
            {
                model.WorkTypeList = JsonConvert.DeserializeObject<List<WorkTypeDTO>>
                    (Convert.ToString(resp.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Work_Type,
                        Value = i.Id.ToString()
                    });
            }

            var res = await _workService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (res != null && res.IsSuccess)
            {
                model.WorkList = JsonConvert.DeserializeObject<List<WorkDTO>>
                    (Convert.ToString(res.Result)).Select(i => new SelectListItem
                    {
                        Text = i.WorkName,
                        Value = i.Id.ToString()
                    });
            }

            var re = await _constructionTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
            if (re != null && re.IsSuccess)
            {
                model.ConstructionTypeList = JsonConvert.DeserializeObject<List<ConstructionTypeDTO>>
                    (Convert.ToString(re.Result)).Select(i => new SelectListItem
                    {
                        Text = i.Ctype,
                        Value = i.Cno.ToString()
                    });
            }
            return View(model);
        }

        public async Task<IActionResult> DeleteServiceCharges(int serviceChargesId)
        {
            ServiceChargesDeleteVM serviceChargesVM = new();
            var response = await _serviceChargesService.GetAsync<APIResponse>(serviceChargesId, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                ServiceChargesDTO model = JsonConvert.DeserializeObject<ServiceChargesDTO>(Convert.ToString(response.Result));
                serviceChargesVM.ServiceCharges = model;
            }

            if (response != null && response.IsSuccess)
            {
                response = await _workTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    serviceChargesVM.WorkTypeList = JsonConvert.DeserializeObject<List<WorkTypeDTO>>
                        (Convert.ToString(response.Result)).Select(i => new SelectListItem
                        {
                            Text = i.Work_Type,
                            Value = i.Id.ToString()
                        });
                }

                response = await _workService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    serviceChargesVM.WorkList = JsonConvert.DeserializeObject<List<WorkDTO>>
                        (Convert.ToString(response.Result)).Select(i => new SelectListItem
                        {
                            Text = i.WorkName,
                            Value = i.Id.ToString()
                        });
                }

                response = await _constructionTypeService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(SD.SessionToken));
                if (response != null && response.IsSuccess)
                {
                    serviceChargesVM.ConstructionTypeList = JsonConvert.DeserializeObject<List<ConstructionTypeDTO>>
                        (Convert.ToString(response.Result)).Select(i => new SelectListItem
                        {
                            Text = i.Ctype,
                            Value = i.Cno.ToString()
                        });
                }
                return View(serviceChargesVM);
            }


            return NotFound();
        }

        //[Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteServiceCharges(ServiceChargesDeleteVM model)
        {

            var response = await _serviceChargesService.DeleteAsync<APIResponse>(model.ServiceCharges.Id, HttpContext.Session.GetString(SD.SessionToken));
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexServiceCharges));
            }

            return View(model);
        }

    }
}
