using api.Dtos.Customer;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("/api/v1/customers")]
    public class CustomerController(CustomerService service) : ControllerBase
    {
        private readonly CustomerService _Service = service;

        [HttpPost]
        public IActionResult Create([FromBody] CustomerRequest request)
        {
            if (ModelState.IsValid)
            {
                CustomerResponse response = _Service.Create(request);

                return Ok(response);
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult FindAll()
        {
            List<CustomerResponse> response = _Service.FindAll();

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult FindById([FromRoute] int id)
        {
            CustomerResponse response = _Service.FindById(id);

            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, CustomerRequest request)
        {
            if (ModelState.IsValid)
            {
                CustomerResponse response = _Service.Update(id, request);
                return Ok(response);

            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _Service.Delete(id);

            return NoContent();
        }
    }
}