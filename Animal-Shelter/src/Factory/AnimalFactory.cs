using animal_Shelter.UI;
using AnimalShelter.src.Models;

namespace animal_Shelter.Factory
{
    public class AnimalFactory
    {

        public static Dog BuildDog(int id)
        {
            string name = ConsoleInput.ReadString("Name: ");
            int age = ConsoleInput.ReadInt("Age (0-50): ", 0, 50);
            string breed = ConsoleInput.ReadString("Breed: ");
            bool isVaccinated = ConsoleInput.ReadBool("Vaccinated? (y/n): ");
            string size = ConsoleInput.ReadString("Size (Small/Medium/Large): ");
            return new Dog(id, name, age, breed, isVaccinated, size);
        }

        public static Cat BuildCat(int id)
        {
            string name = ConsoleInput.ReadString("Name: ");
            int age = ConsoleInput.ReadInt("Age (0-50): ", 0, 50);
            bool isIndoor = ConsoleInput.ReadBool("Indoor cat? (y/n): ");
            string color = ConsoleInput.ReadString("Color/pattern: ");
            bool isVaccinated = ConsoleInput.ReadBool("Vaccinated? (y/n): ");
            return new Cat(id, name, age, isIndoor, color, isVaccinated);
        }

        public static Bird BuildBird(int id)
        {
            string name = ConsoleInput.ReadString("Name: ");
            int age = ConsoleInput.ReadInt("Age (0-50): ", 0, 50);
            string species = ConsoleInput.ReadString("Species (e.g. Parrot, Canary): ");
            bool canFly = ConsoleInput.ReadBool("Can it fly? (y/n): ");
            string wingSpan = ConsoleInput.ReadString("Wingspan (Small/Medium/Large): ");
            return new Bird(id, name, age, species, canFly, wingSpan);
        }

        public static SmallAnimal BuildSmallAnimal(int id)
        {
            string name = ConsoleInput.ReadString("Name: ");
            int age = ConsoleInput.ReadInt("Age (0-50): ", 0, 50);
            string animalType = ConsoleInput.ReadString("Type (e.g. Rabbit, Hamster, Guinea Pig): ");
            string habitatSize = ConsoleInput.ReadString("Habitat size (Small/Medium/Large): ");
            bool isNocturnal = ConsoleInput.ReadBool("Nocturnal? (y/n): ");
            return new SmallAnimal(id, name, age, animalType, habitatSize, isNocturnal);
        }
    }
}
