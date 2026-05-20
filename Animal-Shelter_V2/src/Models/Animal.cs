using System;
using System.Text;

namespace AnimalShelter.src.Models
{
    public abstract class Animal : ICareRecord, IAdoptable
    {
        private int _id;
        private string _name;
        private int _age;
        private AnimalStatus _status;
        private DateTime _intakeDate;
        private string _adopterName;
        private string _vaccineInfo;
        private readonly StringBuilder _careHistory;


        public int Id
        {
            get => _id;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Id must be greater than zero.", nameof(value));
                _id = value;
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be null or empty.", nameof(value));
                _name = value.Trim();
            }
        }
        public int Age
        {
            get => _age;
            set
            {
                if (value < 0 || value > 50)
                    throw new ArgumentException("Age must be between 0 and 50.", nameof(value));
                _age = value;
            }
        }
        public AnimalStatus Status
        {
            get => _status;
            set => _status = value;
        }
        public DateTime IntakeDate
        {
            get => _intakeDate;
            private set => _intakeDate = value;
        }
        public string AdopterName
        {
            get => _adopterName;
            private set => _adopterName = value ?? string.Empty;
        }
        public string VaccineInfo
        {
            get => _vaccineInfo;
            private set => _vaccineInfo = value ?? string.Empty;
        }


        // Constructor to initialize the animal with required properties
        protected Animal(int id, string name, int age, AnimalStatus status= AnimalStatus.Available)
        {
            Id = id;
            Name = name;
            Age = age;
            Status = status;
            IntakeDate = DateTime.Now;
            _adopterName = string.Empty;
            _vaccineInfo = string.Empty;
            _careHistory = new StringBuilder();
        }

        public abstract string GetSpeciesInfo();
        
        
        
        // ICareRecord implementation

        public void AddCareNote(string note)
        {
            if (string.IsNullOrWhiteSpace(note))
                throw new ArgumentException("Care note cannot be null or empty.", nameof(note));
            _careHistory.AppendLine($"{DateTime.Now:yyyy-MM-dd HH:mm}: {note.Trim()}");
        }
        
        public string GetCareHistory() => _careHistory.ToString().Trim();

        public void UpdateVaccineInfo(string vaccineInfo)
        {
            if (string.IsNullOrWhiteSpace(vaccineInfo))
                throw new ArgumentException("Vaccine info cannot be null or empty.", nameof(vaccineInfo));
            VaccineInfo = vaccineInfo.Trim();
            AddCareNote($"Vaccine info updated: {VaccineInfo}");
        }

        //IAdoptable implementation

        public bool IsAvailable() => Status == AnimalStatus.Available;

        public void Adopt(string adopterName)
        {
            if (Status != AnimalStatus.Available && Status != AnimalStatus.PendingAdoption)
                throw new InvalidOperationException($"'{Name}' is not available for adoption, Current Status: {Status}");
            if(string.IsNullOrWhiteSpace(adopterName))
                throw new ArgumentException("Adopter name cannot be null or empty.", nameof(adopterName));
            
            AdopterName = adopterName.Trim();
            Status = AnimalStatus.Adopted;
            AddCareNote($"Adopted by {AdopterName} on {DateTime.Now:yyyy-MM-dd}");
        }

        public string GetAdoptionInfo()
            => Status == AnimalStatus.Adopted
                ? $"Adopted by: {AdopterName}"
                : "Not adopted yet";
    }
}