using AutoMapper;
using GharxpertAPI.Models;
using GharxpertAPI.Models.Dto;
using GharxpertAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GharxpertAPI.Controllers
{
    [Route("api/quotationStatusAPI")]
    [ApiController]
    public class QuotationStatusAPIController : ControllerBase
    {
        protected APIReponse _response;
        private readonly IQuotationStatusRepository _dbQuotationStatus;
        private readonly IMapper _mapper;

        public QuotationStatusAPIController(IQuotationStatusRepository dbQuotationStatus, IMapper mapper)
        {
            _dbQuotationStatus = dbQuotationStatus;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIReponse>> GetQuotationStatuses()
        {
            try
            {
                IEnumerable<QuotationStatus> quotationStatusList = await _dbQuotationStatus.GetAllAsync();
                _response.Result = _mapper.Map<List<QuotationStatusDTO>>(quotationStatusList);
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }



        [HttpGet("{id:int}", Name = "GetQuotationStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIReponse>> GetQuotationStatus(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var quotationStatus = await _dbQuotationStatus.GetAsync(u => u.Id == id);
                if (quotationStatus == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<QuotationStatusDTO>(quotationStatus);
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
        public async Task<ActionResult<APIReponse>> CreateQuotationStatus([FromBody] QuotationStatusCreateDTO createDTO)
        {
            try
            {

                if (await _dbQuotationStatus.GetAsync(u => u.Status.ToLower() == createDTO.Status.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Quotation Status already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                QuotationStatus quotationStatus = _mapper.Map<QuotationStatus>(createDTO);



                await _dbQuotationStatus.CreateAsync(quotationStatus);
                _response.Result = _mapper.Map<QuotationStatusCreateDTO>(quotationStatus);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetQuotationStatus", new { id = quotationStatus.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteQuotationStatus")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIReponse>> DeleteQuotationStatus(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var quotationStatus = await _dbQuotationStatus.GetAsync(u => u.Id == id);
                if (quotationStatus == null)
                {
                    return NotFound();
                }
                await _dbQuotationStatus.RemoveAsync(quotationStatus);
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

        [HttpPut("{id:int}", Name = "UpdateQuotationStatus")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIReponse>> UpdateQuotationStatus(int id, [FromBody] QuotationStatusUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }


                QuotationStatus model = _mapper.Map<QuotationStatus>(updateDTO);


                await _dbQuotationStatus.UpdateAsync(model);
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
