namespace AnimalShelter.src.Models
{
    //Animal that can be adopted
    public interface IAdoptable
    {
        bool IsAvailable();

        void Adopt(string adopterName);

        string GetAdoptionInfo();
        
    }
}