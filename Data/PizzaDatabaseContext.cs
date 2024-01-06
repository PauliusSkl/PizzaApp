using Microsoft.EntityFrameworkCore;
using PizzaApp.Data.Entities;

namespace PizzaApp.Data;

public class PizzaDatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("PizzaDatabase");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Topping>().HasData(
            new Topping { Id = 1, Name = "Pepperoni", Price = 1 },
            new Topping { Id = 2, Name = "Mushrooms", Price = 1 },
            new Topping { Id = 3, Name = "Onions", Price = 1 },
            new Topping { Id = 4, Name = "Sausage", Price = 1 },
            new Topping { Id = 5, Name = "Bacon", Price = 1 },
            new Topping { Id = 6, Name = "Extra cheese", Price = 1 },
            new Topping { Id = 7, Name = "Black olives", Price = 1 }
        );

        modelBuilder.Entity<PizzaSize>().HasData(
            new PizzaSize { Id = 1, Size = "Small", Price = 8 },
            new PizzaSize { Id = 2, Size = "Medium", Price = 10 },
            new PizzaSize { Id = 3, Size = "Large", Price = 12 }
        );

        modelBuilder.Entity<PizzaOrderTopping>()
       .HasKey(pt => new { pt.PizzaOrderId, pt.ToppingId });
    }

    public DbSet<PizzaOrder> Pizzas { get; set; }

    public DbSet<Topping> Toppings { get; set; }

    public DbSet<PizzaSize> PizzaSizes { get; set; }

    public DbSet<PizzaOrderTopping> PizzaOrderToppings { get; set; }
}
