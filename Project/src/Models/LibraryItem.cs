using System;
using System.Collections.Generic;
using System.Text;

public abstract class LibraryItem
{
    private int _id;

    private string _title;

    private string _author;

    private string _description;

    private BookCategory _category;

    private bool _isAvailable;

    abstract public void displayInfo();

    public int Id
    {
        get => _id;
        set
        {
            if (value < 0) throw new Exception($"Id must be a positive integer, Your input is : {value}");

            _id = value;
        }

    }

    public string Title
    {
        get => _title;
        set
        {
            if (string.IsNullOrEmpty(value)) throw new Exception("Title cannot be null or empty");

            _title = value;
        }
    }

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



    public Library(int id, string title, string author, string description, BookCategory category)
    {
        Id = id;
        Title = title;
        Author = author;
        Description = description;
        Category = category;
    }
