using Microsoft.EntityFrameworkCore;
using PizzaApp.Data.Entities;

namespace PizzaApp.Data.Repositories;

public interface IToppingsRepository
{
    Task<List<Topping>> GetAllToppingsAsync();
    Task<List<Topping>> GetSpecificToppingsAsync(List<int> toppingIds);
}

public class ToppingsRepository : IToppingsRepository
{
    private readonly PizzaDatabaseContext _context;

    public ToppingsRepository(PizzaDatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<Topping>> GetAllToppingsAsync()
    {
        return await _context.Toppings.ToListAsync();
    }

    public async Task<List<Topping>> GetSpecificToppingsAsync(List<int> toppingIds)
    {
        return await _context.Toppings.Where(t => toppingIds.Contains(t.Id)).ToListAsync();
    }

}
