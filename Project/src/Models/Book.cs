
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class Book : BookItem, IBorrowable, IBuyable
{
    private DateTime _dueDate;
    public DateTime DueDate
    {
        get => _dueDate;
        set
        {
            if (value < DateTime.Now) throw new Exception("Due date cannot be in the past");
            _dueDate = value;
        }
    }
    public Book(int id, string title, bool isAvailable, string author, string description, BookCategory category) : base(id, title, isAvailable, author, description, category)
    {
    }

    public void BorrowItem()
    {
        if (!IsAvailable)
        {
            Console.WriteLine("Sorry, this book is currently not available for borrowing.");
            
        }
        else
        {
            IsAvailable = false;
            Console.WriteLine($"You have borrowed the book: {Title}");
        }
    }

    public void ReturnItem()
    {
        if (IsAvailable)
        {
            throw new Exception("You cannot return an item that you haven't borrowed.");

        }
        else
        {
            IsAvailable = true;
            Console.WriteLine($"You have returned the book: {Title}");
        }
    }

    public void BuyItem()
    {
        if (IsAvailable)
        {
            IsAvailable = false;
            Console.WriteLine($"You have bought the book: {Title}");
        }
        else
        {
            throw new Exception("Sorry, this book is currently not available for buying.");
        }
    }
}


