using animal_Shelter.Factory;
using animal_Shelter.Repos;
using AnimalShelter.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace animal_Shelter.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _repo;

        public AnimalService(IAnimalRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public void AddAnimal(int type)
        {
            Animal animal = type switch
            {
                1 => AnimalFactory.BuildDog(_repo.NextId()),
                2 => AnimalFactory.BuildCat(_repo.NextId()),
                3 => AnimalFactory.BuildBird(_repo.NextId()),
                4 => AnimalFactory.BuildSmallAnimal(_repo.NextId()),
                _ => throw new InvalidOperationException("Invalid type")
            };

            _repo.Add(animal);
        }

        public bool RemoveAnimal(int id)
        {
            var animal = _repo.FindById(id);

            if (animal == null)
                throw new Exception("Animal not found");

            return _repo.Remove(id);
        }

        public Animal GetAnimal(int id)
        {
            var animal = _repo.FindById(id);
            if (animal == null)
                throw new Exception("Animal not found");
            return animal;
        }

        public List<Animal> GetAllAnimals()
        {
            return _repo.GetAll().ToList();
        }

        public List<Animal> SearchByName(string name)
        {
            return _repo.FindByName(name);
        }

        public List<Animal> GetByStatus(AnimalStatus status)
        {
            return _repo.FindByStatus(status);
        }

        public void AdoptAnimal(int id, string adopterName)
        {
            if (string.IsNullOrWhiteSpace(adopterName))
                throw new Exception("Adopter name is required");
            //var animal = GetAnimal(id);
            _repo.Adopt(id,adopterName);
        }

        public void AddCareNote(int id, string note)
        {
            if (string.IsNullOrWhiteSpace(note))
                throw new Exception("Note cannot be empty");
            //var animal = GetAnimal(id);
            _repo.AddCareNote(id,note);
        }

        public void UpdateStatus(int id, AnimalStatus newStatus)
        {
            //var animal = GetAnimal(id);
            _repo.UpdateStatus(id, newStatus);
            //_repo.AddCareNote(id,$"Status changed to {newStatus}.");
        }
    }
}
