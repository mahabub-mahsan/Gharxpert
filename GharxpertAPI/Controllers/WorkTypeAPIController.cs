using AutoMapper;
using GharxpertAPI.Models;
using GharxpertAPI.Models.Dto;
using GharxpertAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GharxpertAPI.Controllers
{
    [Route("api/WorkTypeAPI")]
    [ApiController]
    public class WorkTypeAPIController : ControllerBase
    {
        protected APIReponse _response;
        private readonly IWorkTypeRepository _dbWorkType;
        private readonly IWorkRepository _dbWork;
        private readonly IMapper _mapper;
        public WorkTypeAPIController(IWorkTypeRepository dbWorkType, IWorkRepository dbWork, IMapper mapper)
        {
            _dbWorkType = dbWorkType;
            _dbWork = dbWork;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIReponse>> GetWorkTypes()
        {
            try
            {
                IEnumerable<WorkType> workTypeList = await _dbWorkType.GetAllAsync(includeProperties: "Work");
                _response.Result = _mapper.Map<List<WorkTypeDTO>>(workTypeList);
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

        [HttpGet("{id:int}", Name = "GetWorkType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIReponse>> GetWorkType(int id)
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
                var workType = await _dbWorkType.GetAsync(u => u.Id == id);
                if (workType == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("Please Enter a correct ID");
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<WorkTypeDTO>(workType);
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

        public async Task<ActionResult<APIReponse>> CreateWorkType([FromBody] WorkTypeCreateDTO createDTO)
        {
            try
            {
                if (await _dbWorkType.GetAsync(u => u.Id == createDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "WorkType Id already Exists!");
                    return BadRequest(ModelState);
                }

                if (await _dbWork.GetAsync(u => u.Id == createDTO.WorkID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Work ID is Invalid");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                WorkType workType = _mapper.Map<WorkType>(createDTO);

                await _dbWorkType.CreateAsync(workType);
                _response.Result = _mapper.Map<WorkTypeDTO>(workType);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetWork", new { id = workType.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteWorkType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIReponse>> DeleteWorkType(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var workType = await _dbWorkType.GetAsync(u => u.Id == id);
                if (workType == null)
                {
                    return NotFound();
                }
                await _dbWorkType.RemoveAsync(workType);
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

        [HttpPut("{id:int}", Name = "UpdateWorkType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<APIReponse>> UpdateWorkType(int id, [FromBody] WorkTypeUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }

                if (await _dbWork.GetAsync(u => u.Id == updateDTO.WorkID) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Work ID is Invalid !");
                    return BadRequest(ModelState);
                }


                WorkType model = _mapper.Map<WorkType>(updateDTO);


                await _dbWorkType.UpdateAsync(model);
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
