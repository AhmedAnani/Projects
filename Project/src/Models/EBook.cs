using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class EBook : BookItem, IBuyable
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

