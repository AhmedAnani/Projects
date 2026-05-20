using AnimalShelter.src.Models;
using System.Collections.Generic;


namespace animal_Shelter.Services
{
    public interface IAnimalService
    {
        void AddAnimal(int type);
        bool RemoveAnimal(int id);

        Animal GetAnimal(int id);
        List<Animal> GetAllAnimals();
        
        List<Animal> GetByStatus(AnimalStatus status);
        List<Animal> SearchByName(string name);

        void AdoptAnimal(int id, string adopterName);
        void AddCareNote(int id, string note);

        // Needed for non-adoption status changes routed through the service layer
        void UpdateStatus(int id, AnimalStatus newStatus);
    }
}
