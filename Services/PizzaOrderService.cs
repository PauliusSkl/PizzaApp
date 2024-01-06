using PizzaApp.Data.Dtos;
using PizzaApp.Data.Entities;
using PizzaApp.Data.Models;
using PizzaApp.Data.Repositories;

namespace PizzaApp.Services;

public interface IPizzaOrderService
{
    Task<decimal> CalculatePriceFromDto(UnfinishedPizzaDto unfinishedPizza);
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

        return pizzas;
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

        var pizzaOrder = await _pizzaOrderRepository.CreatePizzaOrderAsync(pizzaSize, toppings, currentPrice);

        return pizzaOrder;
    }
    public async Task<decimal> CalculatePriceFromDto(UnfinishedPizzaDto unfinishedPizza)
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
