namespace Animal_Shelter_V2.src.Models.Interface
{
    //Animal that can be adopted
    public interface IAdoptable
    {
        bool IsAvailable();

        void Adopt(string adopterName);

        string GetAdoptionInfo();
        
    }
}