using AutoMapper;
using GharxpertAPI.Data;
using GharxpertAPI.Models;
using GharxpertAPI.Models.Dto;
using GharxpertAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.InteropServices;

namespace GharxpertAPI.Controllers
{
    [Route("api/quotationMasterAPI")]
    [ApiController]
    public class QuotationMasterAPIController : ControllerBase
    {

        private readonly ApplicationDbContext _context; 
        protected APIReponse _response;
        private readonly IQuotationMasterRepository _dbQuotationMasters;
        private readonly IQuotationDetailRepository _dbQuotationDetails;
        private readonly IWorkTypeRepository _dbWorkType;
        private readonly IWorkRepository _dbWork;
        private readonly ICustomerRepository _dbCustomer;
        private readonly IMapper _mapper;

        public QuotationMasterAPIController(ApplicationDbContext context,IQuotationMasterRepository dbQuotationMaster, IQuotationDetailRepository dbQoutationDetail, IWorkTypeRepository dbWorkType, ICustomerRepository dbCustomer, IWorkRepository dbWork, IMapper mapper)
        {
            _context = context;
            _dbQuotationMasters = dbQuotationMaster;
            _dbQuotationDetails = dbQoutationDetail;
            _dbWorkType = dbWorkType;
            _dbWork = dbWork;
            _dbCustomer = dbCustomer;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIReponse>> GetQuotationMasters()
        {
            try
            {
                IEnumerable<QuotationMaster> quotationMasterList = await _dbQuotationMasters.GetAllAsync(includeProperties: "Work,WorkType,Customer");
                _response.Result = _mapper.Map<List<QuotationMasterDTO>>(quotationMasterList);
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

        



        [HttpGet("{id:int}", Name = "GetQuotationMaster")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIReponse>> GetQuotationMaster(int id)
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
                var quotationMaster = await _dbQuotationMasters.GetAsync(u => u.QId == id);
                if (quotationMaster == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Please Enter a correct ID");
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<QuotationMasterDTO>(quotationMaster);
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



        //[HttpPost]
        //public async Task<ActionResult<APIReponse>> CreateQuotation([FromBody] QuotationMaster quotationMaster)
        //{
        //    if (quotationMaster == null)
        //    {
        //        return BadRequest("Quotatation Id is null.");
        //    }

        //    _context.QuotationMasters.Add(quotationMaster);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetQuotationById), new { id = quotationMaster.QId }, quotationMaster);
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<APIReponse>> GetQuotationById(int id)
        //{
        //    var quotationMaster = await _context.QuotationMasters
        //        .Include(o => o.QuotationDetails)
        //        .FirstOrDefaultAsync(o => o.QId == id);

        //    if (quotationMaster == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(quotationMaster);
        //}


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIReponse>> CreateQuotationMaster([FromBody] QuotationMasterCreateDTO createDTO)
        {
            try
            {

                if (await _dbQuotationMasters.GetAsync(u => u.QId == createDTO.QId) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Quotation Id already Exists!");
                    return BadRequest(ModelState);
                }

                if (await _dbWork.GetAsync(u => u.Id == createDTO.WorkId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Work ID is Invalid");
                    return BadRequest(ModelState);
                }

                if (await _dbWorkType.GetAsync(u => u.Id == createDTO.WorkTypeId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "WorkType ID is Invalid");
                    return BadRequest(ModelState);
                }

                if (await _dbCustomer.GetAsync(u => u.CustomerId == createDTO.CustomerId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Customer ID is Invalid");
                    return BadRequest(ModelState);
                }

                

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                QuotationMaster quotationMaster = _mapper.Map<QuotationMaster>(createDTO);

                //var lastEntity = await _context.QuotationMasters.OrderByDescending(e => e.QId).FirstOrDefaultAsync();
                //int newId = lastEntity != null ? lastEntity.QId + 1 : 1;

                await _dbQuotationMasters.CreateAsync(quotationMaster);
                _response.Result = _mapper.Map<QuotationMasterDTO>(quotationMaster);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetQuotationMaster", new { id = quotationMaster.QId }, _response);
                //return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpDelete("{id:int}", Name = "DeleteQuotationMaster")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIReponse>> DeleteQuotationMaster(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var quotationMaster = await _dbQuotationMasters.GetAsync(u => u.QId == id);
                if (quotationMaster == null)
                {
                    return NotFound();
                }
                await _dbQuotationMasters.RemoveAsync(quotationMaster);
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

        [HttpPut("{id:int}", Name = "UpdateQuotationMaster")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIReponse>> UpdateQuotationMaster(int id, [FromBody] QuotationMasterUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.QId)
                {
                    return BadRequest();
                }

                if (await _dbWork.GetAsync(u => u.Id == updateDTO.WorkId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Work ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (await _dbWorkType.GetAsync(u => u.Id == updateDTO.WorkTypeId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "WorkType ID is Invalid !");
                    return BadRequest(ModelState);
                }

                if (await _dbCustomer.GetAsync(u => u.CustomerId == updateDTO.CustomerId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Customer ID is Invalid !");
                    return BadRequest(ModelState);
                }


                QuotationMaster model = _mapper.Map<QuotationMaster>(updateDTO);


                await _dbQuotationMasters.UpdateAsync(model);
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
