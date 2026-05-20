namespace Animal_Shelter_V2.src.Models.Interface
{
    //Medical care record for an animal
    public interface ICareRecord
    {
        void AddCareNote(string note);
        string GetCareHistory();
        void UpdateVaccineInfo(string vaccineInfo);
    }
}