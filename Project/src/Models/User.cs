using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class User
{
    private string _name;
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrEmpty(value)) throw new Exception("Name cannot be null or empty");
            _name = value;
        }
    }
    public User(string name)
    {
        Name = name;
    }
    public List<LibraryItem> BorrowedItems { get; set; } = new List<LibraryItem>();
    public List<LibraryItem> BoughtItems { get; set; } = new List<LibraryItem>();

}

