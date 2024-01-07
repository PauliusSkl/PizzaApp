using Microsoft.EntityFrameworkCore;
using PizzaApp.Data.Entities;

namespace PizzaApp.Data.Repositories;

public interface IPizzaOrderRepository
{
    Task CreatePizzaOrderAsync(PizzaOrder pizzaOrder);
    Task<List<PizzaOrder>> GetAllPizzaOrdersAsync();
}

public class PizzaOrderRepository : IPizzaOrderRepository
{
    private readonly PizzaDatabaseContext _context;

    public PizzaOrderRepository(PizzaDatabaseContext context)
    {
        _context = context;
    }

    public async Task CreatePizzaOrderAsync(PizzaOrder pizzaOrder)
    {
        await _context.Pizzas.AddAsync(pizzaOrder);
        await _context.SaveChangesAsync();
    }

    public async Task<List<PizzaOrder>> GetAllPizzaOrdersAsync()
    {
        var pizzaOrders = await _context.Pizzas
            .Include(p => p.Size)
            .Include(p => p.PizzaOrderToppings)
                .ThenInclude(pt => pt.Topping)
            .ToListAsync();

        return pizzaOrders;
    }
}
