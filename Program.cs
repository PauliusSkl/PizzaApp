using PizzaApp.Data;
using PizzaApp.Data.Repositories;
using PizzaApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PizzaDatabaseContext>();

builder.Services.AddTransient<IPizzaOrderService, PizzaOrderService>();

builder.Services.AddTransient<IPizzaOrderRepository, PizzaOrderRepository>();

builder.Services.AddTransient<IPizzaSizeRepository, PizzaSizeRepository>();

builder.Services.AddTransient<IToppingsRepository, ToppingsRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<PizzaDatabaseContext>();
    dbContext.Database.EnsureCreated();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
