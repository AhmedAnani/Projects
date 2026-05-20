namespace AnimalShelter.src.Models
{
    //Medical care record for an animal
    public interface ICareRecord
    {
        void AddCareNote(string note);
        string GetCareHistory();
        void UpdateVaccineInfo(string vaccineInfo);
    }
}