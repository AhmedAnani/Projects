using System.ComponentModel.DataAnnotations;

namespace Project.src.Models
{
    public class Category
    {
        private string _name = string.Empty;

        [Key]
        public int Id { get; set; }
        
        
        [MaxLength(100)]
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Category name cannot be empty.");

                _name = value.Trim();
            }
        }

        // Navigation property for mapping the relationship with LibraryItem (1:many)
        public ICollection<LibraryItem> LibraryItems { get; set; } = new List<LibraryItem>();

        // Parameterless constructor for EF Core
        protected Category() { }

        public Category(string name)
        {
            Name = name;
        }
    }
}