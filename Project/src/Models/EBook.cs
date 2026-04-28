using Project.src.Interfaces;
using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace testing.src.Models
{
    public class EBook : BookItem , IBuyable
    {
        private string _fileSize;
        public string FileSize
        {
            get => _fileSize;
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length > 100) throw new Exception("File size cannot be null or empty and must be less than 100 characters");
                _fileSize = value;
            }
        }
        public EBook(int id, string title, bool isAvailable, string author, string description, BookCategory category, string fileSize) : base(id, title, isAvailable, author, description, category)
        {
            FileSize = fileSize;
        }
        public override void displayInfo()
        {
            base.displayInfo();
            Console.WriteLine($"File Size: {FileSize}");

        }
        
        public void BuyItem()
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                Console.WriteLine($"You have bought the eBook: {Title}");
            }
            else
            {
                Console.WriteLine("Sorry, this eBook is currently not available for buying.");
            }
        }
    }
}
