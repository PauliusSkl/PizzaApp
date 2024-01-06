using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Data.Entities
{
    public class PizzaOrder
    {
        [Key]
        public Guid Id { get; set; }

        public decimal TotalPrice { get; set; }
        public PizzaSize Size { get; set; }

        //public List<Topping> Toppings { get; set; } = new();

        public List<PizzaOrderTopping> PizzaOrderToppings { get; set; } = new();
    }

}
