using System.ComponentModel.DataAnnotations;

namespace Project.src.Models
{
    public abstract class BookItem : LibraryItem
    {
        [MaxLength(100)]
        public string Author { get; private set; } = string.Empty;

        [MaxLength(100)]
        public string Description { get; private set; } = string.Empty;

        // Protected constructor for EF Core 
        protected BookItem() { }

        protected BookItem(string title, int categoryId, string author, string description)
            : base(title, categoryId)
        {
            ChangeAuthor(author);
            ChangeDescription(description);
        }

        //Method to handel Encapsulations in the EF core

        public void ChangeAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be empty.");

            Author = author.Trim();
        }

        public void ChangeDescription(string description)
        {
            Description = description?.Trim() ?? string.Empty;
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