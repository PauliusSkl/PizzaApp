namespace PizzaApp.Data.Entities;

public class PizzaOrderTopping
{
    public Guid PizzaOrderId { get; set; }
    public PizzaOrder PizzaOrder { get; set; }
    public int ToppingId { get; set; }
    public Topping Topping { get; set; }
}
