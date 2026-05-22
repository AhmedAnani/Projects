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
            UpdateBookDetails(author, description);
        }

        //Method to handel Encapsulations in the EF core
        public void UpdateBookDetails(string author, string description)
        {
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be empty.");

            Author = author.Trim();
            Description = description?.Trim() ?? string.Empty;
        }

        public override  string DisplayInfo()
        {
            return $"Id: {Id}\n" +
                   $"Type: {ItemType}\n" +
                   $"Title: {Title}\n" +
                   $"Author: {Author}\n" +
                   $"Description: {Description}\n" +
                   $"Category: {Category?.Name ?? "Not assigned"}\n" +
                   $"Status: {Status}";

           
        }
    }
}