using AutoMapper;
using GharxpertAPI.Models;
using GharxpertAPI.Models.Dto;
using GharxpertAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GharxpertAPI.Controllers
{
    [Route("api/WorkAPI")]
    [ApiController]
    public class WorkAPIController : ControllerBase
    {
        protected APIReponse _response;
        private readonly IWorkRepository _dbWorks;
        private readonly IMapper _mapper;

        public WorkAPIController(IWorkRepository dbWorks, IMapper mapper)
        {
            _dbWorks = dbWorks;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIReponse>> GetWorks()
        {
            try
            {
                IEnumerable<Work> workList = await _dbWorks.GetAllAsync();
                _response.Result = _mapper.Map<List<WorkDTO>>(workList);
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetWork")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIReponse>> GetWork(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var work = await _dbWorks.GetAsync(u => u.Id == id);
                if (work == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<WorkDTO>(work);
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
        public async Task<ActionResult<APIReponse>> CreateWork([FromBody] WorkCreateDTO createDTO)
        {
            try
            {
                if (await _dbWorks.GetAsync(u => u.Id == createDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Work Id already Exists!");
                    return BadRequest(ModelState);
                }

                if (await _dbWorks.GetAsync(u => u.WorkName.ToLower() == createDTO.WorkName.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Work already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Work work = _mapper.Map<Work>(createDTO);



                await _dbWorks.CreateAsync(work);
                _response.Result = _mapper.Map<WorkCreateDTO>(work);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetWork", new { id = work.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteWork")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIReponse>> DeleteWork(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var work = await _dbWorks.GetAsync(u => u.Id == id);
                if (work == null)
                {
                    return NotFound();
                }
                await _dbWorks.RemoveAsync(work);
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

        [HttpPut("{id:int}", Name = "UpdateWork")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIReponse>> UpdateWork(int id, [FromBody] WorkUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }


                Work model = _mapper.Map<Work>(updateDTO);


                await _dbWorks.UpdateAsync(model);
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