using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BooksConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/books", async ([FromServices] DataContext context) =>
{
    var books = await context.Books.ToListAsync(); // Assuming you're using Entity Framework Core
    return Results.Ok(books);
});

app.MapPost("/api/books", async (string title, [FromServices] DataContext context) =>
{
    Book book = new Book
    {
        Title = title,
        PublisherId = 0
    };
    context.Books.Add(book);
    await context.SaveChangesAsync();
    return Results.Created($"/api/books/{book.Id}", book);
});
app.MapPut("/api/books/{id}", async (int id, string title, [FromServices] DataContext context) =>
{
    // Hämta boken från databasen baserat på det angivna ID:t
    var existingBook = await context.Books.FindAsync(id);

    if (existingBook == null)
    {
        return Results.NotFound("Book not found");
    }

    // Uppdatera bokens egenskaper med de nya värdena
    existingBook.Title = title;
    // Uppdatera andra egenskaper för boken här vid behov

    // Spara ändringar i databasen
    await context.SaveChangesAsync();

    // Returnera en "200 OK" HTTP-respons för att indikera att boken har uppdaterats
    return Results.Ok(existingBook);
});
app.MapDelete("/api/books/{id}", async (int id, [FromServices] DataContext context) =>
{
    // Hämta boken från databasen baserat på det angivna ID:t
    var bookToDelete = await context.Books.FindAsync(id);

    if (bookToDelete == null)
    {
        return Results.NotFound("Book not found");
    }

    // Ta bort boken från databasen
    context.Books.Remove(bookToDelete);

    // Spara ändringar i databasen
    await context.SaveChangesAsync();

    // Returnera en "204 No Content" HTTP-respons för att indikera att boken har tagits bort
    return Results.NoContent();
});



app.Run();

