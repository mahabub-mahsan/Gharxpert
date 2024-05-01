using AutoMapper;
using GharxpertAPI.Models;
using GharxpertAPI.Models.Dto;
using GharxpertAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GharxpertAPI.Controllers
{
    [Route("api/quotationDetailAPI")]
    [ApiController]
    public class QuotationDetailAPIController : Controller
    {
        protected APIReponse _response;
        private readonly IQuotationMasterRepository _dbQuotationMasters;
        private readonly IQuotationDetailRepository _dbQuotationDetails;
        private readonly IWorkTypeRepository _dbWorkType;
        private readonly IMapper _mapper;
        public QuotationDetailAPIController(IQuotationMasterRepository dbQuotationMaster, IQuotationDetailRepository dbQoutationDetail, IWorkTypeRepository dbWorkType, IMapper mapper)
        {
            _dbQuotationMasters = dbQuotationMaster;
            _dbQuotationDetails = dbQoutationDetail;
            _dbWorkType = dbWorkType;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIReponse>> GetQuotationDetails()
        {
            try
            {
                IEnumerable<QuotationDetail> quotationDetailList = await _dbQuotationDetails.GetAllAsync(includeProperties: "WorkType,QuotationMaster");
                _response.Result = _mapper.Map<List<QuotationDetailDTO>>(quotationDetailList);
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

        [HttpGet("{id:int}", Name = "GetQuotationDetail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIReponse>> GetQuotationDetail(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Enter a valid ID");
                    return BadRequest(_response);
                }
                var quotationDetail = await _dbQuotationDetails.GetAsync(u => u.Id == id);
                if (quotationDetail == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Please Enter a correct ID");
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<QuotationDetailDTO>(quotationDetail);
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

        public async Task<ActionResult<APIReponse>> CreateQuotationDetail([FromBody] QuotationDetailCreateDTO createDTO)
        {
            try
            {
                if (await _dbQuotationDetails.GetAsync(u => u.Id == createDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "QuotationDetail Id already Exists!");
                    return BadRequest(ModelState);
                }

                if (await _dbQuotationMasters.GetAsync(u => u.QId == createDTO.QId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "QuotationMaster ID is Invalid");
                    return BadRequest(ModelState);
                }

                if (await _dbWorkType.GetAsync(u => u.Id == createDTO.WorkTypeId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "WorkType ID is Invalid");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                QuotationDetail quotationDetail = _mapper.Map<QuotationDetail>(createDTO);

                await _dbQuotationDetails.CreateAsync(quotationDetail);
                _response.Result = _mapper.Map<QuotationDetailDTO>(quotationDetail);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetQuotationMaster, GetWorkType", new { id = quotationDetail.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteQuotationDetail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIReponse>> DeleteQuotationDetail(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var quotationDetail = await _dbQuotationDetails.GetAsync(u => u.Id == id);
                if (quotationDetail == null)
                {
                    return NotFound();
                }
                await _dbQuotationDetails.RemoveAsync(quotationDetail);
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


        [HttpPut("{id:int}", Name = "UpdateQuotationDetail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIReponse>> UpdateQuotationDetail(int id, [FromBody] QuotationDetailUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.QId)
                {
                    return BadRequest();
                }

                if (await _dbQuotationMasters.GetAsync(u => u.QId == updateDTO.QId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Quotation Master ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (await _dbWorkType.GetAsync(u => u.Id == updateDTO.WorkTypeId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "WorkType ID is Invalid !");
                    return BadRequest(ModelState);
                }

                QuotationDetail model = _mapper.Map<QuotationDetail>(updateDTO);


                await _dbQuotationDetails.UpdateAsync(model);
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
