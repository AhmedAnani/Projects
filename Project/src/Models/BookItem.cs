using System;
using System.Collections.Generic;
using System.Text;

namespace Project.src.Models
{
    public abstract class BookItem : LibraryItem
    {

        private string _author;

        private string _description;

        private BookCategory _category;


        public string Author
        {
            get => _author;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new Exception("Author cannot be null or empty");

                _author = value;
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
            }
        }

        public BookCategory Category
        {
            get => _category;

            set
            {
                if (!Enum.IsDefined(typeof(BookCategory), value)) //Check if the value is a valid 
                {
                    string Categoryes = string.Join(", ", Enum.GetNames<BookCategory>()); //Get the allowed Categoryes from the enum and add it  them into a string
                    throw new IndexOutOfRangeException($"Invalid category: {value}. Allowed values are: {Categoryes}");
                }

                _category = value;
            }
        }

        public BookItem(int id, string title, bool isAvailable, string author, string description, BookCategory category) : base(id, title, isAvailable)
        {
            Author = author;
            Description = description;
            Category = category;
        }

        public override void displayInfo()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Category: {Category}");
            Console.WriteLine($"Is Available: {(IsAvailable ? "Yes" : "No")}");


        }
    }
}
