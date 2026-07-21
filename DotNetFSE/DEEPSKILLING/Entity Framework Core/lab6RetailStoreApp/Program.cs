using Microsoft.EntityFrameworkCore;
using RetailStoreApp.Data;
using RetailStoreApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    context.Database.EnsureCreated();

    if (!context.Categories.Any())
    {
        var electronics = new Category { Name = "Electronics" };
        var groceries = new Category { Name = "Groceries" };

        context.Categories.AddRange(electronics, groceries);

        context.Products.AddRange(
            new Product
            {
                Name = "Laptop",
                Price = 75000,
                Category = electronics
            },
            new Product
            {
                Name = "Rice Bag",
                Price = 1200,
                Category = groceries
            });

        context.SaveChanges();
    }

    // Update Product
    var product = await context.Products
        .FirstOrDefaultAsync(p => p.Name == "Laptop");

    if (product != null)
    {
        product.Price = 70000;
        await context.SaveChangesAsync();

        Console.WriteLine("Product Updated Successfully");
    }

    // Delete Product
    var toDelete = await context.Products
        .FirstOrDefaultAsync(p => p.Name == "Rice Bag");

    if (toDelete != null)
    {
        context.Products.Remove(toDelete);
        await context.SaveChangesAsync();

        Console.WriteLine("Product Deleted Successfully");
    }

    Console.WriteLine("\nRemaining Products:");

    var products = await context.Products.ToListAsync();

    foreach (var p in products)
    {
        Console.WriteLine($"{p.Name} - ₹{p.Price}");
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();