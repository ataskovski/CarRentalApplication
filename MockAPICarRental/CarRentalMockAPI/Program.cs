using CarRentalMockAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure port - use environment variable for cloud deployment, fallback to 5000 for local
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowAll");

// Car Rental API Endpoints
// Get all cars
app.MapGet("/api/cars", (ICarService carService) =>
{
    var cars = carService.GetAllCars();
    return Results.Ok(cars);
})
.WithName("GetAllCars")
.WithOpenApi();

// Get car by ID
app.MapGet("/api/cars/{id}", (int id, ICarService carService) =>
{
    var car = carService.GetCarById(id);
    if (car == null)
    {
        return Results.NotFound(new { message = $"Car with ID {id} not found" });
    }
    return Results.Ok(car);
})
.WithName("GetCarById")
.WithOpenApi();

// Get available cars
app.MapGet("/api/cars/available/true", (ICarService carService) =>
{
    var cars = carService.GetAllCars().Where(c => c.IsAvailable).ToList();
    return Results.Ok(cars);
})
.WithName("GetAvailableCars")
.WithOpenApi();

app.Run();
