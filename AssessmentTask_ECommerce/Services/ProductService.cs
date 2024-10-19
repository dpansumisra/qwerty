using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services;

public class ProductService
{
    private readonly ApplicationDbContext _context;

    // Add a discount
    public async Task<Discount> AddDiscountAsync(Discount discount)
    {
        _context.Discounts.Add(discount);
        await _context.SaveChangesAsync();
        return discount;
    }

    // Update a discount
    public async Task<Discount> UpdateDiscountAsync(Discount discount)
    {
        _context.Discounts.Update(discount);
        await _context.SaveChangesAsync();
        return discount;
    }

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Discount> GetDiscountByCodeAsync(string code)
    {
        return await _context.Discounts.FirstOrDefaultAsync(d => d.DiscountCode.ToUpper() == code.ToUpper());
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return false;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}