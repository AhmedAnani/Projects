using System;
using System.Collections.Generic;
using System.IO;
using AnimalShelter.src.Models;


namespace AnimalShelter.src.Repos
{
    // This class only handles reading and writing animals to a text file.
    public class ShelterFileHandler
    {
        private readonly string _filePath;

        private const char SEP = '|';

        public ShelterFileHandler(string filePath)
        {
            _filePath = filePath;
        }

        public void SaveAll(IReadOnlyList<Animal> animals)
        {
            try
            {
                string fullPath = Path.GetFullPath(_filePath);
                string dir = Path.GetDirectoryName(fullPath);

                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                using var writer = new StreamWriter(fullPath, append: false);

                foreach (var animal in animals)
                {
                    // Build the shared base fields common to every animal type
                    string line = $"{animal.GetType().Name}{SEP}" +
                                  $"{animal.Id}{SEP}" +
                                  $"{animal.Name}{SEP}" +
                                  $"{animal.Age}{SEP}" +
                                  $"{animal.Status}{SEP}" +
                                  $"{animal.AdopterName}{SEP}";

                    // Append the subclass-specific extra fields
                    line += animal switch
                    {
                        Dog d => $"{d.Breed}{SEP}{d.IsVaccinated}{SEP}{d.Size}",
                        Cat c => $"{c.Color}{SEP}{c.IsIndoor}{SEP}{c.IsVaccinated}",
                        Bird b => $"{b.Species}{SEP}{b.CanFly}{SEP}{b.WingSpan}",
                        SmallAnimal s => $"{s.AnimalType}{SEP}{s.Habitat}{SEP}{s.IsNocturnal}",
                        _ => throw new NotSupportedException($"Unknown type: {animal.GetType().Name}")
                    };

                    writer.WriteLine(line);
                }
                
                Console.ForegroundColor = ConsoleColor.Blue;
                //Console.WriteLine($"\n[FILE] {animals.Count} record(s) saved to {fullPath}");
                Console.ResetColor();
            }
            catch (IOException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[FILE ERROR] Could not save: {ex.Message}");
                Console.ResetColor();
            }
        }


        // Reads the file line by line and rebuilds Animal objects.
        // Returns an empty list if the file does not exist yet.
        public List<Animal> LoadAll()
        {
            var result = new List<Animal>();

            string fullPath = Path.GetFullPath(_filePath);

            if (!File.Exists(fullPath))
                return result;

            try
            {
                using var reader = new StreamReader(fullPath);
                string line;
                int lineNumber = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    try
                    {
                        string[] p = line.Split(SEP);

                        // Shared fields: [0]=type [1]=id [2]=name [3]=age [4]=status [5]=adopterName
                        string typeName = p[0];
                        int id = int.Parse(p[1]);
                        string name = p[2];
                        int age = int.Parse(p[3]);
                        var status = Enum.Parse<AnimalStatus>(p[4]);
                        string adopterName = p[5];

                        // Subclass-specific fields start at index 6
                        Animal animal = typeName switch
                        {
                            "Dog" => new Dog(
                                id, name, age,
                                breed: p[6],
                                isVaccinated: bool.Parse(p[7]),
                                size: p[8],
                                status: status),

                            "Cat" => new Cat(
                                id, name, age,
                                isIndoor: bool.Parse(p[7]),
                                color: p[6],
                                isVaccinated: bool.Parse(p[8]),
                                status: status),

                            "Bird" => new Bird(
                                id, name, age,
                                species: p[6],
                                canFly: bool.Parse(p[7]),
                                wingspan: p[8],
                                status: status),

                            "SmallAnimal" => new SmallAnimal(
                                id, name, age,
                                animalType: p[6],
                                habitat: p[7],
                                isNocturnal: bool.Parse(p[8]),
                                status: status),

                            _ => throw new NotSupportedException($"Unknown type '{typeName}'")
                        };

                        if (status == AnimalStatus.Adopted && !string.IsNullOrWhiteSpace(adopterName))
                            animal.Adopt(adopterName);

                        result.Add(animal);
                    }
                    catch (Exception ex)
                    {
                        // Skip bad lines
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"[FILE WARN] Skipping line {lineNumber}: {ex.Message}");
                        Console.ResetColor();
                    }
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"[FILE] {result.Count} record(s) loaded from {fullPath}");
                Console.ResetColor();
            }
            catch (IOException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[FILE ERROR] Could not load: {ex.Message}");
                Console.ResetColor();
            }

            return result;
        }
    }
}