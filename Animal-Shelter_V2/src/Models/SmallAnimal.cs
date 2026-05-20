using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelter.src.Models
{
    // Represents a small animal (rabbit, hamster, guinea pig, etc.)
    public sealed class SmallAnimal : Animal
    {
        private string _animalType;
        private string _habitat;

        public string AnimalType
        {
            get => _animalType;
            set => _animalType = string.IsNullOrWhiteSpace(value) ? "Unknown" : value.Trim();
        }

        // Habitat refers to the natural environment where the animal lives (e.g., "Desert", "Forest", "Sea", "Savannah").
        public string Habitat
        {
            get => _habitat;
            set => _habitat = string.IsNullOrWhiteSpace(value) ? "Desert" : value.Trim();
        }
        //Nocturnal animals are active during the night and sleep during the day.
        public bool IsNocturnal { get; set; }

        public SmallAnimal(int id, string name, int age, string animalType, string habitat, bool isNocturnal, AnimalStatus status = AnimalStatus.Available)
            : base(id, name, age, status)
        {
            AnimalType = animalType;
            Habitat = habitat;
            IsNocturnal = isNocturnal;
        }
        public override string GetSpeciesInfo()
            => $"Small Animal | Type: {AnimalType} | Habitat: {Habitat} | Nocturnal: {(IsNocturnal ? "Yes" : "No")}";
    }
}
