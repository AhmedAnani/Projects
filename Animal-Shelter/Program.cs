using animal_Shelter.Repos;
using animal_Shelter.Services;
using AnimalShelter.src.Controllers;
using AnimalShelter.src.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

// Dynamically calculates the path: 
// 1. Gets the current running directory (bin/Debug/netX.0/)
// 2. Uses @"..\..\..\" to step up 3 folder levels to your project root
// 3. Combines it with the data folder and file name
 string DataFile = Path.GetFullPath(
    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\data\shelter_data.txt")
);



 void Main(string[] args)
{
    Console.Title = "Animal Shelter Management System";


    var fileHandler = new ShelterFileHandler(DataFile);

    IAnimalRepository repoFile = new AnimalFileRepository(fileHandler);
    IAnimalService service = new AnimalService(repoFile);
    var controller = new AppController(service);
    controller.Run();

}

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
