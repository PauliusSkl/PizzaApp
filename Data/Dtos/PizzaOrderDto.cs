namespace PizzaApp.Data.Dtos
{
    public record PizzaOrderDto
    {

        public Guid Id { get; set; }
        public decimal TotalPrice { get; set; }
        public required string Size { get; set; }
        public required List<string> Toppings { get; set; }
    }
}
