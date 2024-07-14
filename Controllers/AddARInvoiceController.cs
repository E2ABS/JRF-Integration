using System;
using System.Threading.Tasks;
using YourNamespace.Services;
using Microsoft.AspNetCore.Mvc;
using YourNamespace.Models;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddARInvoiceController : ControllerBase
    {
        private readonly ISapB1Service _sapB1Service;

        public AddARInvoiceController(ISapB1Service sapB1Service)
        {
            _sapB1Service = sapB1Service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateARInvoice([FromBody] ARInvoiceRequestModel arInvoice)
        {
            try
            {
                var response = await _sapB1Service.CreateARInvoice(arInvoice);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

   
}
