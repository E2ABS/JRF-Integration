// Controllers/AddCustomerController.cs
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YourNamespace.Services;
using YourNamespace.Models;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddCustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public AddCustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerRequestModel customer)
        {
            try
            {
                var response = await _customerService.CreateCustomer(customer);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
