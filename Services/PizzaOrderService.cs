using PizzaApp.Data.Dtos;
using PizzaApp.Data.Entities;
using PizzaApp.Data.Models;
using PizzaApp.Data.Repositories;

namespace PizzaApp.Services;

public interface IPizzaOrderService
{
    Task<decimal> CalculatePriceFromUnfinishedPizza(UnfinishedPizzaDto unfinishedPizza);
    Task<PizzaOrderDto?> CreatePizzaOrderAsync(UnfinishedPizzaDto unfinishedPizza);
    Task<List<PizzaOrderDto>> GetAllPizzaOrdersAsync();
    Task<List<PizzaSize>> GetAllPizzaSizesAsync();
    Task<List<Topping>> GetAllToppingsAsync();
}

public class PizzaOrderService : IPizzaOrderService
{
    private readonly IPizzaOrderRepository _pizzaOrderRepository;

    private readonly IPizzaSizeRepository _pizzaSizeRepository;

    private readonly IToppingsRepository _toppingsRepository;

    public PizzaOrderService(IPizzaOrderRepository pizzaOrderRepository, IPizzaSizeRepository pizzaSizeRepository, IToppingsRepository toppingsRepository)
    {
        _pizzaOrderRepository = pizzaOrderRepository;
        _pizzaSizeRepository = pizzaSizeRepository;
        _toppingsRepository = toppingsRepository;
    }

    public async Task<List<Topping>> GetAllToppingsAsync()
    {
        return await _toppingsRepository.GetAllToppingsAsync();
    }
    public async Task<List<PizzaSize>> GetAllPizzaSizesAsync()
    {
        return await _pizzaSizeRepository.GetAllPizzaSizesAsync();
    }
    public async Task<List<PizzaOrderDto>> GetAllPizzaOrdersAsync()
    {
        var pizzas = await _pizzaOrderRepository.GetAllPizzaOrdersAsync();

        var pizzaOrdersDtos = pizzas.Select(p => new PizzaOrderDto
        {
            Id = p.Id,
            TotalPrice = p.TotalPrice,
            Size = p.Size.Size,
            Toppings = p.PizzaOrderToppings.Select(pt => pt.Topping.Name).ToList(),
            CreationDate = p.CreationDate
        }).ToList();

        return pizzaOrdersDtos;
    }

    public async Task<PizzaOrderDto?> CreatePizzaOrderAsync(UnfinishedPizzaDto unfinishedPizza)
    {
        var pizzaSize = await _pizzaSizeRepository.GetSpecificPizzaSize(unfinishedPizza.Size);

        if (pizzaSize == null)
        {
            return null;
        }

        var toppings = await _toppingsRepository.GetSpecificToppingsAsync(unfinishedPizza.ToppingIds);

        decimal currentPrice = CalculatePrice(pizzaSize, toppings);

        var pizzaOrder = new PizzaOrder
        {
            Size = pizzaSize,
            TotalPrice = currentPrice,
            CreationDate = DateTime.UtcNow
        };

        foreach (var topping in toppings)
        {
            pizzaOrder.PizzaOrderToppings.Add(new PizzaOrderTopping
            {
                Topping = topping
            });
        }

        await _pizzaOrderRepository.CreatePizzaOrderAsync(pizzaOrder);

        var pizzaOrderDto = new PizzaOrderDto
        {
            Id = pizzaOrder.Id,
            TotalPrice = pizzaOrder.TotalPrice,
            Size = pizzaOrder.Size.Size,
            Toppings = pizzaOrder.PizzaOrderToppings.Select(pt => pt.Topping.Name).ToList(),
            CreationDate = pizzaOrder.CreationDate
        };

        return pizzaOrderDto;
    }
    public async Task<decimal> CalculatePriceFromUnfinishedPizza(UnfinishedPizzaDto unfinishedPizza)
    {
        var pizzaSize = await _pizzaSizeRepository.GetSpecificPizzaSize(unfinishedPizza.Size);

        if (pizzaSize == null)
        {
            return -1;
        }

        var toppings = await _toppingsRepository.GetSpecificToppingsAsync(unfinishedPizza.ToppingIds);

        decimal currentPrice = CalculatePrice(pizzaSize, toppings);

        return currentPrice;

    }
    private decimal CalculatePrice(PizzaSize pizzaSize, List<Topping> toppings)
    {
        decimal currentPrice = 0;

        currentPrice += pizzaSize.Price;

        foreach (var topping in toppings)
        {
            currentPrice += topping.Price;
        }

        return currentPrice;
    }
}
