
using Project.src.Interfaces;
using Project.src.Models;
using testing.src.Models;

namespace Project.src.Services
{   
    // That Class for Borrowing items only with specific rules 
    public class BorrowingService
    {
        private const int MaxBorrowLimit = 3; // Max Limit of borrow
        private const int BorrowDaysLimit = 14; // Max Days of Borrowing
        private const double FinePerDay = 10.0;// fees for lating Days ya D8of XD
        public void Process_Of_Borrow(User user, LibraryItem item)
        {
            if (item is not IBorrowable borrowableItem) //Check item can be Borrow or not
            {
                Console.WriteLine("Only books can be borrowed.");
                return;
            }
            if (user.BorrowedItems.Count >= MaxBorrowLimit)//Check  First Rules if U Dont go up Clown
            {
                throw new Exception("You have reached the maximum borrow limit.");
            }
            
              user.BorrowedItems.Add(item);
              borrowableItem.DueDate = DateTime.Now.AddDays(BorrowDaysLimit);
              Console.WriteLine($"Due date: {borrowableItem.DueDate}");
                

        }

        public void Process_Of_Return(User user, LibraryItem item)
        {
            if (item is not IBorrowable borrowableItem) //Check item can be Borrow or not
            {
                Console.WriteLine(" This item is not a returnable book.");
                return;
            }
            if (user.BorrowedItems.Contains(item))//Check that item in ur Borrow list or not
            {
                user.BorrowedItems.Remove(item);
                borrowableItem.ReturnItem();
                if (DateTime.Now > borrowableItem.DueDate)// Check DueDate 
                {
                    int daysLate = (DateTime.Now - borrowableItem.DueDate).Days;
                    double fine = daysLate * FinePerDay;
                    Console.WriteLine($"You are {daysLate} days late. Your fine is: {fine} currency units.");
                }
                else
                {
                    Console.WriteLine($"Book '{item.Title}' returned on time. Thank you!");
                }
            }
            else
            {
                Console.WriteLine("This user does not have this book in their borrowed list.");
            }

        }
    }
}
