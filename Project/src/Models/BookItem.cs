using System.ComponentModel.DataAnnotations;

namespace Project.src.Models
{
    public abstract class BookItem : LibraryItem
    {
        private string _author = string.Empty;
        private string _description = string.Empty;

        [MaxLength(100)]
        public string Author
        {
            get => _author;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Author cannot be empty.");

                _author = value.Trim();
            }
        }

        [MaxLength(100)]
        public string Description
        {
            get => _description;
            set => _description = value?.Trim() ?? string.Empty;
        }

        // Parameterless constructor for EF Core
        protected BookItem() { }

        protected BookItem(string title, int categoryId, string author, string description)
            : base(title, categoryId)
        {
            Author = author;
            Description = description;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Type: {ItemType}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Category: {Category?.Name ?? "Not assigned"}");
            Console.WriteLine($"Status: {Status}");
        }
    }
}