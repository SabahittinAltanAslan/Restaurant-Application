using Microsoft.EntityFrameworkCore;
using MinimalAPI.Data;
using MinimalAPI.Data.Context;
using MinimalAPI.Data.Entities;

namespace MinimalAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            // API Endpoints for Products
            app.MapGet("/products", async (ApplicationDbContext db) => await db.Products.ToListAsync());
            app.MapGet("/products/{id}", async (ApplicationDbContext db, int id) =>
            {
                var product = await db.Products.FindAsync(id);
                return product is not null ? Results.Ok(product) : Results.NotFound();
            });
            app.MapPost("/products", async (ApplicationDbContext db, Product product) =>
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return Results.Created($"/products/{product.Id}", product);
            });
            app.MapPut("/products/{id}", async (ApplicationDbContext db, int id, Product updatedProduct) =>
            {
                var product = await db.Products.FindAsync(id);
                if (product is null) return Results.NotFound();

                product.Name = updatedProduct.Name;
                product.Price = updatedProduct.Price;
                product.CategoryId = updatedProduct.CategoryId;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            app.MapDelete("/products/{id}", async (ApplicationDbContext db, int id) =>
            {
                var product = await db.Products.FindAsync(id);
                if (product is null) return Results.NotFound();

                db.Products.Remove(product);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            // API Endpoints for Categories
            app.MapGet("/categories", async (ApplicationDbContext db) => await db.Categories.ToListAsync());
            app.MapGet("/categories/{id}", async (ApplicationDbContext db, int id) =>
            {
                var category = await db.Categories.FindAsync(id);
                return category is not null ? Results.Ok(category) : Results.NotFound();
            });
            app.MapPost("/categories", async (ApplicationDbContext db, Category category) =>
            {
                db.Categories.Add(category);
                await db.SaveChangesAsync();
                return Results.Created($"/categories/{category.Id}", category);
            });
            app.MapPut("/categories/{id}", async (ApplicationDbContext db, int id, Category updatedCategory) =>
            {
                var category = await db.Categories.FindAsync(id);
                if (category is null) return Results.NotFound();

                category.Name = updatedCategory.Name;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            app.MapDelete("/categories/{id}", async (ApplicationDbContext db, int id) =>
            {
                var category = await db.Categories.FindAsync(id);
                if (category is null) return Results.NotFound();

                db.Categories.Remove(category);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            // API Endpoints for Orders
            app.MapGet("/orders", async (ApplicationDbContext db) => await db.Orders.Include(o => o.Product).ToListAsync());
            app.MapGet("/orders/{id}", async (ApplicationDbContext db, int id) =>
            {
                var order = await db.Orders.Include(o => o.Product).FirstOrDefaultAsync(o => o.Id == id);
                return order is not null ? Results.Ok(order) : Results.NotFound();
            });
            app.MapPost("/orders", async (ApplicationDbContext db, Order order) =>
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return Results.Created($"/orders/{order.Id}", order);
            });
            app.MapPut("/orders/{id}", async (ApplicationDbContext db, int id, Order updatedOrder) =>
            {
                var order = await db.Orders.FindAsync(id);
                if (order is null) return Results.NotFound();

                order.ProductId = updatedOrder.ProductId;
                order.OrderDate = updatedOrder.OrderDate;
                order.UserName = updatedOrder.UserName;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });
            app.MapDelete("/orders/{id}", async (ApplicationDbContext db, int id) =>
            {
                var order = await db.Orders.FindAsync(id);
                if (order is null) return Results.NotFound();

                db.Orders.Remove(order);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            app.Run();
        }
    }
}
