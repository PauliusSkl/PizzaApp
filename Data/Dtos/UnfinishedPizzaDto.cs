namespace PizzaApp.Data.Models;

public record UnfinishedPizzaDto
{
    public int Size { get; set; }
    public required List<int> ToppingIds { get; set; }
}
