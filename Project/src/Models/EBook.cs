using Project.src.Enums;
using Project.src.Interfaces;
using Project.src.Validations;
using System.ComponentModel.DataAnnotations;

namespace Project.src.Models
{
    public class EBook : BookItem, IBuyable
    {
        [MaxLength(500)]
        public string FileSize { get; private set; } = string.Empty;

        public override ItemType ItemType => ItemType.EBook;

        //Parameterless constructor for EF Core
        protected EBook() { }

        public EBook(string title, int categoryId, string author, string description, string fileSize)
            : base(title, categoryId, author, description)
        {
            ChangeFileSize(fileSize);
        }

        //Method that handel Encapsulations in the EF core
        public void ChangeFileSize(string fileSize)
        {
            // Validate file size input before setting the property
            if (fileSize.Length > 500)
                throw new ArgumentException("File size must be less than or equal to 500 characters.");

            FileSize = ValidationHelper.CheckNotNullOrWhiteSpaceText(fileSize, "File size cannot be null or whitespace.", nameof(fileSize));
        }

        public override string  DisplayInfo()
        {
            return base.DisplayInfo() + $"\nFile Size: {FileSize}";
        }

        public void BuyItem()
        {
            if (IsAvailable)
                MarkAsSold();
        }
    }
}