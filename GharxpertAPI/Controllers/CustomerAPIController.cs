using AutoMapper;
using GharxpertAPI.Models;
using GharxpertAPI.Models.Dto;
using GharxpertAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GharxpertAPI.Controllers
{
    [Route("api/customerAPI")]
    [ApiController]
    public class CustomerAPIController : ControllerBase
    {
        protected APIReponse _response;
        private readonly ICustomerRepository _dbCustomers;
        private readonly IMapper _mapper;

        public CustomerAPIController(ICustomerRepository dbCustomers, IMapper mapper)
        {
            _dbCustomers = dbCustomers;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIReponse>> GetCustomers()
        {
            try
            {
                IEnumerable<Customer> customerList = await _dbCustomers.GetAllAsync();
                _response.Result = _mapper.Map<List<CustomerDTO>>(customerList);
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }



        [HttpGet("{id:int}", Name = "GetCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<APIReponse>> GetCustomer(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var customer = await _dbCustomers.GetAsync(u => u.CustomerId == id);
                if (customer == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<CustomerDTO>(customer);
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
        public async Task<ActionResult<APIReponse>> CreateCustomer([FromBody] CustomerCreateDTO createDTO)
        {
            try
            {

                if (await _dbCustomers.GetAsync(u => u.Mobile.ToLower() == createDTO.Mobile.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Customer already Exists!");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Customer customer = _mapper.Map<Customer>(createDTO);



                await _dbCustomers.CreateAsync(customer);
                _response.Result = _mapper.Map<CustomerCreateDTO>(customer);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetCustomer", new { id = customer.CustomerId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteCustomer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIReponse>> DeleteCustomer(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var customer = await _dbCustomers.GetAsync(u => u.CustomerId == id);
                if (customer == null)
                {
                    return NotFound();
                }
                await _dbCustomers.RemoveAsync(customer);
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

        [HttpPut("{id:int}", Name = "UpdateCustomer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIReponse>> UpdateCustomer(int id, [FromBody] CustomerUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.CustomerId)
                {
                    return BadRequest();
                }


                Customer model = _mapper.Map<Customer>(updateDTO);


                await _dbCustomers.UpdateAsync(model);
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
