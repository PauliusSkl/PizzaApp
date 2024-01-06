using Microsoft.AspNetCore.Mvc;
using PizzaApp.Data.Models;
using PizzaApp.Services;

namespace PizzaApp.Controllers;

[ApiController]
public class PizzaController : ControllerBase
{

    private readonly IPizzaOrderService _pizzaOrderService;

    public PizzaController(IPizzaOrderService pizzaOrderService)
    {
        _pizzaOrderService = pizzaOrderService;
    }



    [HttpGet]
    [Route("api/toppings")]
    public async Task<IActionResult> GetToppings()
    {
        var toppings = await _pizzaOrderService.GetAllToppingsAsync();
        return Ok(toppings);
    }

    [HttpGet]
    [Route("api/pizzaSizes")]
    public async Task<IActionResult> GetPizzaSizes()
    {

        var pizzaSizes = await _pizzaOrderService.GetAllPizzaSizesAsync();
        return Ok(pizzaSizes);
    }

    [HttpGet]
    [Route("api/pizzas")]
    public async Task<IActionResult> GetPizzas()
    {
        var pizzas = await _pizzaOrderService.GetAllPizzaOrdersAsync();
        return Ok(pizzas);
    }




    [HttpPost]
    [Route("api/pizzas")]
    public async Task<IActionResult> CreatePizza(UnfinishedPizzaDto unfinishedPizza)
    {
        var pizza = await _pizzaOrderService.CreatePizzaOrderAsync(unfinishedPizza);

        if (pizza == null)
        {
            return NotFound();
        }

        return Ok(pizza);
    }

    [HttpPost]
    [Route("api/pizzas/current-price")]
    public async Task<IActionResult> GetCurrentPizzaPrice(UnfinishedPizzaDto unfinishedPizza)
    {
        decimal currentPrice = await _pizzaOrderService.CalculatePriceFromDto(unfinishedPizza);

        if (currentPrice == -1)
        {
            return NotFound();
        }

        return Ok(new { CurrentPrice = currentPrice });
    }
}