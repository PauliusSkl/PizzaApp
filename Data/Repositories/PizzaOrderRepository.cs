using Microsoft.EntityFrameworkCore;
using PizzaApp.Data.Dtos;
using PizzaApp.Data.Entities;

namespace PizzaApp.Data.Repositories;

public interface IPizzaOrderRepository
{
    Task<PizzaOrderDto> CreatePizzaOrderAsync(PizzaSize pizzaSize, List<Topping> toppings, decimal price);
    Task<List<PizzaOrderDto>> GetAllPizzaOrdersAsync();
}

public class PizzaOrderRepository : IPizzaOrderRepository
{
    private readonly PizzaDatabaseContext _context;

    public PizzaOrderRepository(PizzaDatabaseContext context)
    {
        _context = context;
    }

    public async Task<PizzaOrderDto> CreatePizzaOrderAsync(PizzaSize pizzaSize, List<Topping> toppings, decimal price)
    {
        var pizzaOrder = new PizzaOrder
        {
            Size = pizzaSize,
            TotalPrice = price
        };

        foreach (var topping in toppings)
        {
            pizzaOrder.PizzaOrderToppings.Add(new PizzaOrderTopping
            {
                Topping = topping
            });
        }

        await _context.Pizzas.AddAsync(pizzaOrder);
        await _context.SaveChangesAsync();

        var pizzaOrderDto = new PizzaOrderDto
        {
            Id = pizzaOrder.Id,
            TotalPrice = pizzaOrder.TotalPrice,
            Size = pizzaOrder.Size?.Size,
            Toppings = pizzaOrder.PizzaOrderToppings.Select(pt => pt.Topping.Name).ToList()
        };

        return pizzaOrderDto;
    }
    public async Task<List<PizzaOrderDto>> GetAllPizzaOrdersAsync()
    {
        var pizzaOrders = await _context.Pizzas
            .Include(p => p.Size)
            .Include(p => p.PizzaOrderToppings)
                .ThenInclude(pt => pt.Topping)
            .ToListAsync();

        var pizzaOrderDtos = pizzaOrders.Select(p => new PizzaOrderDto
        {
            Id = p.Id,
            TotalPrice = p.TotalPrice,
            Size = p.Size?.Size,
            Toppings = p.PizzaOrderToppings.Select(pt => pt.Topping.Name).ToList()
        }).ToList();

        return pizzaOrderDtos;
    }
}
