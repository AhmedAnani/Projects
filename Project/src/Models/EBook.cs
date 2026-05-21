using Project.src.Enums;
using Project.src.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.src.Models
{
    public class EBook : BookItem, IBuyable
    {
        private string _fileSize = string.Empty;

        [NotMapped]
        public override ItemType ItemType => ItemType.EBook;

        [MaxLength(500)]
        public string FileSize
        {
            get => _fileSize;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("File size cannot be empty.");

                if (value.Length > 500)
                    throw new ArgumentException("File size must be less than 500 characters.");

                _fileSize = value.Trim();
            }
        }

        // Parameterless constructor for EF Core
        protected EBook() { }

        public EBook(string title, int categoryId, string author, string description, string fileSize)
            : base(title, categoryId, author, description)
        {
            FileSize = fileSize;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"File Size: {FileSize}");
        }

        public void BuyItem()
        {
            if (IsAvailable)
                Status = ItemStatus.Sold;
        }
    }
}