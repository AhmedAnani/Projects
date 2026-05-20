using System;
using System.Collections.Generic;
using System.Text;

namespace Animal_Shelter_V2.src.Controllers.Interfaces
{
    internal interface IAppController
    {
        private static void PrintMenu();

        public void Run();
        private void AddAnimal();
        private void ViewAllAnimals();
        private void UpdateAnimalStatus();
        private void RemoveAnimal();
    }
}
