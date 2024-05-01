using AutoMapper;
using GharxpertAPI.Models;
using GharxpertAPI.Models.Dto;
using GharxpertAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GharxpertAPI.Controllers
{
    [Route("api/ServiceChargesAPI")]
    [ApiController]
    public class ServiceChargesAPIController : ControllerBase
    {
        protected APIReponse _response;
        private readonly IServiceChargesRepository _dbServiceCharge;
        private readonly IWorkTypeRepository _dbWorkType;
        private readonly IMapper _mapper;

        public ServiceChargesAPIController(IServiceChargesRepository dbServiceCharge, IWorkTypeRepository dbWorkType, IMapper mapper)
        {
            _dbServiceCharge = dbServiceCharge;
            _dbWorkType = dbWorkType;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<APIReponse>> GetServiceCharges()
        {
            try
            {
                IEnumerable<ServiceCharges> serviceChargesList = await _dbServiceCharge.GetAllAsync(includeProperties: "WorkType");
                _response.Result = _mapper.Map<List<ServiceChargesDTO>>(serviceChargesList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetServiceCharge")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIReponse>> GetServiceCharge(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Enter a vaild ID");
                    return BadRequest(_response);
                }
                var serviceCharges = await _dbServiceCharge.GetAsync(u => u.Id == id);
                if (serviceCharges == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Please Enter a correct ID");
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<ServiceChargesDTO>(serviceCharges);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIReponse>> CreateServiceCharges([FromBody]  ServiceChargesCreateDTO createDTO)
        {
            try
            {
                if (await _dbServiceCharge.GetAsync(u => u.Id == createDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Service Charge Id already Exists!");
                    return BadRequest(ModelState);
                }

                if (await _dbWorkType.GetAsync(u => u.Id == createDTO.WorkTypeId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Work Type ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                ServiceCharges serviceCharges = _mapper.Map<ServiceCharges>(createDTO);


                await _dbServiceCharge.CreateAsync(serviceCharges);
                _response.Result = _mapper.Map<ServiceChargesDTO>(serviceCharges);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetWorkType", new { id = serviceCharges.WorkTypeId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteServiceCharges")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIReponse>> DeleteServiceCharges(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var serviceCharges = await _dbServiceCharge.GetAsync(u => u.Id == id);
                if (serviceCharges == null)
                {
                    return NotFound();
                }
                await _dbServiceCharge.RemoveAsync(serviceCharges);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateServiceCharges")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIReponse>> UpdateServiceCharges(int id, [FromBody] ServiceChargesUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }

                if (await _dbWorkType.GetAsync(u => u.Id == updateDTO.WorkTypeId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Work Type ID is Invalid !");
                    return BadRequest(ModelState);
                }


                ServiceCharges model = _mapper.Map<ServiceCharges>(updateDTO);


                await _dbServiceCharge.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
