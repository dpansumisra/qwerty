using E_Commerce.Models;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController: ControllerBase
{   
    
    private readonly ProductService ProductService;
    
    public ProductController(ProductService productService)
    {
        ProductService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        var products = await ProductService.GetProductsAsync();
        return Ok(products);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await ProductService.GetProductByIdAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        var createdProduct = await ProductService.AddProductAsync(product);
        return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.ProductID }, createdProduct);
    }
    // GET: /Product/Discount/{code}
    [HttpGet("Discount/{code}")]
    public async Task<ActionResult<Discount>> GetDiscountByCode(string code)
    {
        var discount = await ProductService.GetDiscountByCodeAsync(code);
        if (discount == null)
        {
            return NotFound("Discount code not found");
        }

        return Ok(discount);
    }

    // POST: /Product/Discount
    [HttpPost("Discount")]
    public async Task<ActionResult<Discount>> PostDiscount(Discount discount)
    {
        var createdDiscount = await ProductService.AddDiscountAsync(discount);
        return CreatedAtAction(nameof(GetDiscountByCode), new { code = createdDiscount.DiscountCode }, createdDiscount);
    }
    // PUT: api/products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, Product product)
    {
        if (id != product.ProductID)
        {
            return BadRequest();
        }

        try
        {
            await ProductService.UpdateProductAsync(product);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await ProductService.GetProductByIdAsync(id) == null)
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

    // DELETE: api/products/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await ProductService.DeleteProductAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}