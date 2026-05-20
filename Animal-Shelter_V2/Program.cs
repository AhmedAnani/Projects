using animal_Shelter.Repos;
using animal_Shelter.Services;
using AnimalShelter.src.Controllers;
using AnimalShelter.src.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//var builder = Host.CreateApplicationBuilder(args);

// Add services to the container.


 //builder.Services.AddDbContext<AppDBContext>(d =>
 //   d.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//var app = builder.Build();

// Dynamically calculates the path: 
// 1. Gets the current running directory (bin/Debug/netX.0/)
// 2. Uses @"..\..\..\" to step up 3 folder levels to your project root
// 3. Combines it with the data folder and file name
 string DataFile = Path.GetFullPath(
    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\data\shelter_data.txt")
);



 
    Console.Title = "Animal Shelter Management System";


    var fileHandler = new ShelterFileHandler(DataFile);

    IAnimalRepository repoFile = new AnimalFileRepository(fileHandler);
    IAnimalService service = new AnimalService(repoFile);
    var controller = new AppController(service);
    controller.Run();



//app.Run();


