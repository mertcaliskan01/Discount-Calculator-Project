using Microsoft.AspNetCore.Mvc;
using DiscountCalculatorAPI.Models;
using DiscountCalculatorAPI.Services;

namespace DiscountCalculatorAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly DiscountCalculatorService _discountService;

        public ProductController(DiscountCalculatorService discountService)
        {
            _discountService = discountService;
        }

        [HttpPost("CalculateDiscount")]
        public IActionResult CalculateDiscount([FromBody] DiscountRequest request)
        {
            var response = _discountService.CalculateDiscount(request);
            return Ok(response);
        }
    }

}
