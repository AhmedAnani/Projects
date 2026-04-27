using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testing.src.Models
{
    public class EBook : Book
    {
        private string _fileFormat;
        public string FileFormat
        {
            get => _fileFormat;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new Exception("File format cannot be null or empty");
                _fileFormat = value;
            }
        }
        public EBook(int id, string title, bool isAvailable, string author, string description, BookCategory category, string fileFormat) : base(id, title, isAvailable, author, description, category)
        {
            FileFormat = fileFormat;
        }
        public override void displayInfo()
        {
            base.displayInfo();
            Console.WriteLine($"File Format: {FileFormat}");

        }

    }
}
