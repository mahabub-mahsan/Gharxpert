using AutoMapper;
using GharxpertAPI.Models;
using GharxpertAPI.Models.Dto;
using GharxpertAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GharxpertAPI.Controllers
{
    [Route("api/constructionTypeAPI")]
    [ApiController]
    public class ConstructionTypeAPIController : ControllerBase
    {
        
        protected APIReponse _response;
        private readonly IConstructionTypeRepository _dbConstructionTypes;
        private readonly IMapper _mapper;

        public ConstructionTypeAPIController(IConstructionTypeRepository dbConstructionTypes, IMapper mapper)
        {
            _dbConstructionTypes = dbConstructionTypes;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIReponse>> GetConstructionTypes()
        {
            try
            {
                IEnumerable<ConstructionType> constructionTypeList = await _dbConstructionTypes.GetAllAsync();
                _response.Result = _mapper.Map<List<ConstructionTypeDTO>>(constructionTypeList);
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetConstructionType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIReponse>> GetConstructionType(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var constructionType = await _dbConstructionTypes.GetAsync(u => u.Cno == id);
                if (constructionType == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<ConstructionTypeDTO>(constructionType);
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
        public async Task<ActionResult<APIReponse>> CreateConstructionType([FromBody] ConstructionTypeCreateDTO createDTO)
        {
            try
            {
                if (await _dbConstructionTypes.GetAsync(u => u.Cno == createDTO.Cno) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Construction Id already Exists!");
                    return BadRequest(ModelState);
                }

                if (await _dbConstructionTypes.GetAsync(u => u.Ctype.ToLower() == createDTO.Ctype.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "ConstructionType already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                ConstructionType constructionType = _mapper.Map<ConstructionType>(createDTO);



                await _dbConstructionTypes.CreateAsync(constructionType);
                _response.Result = _mapper.Map<ConstructionTypeCreateDTO>(constructionType);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetConstructionType", new { id = constructionType.Cno }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteConstructionType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIReponse>> DeleteConstructionType(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var constructionType = await _dbConstructionTypes.GetAsync(u => u.Cno == id);
                if (constructionType == null)
                {
                    return NotFound();
                }
                await _dbConstructionTypes.RemoveAsync(constructionType);
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

        [HttpPut("{id:int}", Name = "UpdateConstructionType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIReponse>> UpdateConstructionType(int id, [FromBody] ConstructionTypeUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Cno)
                {
                    return BadRequest();
                }


                ConstructionType model = _mapper.Map<ConstructionType>(updateDTO);


                await _dbConstructionTypes.UpdateAsync(model);
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
