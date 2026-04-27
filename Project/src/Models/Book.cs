using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testing.src.Interfaces;

namespace testing.src.Models
{
    public class Book : BookItem ,  IBorrowable , IBuyable
    {


        public override void displayInfo()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Category: {Category}");
            if(IsAvailable)
                 Console.WriteLine("Is Available");
            else
                Console.WriteLine("Is Not Available");
        }

        public void BorrowItem()
        {
            if (!IsAvailable)
            {
                Console.WriteLine("Sorry, this book is currently not available for borrowing.");
                return;
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
                Console.WriteLine("This book is already available in the library.");
                return;
            }
            IsAvailable = true;
            Console.WriteLine($"You have returned the book: {Title}");
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
                Console.WriteLine("Sorry, this book is currently not available for buying.");
            }
        }
    }
}
