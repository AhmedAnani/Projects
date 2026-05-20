using AnimalShelter.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimalShelter.src.UI
{
    public static class AnimalDisplay
    {
        //List all animals
        public static void ShowAll(IReadOnlyList<Animal> animals)
        {
            Console.WriteLine("\n--- All Animals ---");
            if (!animals.Any())
            {
                Console.WriteLine("No animals currently in the shelter.");
                return;
            }

            foreach (var animal in animals.OrderBy(a => a.Id))
            {
                Console.WriteLine($"ID: {animal.Id} | Name: {animal.Name} | Age: {animal.Age} | Status: {animal.Status} | {animal.GetSpeciesInfo()}");
            }
            Console.WriteLine("-------------------\n");
        }

        public static void ShowDetail(Animal animal)
        {
            if (animal == null) throw new ArgumentNullException(nameof(animal));


            Console.WriteLine($"\n--- Details for {animal.Name} ---");
            Console.WriteLine($"  ID         : {animal.Id}");
            Console.WriteLine($"  Age        : {animal.Age}");
            Console.WriteLine($"  Status     : {animal.Status}");
            Console.WriteLine($"  Intake     : {animal.IntakeDate:yyyy-MM-dd}");
            Console.WriteLine($"  Species    : {animal.GetSpeciesInfo()}");  
            Console.WriteLine($"  Adoption   : {animal.GetAdoptionInfo()}");
            Console.WriteLine($"  Care Notes :");

            string history = animal.GetCareHistory();
            if (string.IsNullOrWhiteSpace(history))
            {
                Console.WriteLine("    No care records yet.");
            }
            else
            {
                foreach (var line in history.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                    Console.WriteLine($"    {line.Trim()}");
            }
            Console.WriteLine("---------------------------\n");
        }

        public static void ShowSuccess(string message) { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"\n[SUCCESS] {message}"); Console.ResetColor(); }
        public static void ShowError(string message) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine($"\n[ERROR] {message}\n"); Console.ResetColor(); }
        public static void ShowInfo(string message) { Console.ForegroundColor = ConsoleColor.Blue; Console.WriteLine($"\n[INFO] {message}"); Console.ResetColor(); }
        public static void ShowSectionHeader(string title) { Console.ForegroundColor = ConsoleColor.Cyan; Console.WriteLine($"\n=== {title.ToUpper()} ==="); Console.ResetColor(); }
    }
}
