using Project.src.Validations;
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

        

        public void ChangeAuthor(string author)
        {
            Author = ValidationHelper.CheckNotNullOrWhiteSpaceText(author, "Author cannot be null or whitespace.", nameof(author));
        }
        public void ChangeDescription(string description)
        {
            Description = ValidationHelper.CheckNotNullOrWhiteSpaceText(description, "Description cannot be null or whitespace.", nameof(description));
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