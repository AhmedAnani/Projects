using animal_Shelter.Repos;
using animal_Shelter.UI;
using AnimalShelter.src.Models;
using AnimalShelter.src.UI;
using animal_Shelter.Services;
using System;
namespace AnimalShelter.src.Controllers
{
    public sealed class AppController
    {
        private readonly IAnimalService _service;

        // The repository is injected, but we immediately wrap it in our Service layer.
        // AppController always talks to IAnimalService — never to the repo directly.

        public AppController(IAnimalService service)
        {
            _service = service;
        }

        private static void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  ┌──────────────────────────────────┐");
            Console.WriteLine("  │         MAIN MENU                │");
            Console.WriteLine("  ├──────────────────────────────────┤");
            Console.WriteLine("  │  1. Add a new animal             │");
            Console.WriteLine("  │  2. View all animals             │");
            Console.WriteLine("  │  3. Search by name / ID / Status │");
            Console.WriteLine("  │  4. Update an animal's status    │");
            Console.WriteLine("  │  5. Remove an animal             │");
            Console.WriteLine("  │  6. Save and Exit                │");
            Console.WriteLine("  └──────────────────────────────────┘");
            Console.ResetColor();
            Console.WriteLine();
        }

        // Entry point called by Program 
        public void Run()
        {
            int choice;
            do
            {
                PrintMenu();
                choice = ConsoleInput.ReadInt("Your choice: ", 1, 6);

                switch (choice)
                {
                    case 1: AddAnimal(); break;
                    case 2: ViewAllAnimals(); break;
                    case 3: SearchAnimals(); break;
                    case 4: UpdateAnimalStatus(); break;
                    case 5: RemoveAnimal(); break;
                    case 6: Console.WriteLine("\nGoodbye!\n"); break;
                }
            }
            while (choice != 6);
        }

        // MENU ACTIONS
        private void AddAnimal()
        {
            AnimalDisplay.ShowSectionHeader("Add a New Animal");
            Console.WriteLine("1. Dog  |  2. Cat  |  3. Bird  |  4. Small Animal");

            int type = ConsoleInput.ReadInt("Animal type (1-4): ", 1, 4);
            try
            {
                _service.AddAnimal(type);
                AnimalDisplay.ShowSuccess("Animal added successfully");
            }
            catch (Exception ex)
            {
                AnimalDisplay.ShowError(ex.Message);
            }
        }

        private void ViewAllAnimals()
        {
            AnimalDisplay.ShowAll(_service.GetAllAnimals());
        }

        private void SearchAnimals()
        {
            AnimalDisplay.ShowSectionHeader("Search Animals");
            Console.WriteLine("1. Name  |  2. ID  |  3. Status");

            int searchType = ConsoleInput.ReadInt("Search type (1-3): ", 1, 3);

            try
            {
                switch (searchType)
                {
                    case 1:
                        string name = ConsoleInput.ReadString("Name: ");
                        var results = _service.SearchByName(name);
                        foreach (var a in results)
                           AnimalDisplay.ShowDetail(a);
                        break;
                    case 2:
                        int id = ConsoleInput.ReadInt("ID: ", 1, int.MaxValue);
                        Animal animal = _service.GetAnimal(id);
                        AnimalDisplay.ShowDetail(animal);
                        break;
                    case 3:
                        Console.WriteLine();
                        var statusValues = Enum.GetValues<AnimalStatus>();
                        foreach (AnimalStatus s in statusValues)
                            Console.WriteLine($"  {(int)s}. {s}");

                        int statusInt = ConsoleInput.ReadInt(
                            $"Status (0-{statusValues.Length - 1}): ",
                            0, statusValues.Length - 1);

                        var statusResults = _service.GetByStatus((AnimalStatus)statusInt);
                        foreach (var a in statusResults)
                            AnimalDisplay.ShowDetail(a);
                        break;
                }
            }
            catch (Exception ex) { AnimalDisplay.ShowError(ex.Message); }
        }

        private void UpdateAnimalStatus()
        {
            AnimalDisplay.ShowSectionHeader("Update Animal Status");
            AnimalDisplay.ShowAll(_service.GetAllAnimals());
            try
            {
                int id = ConsoleInput.ReadInt("Animal ID to update: ", 1, int.MaxValue);
                Animal animal = _service.GetAnimal(id);

                Console.WriteLine($"\n  {animal.Name} — current status: {animal.Status}\n");

                var statusValues = Enum.GetValues<AnimalStatus>();
                foreach (AnimalStatus s in statusValues)
                    Console.WriteLine($"  {(int)s}. {s}");

                int newStatusInt = ConsoleInput.ReadInt(
                    $"New status (0-{statusValues.Length - 1}): ",
                    0, statusValues.Length - 1);

                var newStatus = (AnimalStatus)newStatusInt;

                if (newStatus == AnimalStatus.Adopted)
                {
                    string adopterName = ConsoleInput.ReadString("Adopter name: ");
                    _service.AdoptAnimal(id, adopterName);
                }
                else
                {
                    _service.UpdateStatus(id, newStatus);
                    //_service.AddCareNote(id, $"Status changed to {newStatus}.");
                }
                var updatedAnimal = _service.GetAnimal(id);


                AnimalDisplay.ShowSuccess($"{updatedAnimal.Name} status updated to {updatedAnimal.Status}.");
            }
            catch (Exception ex) { AnimalDisplay.ShowError(ex.Message); }
        }

        private void RemoveAnimal()
        {
            AnimalDisplay.ShowSectionHeader("Remove an Animal");
            AnimalDisplay.ShowAll(_service.GetAllAnimals());

            int id = ConsoleInput.ReadInt("Animal ID to remove (0 to cancel): ", 0, int.MaxValue);

            if (id == 0)
            {
                AnimalDisplay.ShowInfo("Cancelled.");
                return;
            }

            try
            {
                Animal animal = _service.GetAnimal(id);
                string confirm = ConsoleInput.ReadString($"Remove {animal.Name}? (y/n): ");

                if (confirm != "y" && confirm != "yes")
                {
                    AnimalDisplay.ShowInfo("Cancelled.");
                    return;
                }

                if (_service.RemoveAnimal(id))
                    AnimalDisplay.ShowSuccess($"{animal.Name} removed.");
            }
            catch (Exception ex)
            {
                AnimalDisplay.ShowError(ex.Message);
            }
        }
    }
}