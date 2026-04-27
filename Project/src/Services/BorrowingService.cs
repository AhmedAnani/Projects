using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.src.Services
{
    public class BorrowingService
    {
        private const int MaxBorrowLimit = 3;
        private const int BorrowDaysLimit = 14; 
        private const double FinePerDay = 10.0;
        //private DateTime _dueDate;
        //public DateTime DueDate
        //{
        //    get => _dueDate;
        //    private set
        //    {
        //        if (value < DateTime.Now) throw new Exception("Due date cannot be in the past");
        //        _dueDate = value;
        //    }
        //}

        public void Process_Of_Borrow(User user, Book item)
        {
            if (user.BorrowedItems.Count >= MaxBorrowLimit)
            {
                throw new Exception("You have reached the maximum borrow limit.");
            }
            else {
                item.BorrowItem();
                if (!item.IsAvailable)
                {
                    user.BorrowedItems.Add(item);
                    item.DueDate = DateTime.Now.AddDays(BorrowDaysLimit);
                    Console.WriteLine($"Due date: {item.DueDate}");
                }
            }

        }
        
        public void Process_Of_Return(User user, Book item )
        {
            if(user.BorrowedItems.Contains(item))
            {
                user.BorrowedItems.Remove(item);
                item.ReturnItem();
                if (DateTime.Now > item.DueDate)
                {
                    int daysLate = (DateTime.Now - item.DueDate).Days;
                    double fine = daysLate * FinePerDay;
                    Console.WriteLine($"You are {daysLate} days late. Your fine is: {fine} currency units.");
                }
            }


        }
    }
}
