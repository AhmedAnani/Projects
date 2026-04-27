using System;
using System.Collections.Generic;
using System.Text;

public abstract class LibraryItem
{
    private int _id;
    private string _title;
    private bool _isAvailable;

    public bool IsAvailable
    {
        get => _isAvailable;
        set => _isAvailable = value;
    }

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

    public LibraryItem(int id, string title, bool isAvailable)
    {
        Id = id;
        Title = title;
        IsAvailable = isAvailable;

    }
}
