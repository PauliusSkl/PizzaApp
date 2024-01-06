using Microsoft.EntityFrameworkCore;
using PizzaApp.Data.Entities;

namespace PizzaApp.Data.Repositories;

public interface IPizzaSizeRepository
{
    Task<List<PizzaSize>> GetAllPizzaSizesAsync();
    Task<PizzaSize?> GetSpecificPizzaSize(int sizeId);
}

public class PizzaSizeRepository : IPizzaSizeRepository
{
    private readonly PizzaDatabaseContext _context;

    public PizzaSizeRepository(PizzaDatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<PizzaSize>> GetAllPizzaSizesAsync()
    {
        return await _context.PizzaSizes.ToListAsync();
    }

    public async Task<PizzaSize?> GetSpecificPizzaSize(int sizeId)
    {
        return await _context.PizzaSizes.FirstOrDefaultAsync(s => s.Id == sizeId);
    }
}
