using Animal_Shelter_V2.GlobalFiles;
using Animal_Shelter_V2.src.Models.implementation;
using Animal_Shelter_V2.src.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Animal_Shelter_V2.src.Repositories.Implementations
{
    public class AnimalManager : IAnimalRepository
    {
        private readonly List<Animal> _animals;
        private int _nextId;

        public AnimalManager()
        {
            _animals = new List<Animal>();
            _nextId = 1;
        }

        //IAnimalRepository implementation
        public int Count => _animals.Count;

        public int NextId() => _nextId;

        public void Add(Animal animal)
        {
            if (animal == null)
                throw new ArgumentNullException(nameof(animal));
            if (_animals.Any(a => a.Id == animal.Id))
                throw new InvalidOperationException(
                    $"An animal with ID {animal.Id} already exists.");
            _animals.Add(animal);

            // Ensure _nextId is always greater than any existing ID
            if (animal.Id >= _nextId)
            {
                _nextId = animal.Id + 1;
            }
        }

        public bool Remove(int id)
        {
            var animal = FindById(id);
            if (animal == null) 
                return false;
            _animals.Remove(animal);
            return true;
        }

        public Animal FindById(int id)
            => _animals.FirstOrDefault(a => a.Id == id);

        public List<Animal> FindByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Cannot be empty.", nameof(name));
            return _animals
                .Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        public List<Animal> FindByStatus(AnimalStatus status)
            => _animals.Where(a => a.Status == status).ToList();

        public void UpdateStatus(int id, AnimalStatus newStatus)
        {
            var animal = FindById(id);
            if (animal == null)
                throw new Exception("Animal not found");
            animal.Status = newStatus;
        }

        public void Adopt(int id, string adopterName)
        {
            var animal = FindById(id);
            if (animal == null)
                throw new Exception("Animal not found");

            animal.Adopt(adopterName);
            
        }

        public void AddCareNote(int id, string note)
        {
            var animal = FindById(id);
            if (animal == null)
                throw new Exception("Animal not found");

            animal.AddCareNote(note);
            
        }

        public IReadOnlyList<Animal> GetAll()
            => _animals.OrderBy(a => a.Id).ToList().AsReadOnly();
    }
}
