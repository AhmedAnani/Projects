
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    public class Book : BookItem ,  IBorrowable , IBuyable
    {
        public Book(int id, string title, bool isAvailable, string author, string description, BookCategory category) : base(id, title, isAvailable, author, description, category)
        {
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

