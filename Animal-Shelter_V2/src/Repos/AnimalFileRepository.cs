using animal_Shelter.Repos;
using AnimalShelter.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelter.src.Repos
{
    public class AnimalFileRepository : IAnimalRepository
    {
        private readonly List<Animal> _animals = new();
        private readonly ShelterFileHandler _fileHandler;
        private int _nextId = 1;

        public AnimalFileRepository(ShelterFileHandler FileHandler)
        {
            _fileHandler = FileHandler;
            var loaded = _fileHandler.LoadAll();
            _animals.AddRange(loaded);

            if (_animals.Any())
                _nextId = _animals.Max(a => a.Id) + 1;

        }

        public int Count => _animals.Count;

        public int NextId() => _nextId;


        private void Save() => _fileHandler.SaveAll(_animals);


        public void Add(Animal animal)
        {
            if (animal == null)
                throw new ArgumentNullException(nameof(animal));

            if (_animals.Any(a => a.Id == animal.Id))
                throw new InvalidOperationException("Duplicate ID");

            _animals.Add(animal);

            if (animal.Id >= _nextId)
                _nextId = animal.Id + 1;

            Save();
        }

        public bool Remove(int id)
        {
            
            var animal = FindById(id);
            if (animal == null) return false;

            _animals.Remove(animal);
            Save();
            return true;
        }

        public void UpdateStatus(int id, AnimalStatus newStatus)
        {
            var animal = FindById(id);
            if (animal == null)
                throw new Exception("Animal not found");

            animal.Status = newStatus;
            animal.AddCareNote($"Status changed to {newStatus}.");

            Save();
        }

        public Animal FindById(int id)
           => _animals.FirstOrDefault(a => a.Id == id);

        public List<Animal> FindByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty");

            return _animals
                .Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Animal> FindByStatus(AnimalStatus status)
            => _animals.Where(a => a.Status == status).ToList();

        public void Adopt(int id, string adopterName)
        {
            var animal = FindById(id);
            if (animal == null)
                throw new Exception("Animal not found");

            animal.Adopt(adopterName);
            Save(); 
        }

        public void AddCareNote(int id, string note)
        {
            var animal = FindById(id);
            if (animal == null)
                throw new Exception("Animal not found");

            animal.AddCareNote(note);
            Save(); 
        }

        public IReadOnlyList<Animal> GetAll()
            => _animals.OrderBy(a => a.Id).ToList().AsReadOnly();

    }
}
