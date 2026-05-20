using Animal_Shelter_V2.GlobalFiles;
using Animal_Shelter_V2.src.Models.implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal_Shelter_V2.src.Repositories.Interfaces
{
    public interface IAnimalRepository
    {
        int Count { get; }
        int NextId();

        void Add(Animal animal);
        bool Remove(int id);

        Animal FindById(int id);
        List<Animal> FindByName(string name);
        List<Animal> FindByStatus(AnimalStatus status);

        void Adopt(int id, string adopterName);
        void AddCareNote(int id, string note);

        void UpdateStatus(int id, AnimalStatus newStatus);

        IReadOnlyList<Animal> GetAll();
    }
}
