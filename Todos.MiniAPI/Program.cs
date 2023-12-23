using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Todos.Data.Contexts;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<TodosDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TodoConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
        // c.RoutePrefix = string.Empty; // Om du vill öppna Swagger UI på rotvägen
    }
    );
}


app.UseHttpsRedirection();

app.UseRouting();

//app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();