using System.ComponentModel.DataAnnotations;

namespace PizzaApp.Data.Entities
{
    public class PizzaSize
    {
        [Key]
        public int Id { get; set; }
        public required string Size { get; set; }
        public decimal Price { get; set; }
    }
}
