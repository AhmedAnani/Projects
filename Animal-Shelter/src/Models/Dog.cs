using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelter.src.Models
{
    public sealed class Dog : Animal
    {

        private string _breed;
        private string _size;

        public string Breed
        {
            get => _breed; 
            set => _breed = string.IsNullOrWhiteSpace(value)? "Unknown" : value.Trim();
        }
        public string Size
        {
            get => _size;
            set => _size = string.IsNullOrEmpty(value) ? "Medium" : value.Trim();
        }
        public bool IsVaccinated { get; set; }
        
        public Dog(int id, string name, int age,string breed,bool isVaccinated,string size, AnimalStatus status = AnimalStatus.Available)
            : base(id, name, age, status)
        {
            Breed = breed;
            Size = size;
            IsVaccinated = isVaccinated;
        }

        public override string GetSpeciesInfo()
            => $"Dog | Breed: {Breed} | Size: {Size} | Vaccinated: {(IsVaccinated?"Yes":"No")}";
    }
}
