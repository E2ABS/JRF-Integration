// Controllers/AddPaymentController.cs
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YourNamespace.Services;
using YourNamespace.Models;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddPaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public AddPaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncomingPayment([FromBody] IncomingPaymentRequestModel payment)
        {
            try
            {
                var response = await _paymentService.CreateIncomingPayment(payment);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
