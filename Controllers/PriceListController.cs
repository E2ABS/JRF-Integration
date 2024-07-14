// Controllers/PriceListController.cs
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YourNamespace.Services;
using YourNamespace.Models;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class PriceListController : ControllerBase
    {
        private readonly IPriceListService _priceListService;

        public PriceListController(IPriceListService priceListService)
        {
            _priceListService = priceListService;
        }

        [HttpGet]
        public async Task<IActionResult> GetECommercePriceList()
        {
            try
            {
                var priceList = await _priceListService.GetECommercePriceList();
                if (priceList == null)
                {
                    return NotFound("E-Commerce price list not found.");
                }
                return Ok(priceList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
