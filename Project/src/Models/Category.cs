using System.ComponentModel.DataAnnotations;

namespace Project.src.Models
{
    public class Category
    {
        [Key]
        public int Id { get; private set; }

        [MaxLength(100)]
        public string Name { get; private set; } = string.Empty;

        // Navigation property for mapping the relationship with LibraryItem (1:many)
        public ICollection<LibraryItem> LibraryItems { get; private set; } = new List<LibraryItem>();

        // Parameterless constructor for EF Core
        protected Category() { }

        public Category(string name)
        {
            Rename(name);
        }

        // Method to rename the category with validation (EF Core mapping Encapsulation)
        public void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name cannot be empty.");

            Name = name.Trim();
        }
    }
}