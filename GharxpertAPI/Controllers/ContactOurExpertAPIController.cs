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
    [Route("api/ContactOurExpertAPI")]
    [ApiController]
    public class ContactOurExpertAPIController : ControllerBase
    {
        protected APIReponse _response;
        private readonly IContactOurExpertRepository _dbContactOurExpert;
        private readonly IMapper _mapper;

        public ContactOurExpertAPIController(IContactOurExpertRepository dbContactOurExpert, IMapper mapper)
        {
            _dbContactOurExpert = dbContactOurExpert;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIReponse>> GetContactOurExperts()
        {
            try
            {
                IEnumerable<ContactOurExpert> contactOurExpertList = await _dbContactOurExpert.GetAllAsync();
                _response.Result = _mapper.Map<List<ContactOurExpertDTO>>(contactOurExpertList);
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("{id:int}", Name = "GetContactOurExpert")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIReponse>> GetContactOurExpert(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var contactOurExpert = await _dbContactOurExpert.GetAsync(u => u.Id == id);
                if (contactOurExpert == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<ContactOurExpertDTO>(contactOurExpert);
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
        public async Task<ActionResult<APIReponse>> CreateWork([FromBody] ContactOurExpertCreateDTO createDTO)
        {
            try
            {
                //if (await _dbContactOurExpert.GetAsync(u => u.Id == createDTO.Id) != null)
                //{
                //    ModelState.AddModelError("ErrorMessages", "ContactOurExpert Id already Exists!");
                //    return BadRequest(ModelState);
                //}

                if (await _dbContactOurExpert.GetAsync(u => u.CName.ToLower() == createDTO.CName.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "CName already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                ContactOurExpert contactOurExpert = _mapper.Map<ContactOurExpert>(createDTO);



                await _dbContactOurExpert.CreateAsync(contactOurExpert);
                _response.Result = _mapper.Map<ContactOurExpertCreateDTO>(contactOurExpert);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetWork", new { id = contactOurExpert.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteContactOurExpert")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIReponse>> DeleteContactOurExpert(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var contactOurExpert = await _dbContactOurExpert.GetAsync(u => u.Id == id);
                if (contactOurExpert == null)
                {
                    return NotFound();
                }
                await _dbContactOurExpert.RemoveAsync(contactOurExpert);
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

        [HttpPut("{id:int}", Name = "UpdateContactOurExpert")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIReponse>> UpdateContactOurExpert(int id, [FromBody] ContactOurExpertUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }


                ContactOurExpert model = _mapper.Map<ContactOurExpert>(updateDTO);


                await _dbContactOurExpert.UpdateAsync(model);
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