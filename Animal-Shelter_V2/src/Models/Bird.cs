using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelter.src.Models
{
    public sealed class Bird : Animal
    {
        private string _species;
        private string _wingspan;

        public string Species
        {
            get => _species;
            set => _species = string.IsNullOrWhiteSpace(value) ? "Unknown" : value.Trim();
        }
        public string WingSpan
        {
            get => _wingspan;
            set => _wingspan = string.IsNullOrWhiteSpace(value) ? "Medium" : value.Trim();
        }

        public bool CanFly { get; set; }


        public Bird(int id, string name, int age,string species,bool canFly,string wingspan, AnimalStatus status = AnimalStatus.Available) 
            : base(id, name, age, status)
        {
            Species = species;
            CanFly = canFly;
            WingSpan = wingspan;
        }

        public override string GetSpeciesInfo()
            => $"Bird | Species: {Species} | Can Fly: {(CanFly ? "Yes" : "No")} | Wingspan: {WingSpan}";
    }
}
