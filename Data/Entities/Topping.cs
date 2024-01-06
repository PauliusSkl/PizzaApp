using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Data.Entities
{
    public class Topping
    {

        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; }

    }
}
