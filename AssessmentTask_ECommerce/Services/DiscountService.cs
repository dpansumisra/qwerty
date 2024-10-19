using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Services;

public class DiscountService
{
    private readonly ApplicationDbContext _context;

    public DiscountService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Get all discounts
    public async Task<IEnumerable<Discount>> GetDiscountsAsync()
    {
        return await _context.Discounts.ToListAsync();
    }

    // Get discount by code
    public async Task<Discount> GetDiscountByCodeAsync(string code)
    {
        return await _context.Discounts.FirstOrDefaultAsync(d=> d.DiscountCode.ToUpper() == code.ToUpper());
    }

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

    // Delete a discount by ID
    public async Task<bool> DeleteDiscountAsync(int id)
    {
        var discount = await _context.Discounts.FindAsync(id);
        if (discount == null)
        {
            return false;
        }

        _context.Discounts.Remove(discount);
        await _context.SaveChangesAsync();
        return true;
    }
}
