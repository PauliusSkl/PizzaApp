using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Data.Entities;

public class PizzaOrder
{
    [Key]
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; }
    public PizzaSize Size { get; set; }
    public List<PizzaOrderTopping> PizzaOrderToppings { get; set; } = new();

    public DateTime CreationDate { get; set; }
}
