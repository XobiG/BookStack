using BookStack.Server.Catalog.Data;
using BookStack.Shared;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// Migrationen beim Start automatisch anwenden
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseCors();

app.MapGet("/api/books", async (AppDbContext db) =>
    await db.Books.ToListAsync())
   .WithName("GetBooks");

app.MapGet("/api/books/{id}", async (int id, AppDbContext db) =>
    await db.Books.FindAsync(id) is BookDto book
        ? Results.Ok(book)
        : Results.NotFound())
   .WithName("GetBookById");

app.Run();
