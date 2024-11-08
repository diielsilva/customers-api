using api.Data;
using api.Middlewares;
using api.Repositories;
using api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add database connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var databaseConnection = builder.Configuration.GetConnectionString("DatabaseConnection");

    options.UseMySql(databaseConnection, ServerVersion.AutoDetect(databaseConnection));
});

builder.Services.AddControllers();

//Add repositories
builder.Services.AddScoped(typeof(CustomerRepository));

//Add services
builder.Services.AddScoped(typeof(CustomerService));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Registering the Middleware
app.UseMiddleware(typeof(ExceptionHandler));
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
