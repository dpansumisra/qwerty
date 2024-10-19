using E_Commerce.Models;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly DiscountService _discountService;

        public DiscountController(DiscountService discountService)
        {
            _discountService = discountService;
        }

        // GET: /Discount
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Discount>>> GetDiscounts()
        {
            var discounts = await _discountService.GetDiscountsAsync();
            return Ok(discounts);
        }

        // GET: /Discount/{code}
        [HttpGet("{code}")]
        public async Task<ActionResult<Discount>> GetDiscountByCode(string code)
        {
            var discount = await _discountService.GetDiscountByCodeAsync(code);
            if (discount == null)
            {
                return NotFound("Discount code not found");
            }

            return Ok(discount);
        }

        // POST: /Discount
        [HttpPost]
        public async Task<ActionResult<Discount>> PostDiscount(Discount discount)
        {
            var createdDiscount = await _discountService.AddDiscountAsync(discount);
            return CreatedAtAction(nameof(GetDiscountByCode), new { code = createdDiscount.DiscountCode }, createdDiscount);
        }

        // PUT: /Discount/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiscount(int id, Discount discount)
        {
            if (id != discount.DiscountID)
            {
                return BadRequest();
            }

            try
            {
                await _discountService.UpdateDiscountAsync(discount);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _discountService.GetDiscountByCodeAsync(discount.DiscountCode) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: /Discount/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var result = await _discountService.DeleteDiscountAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}