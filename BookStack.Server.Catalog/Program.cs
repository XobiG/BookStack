using BookStack.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();

var books = new List<BookDto>
{
    new() { Id = 1, Title = "Der Prozess",  Author = "Franz Kafka",       ISBN = "978-3-16-148410-0", Genre = "Roman", Stock = 5 },
    new() { Id = 2, Title = "Faust",        Author = "Johann W. Goethe",  ISBN = "978-3-16-148410-1", Genre = "Drama", Stock = 3 },
    new() { Id = 3, Title = "Effi Briest",  Author = "Theodor Fontane",   ISBN = "978-3-16-148410-2", Genre = "Roman", Stock = 2 },
};

app.MapGet("/api/books", () => books)
   .WithName("GetBooks");

app.MapGet("/api/books/{id}", (int id) =>
    books.FirstOrDefault(b => b.Id == id) is BookDto book
        ? Results.Ok(book)
        : Results.NotFound())
   .WithName("GetBookById");

app.Run();
