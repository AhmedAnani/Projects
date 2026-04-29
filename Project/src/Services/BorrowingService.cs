
using Project.src.Models;
using testing.src.Models;

namespace Project.src.Services
{
    public class BorrowingService
    {
        private const int MaxBorrowLimit = 3;
        private const int BorrowDaysLimit = 14;
        private const double FinePerDay = 10.0;
        public void Process_Of_Borrow(User user, LibraryItem item)
        {
            if (item is not Book book)
            {
                Console.WriteLine("Only books can be borrowed.");
                return;
            }
            if (user.BorrowedItems.Count >= MaxBorrowLimit)
            {
                throw new Exception("You have reached the maximum borrow limit.");
            }
            else
            {
                book.BorrowItem();
                if (!item.IsAvailable)
                {
                    user.BorrowedItems.Add(item);
                    book.DueDate = DateTime.Now.AddDays(BorrowDaysLimit);
                    Console.WriteLine($"Due date: {book.DueDate}");
                }
            }

        }

        public void Process_Of_Return(User user, LibraryItem item)
        {
            if (item is not Book book)
            {
                Console.WriteLine("Error: This item is not a returnable book.");
                return;
            }
            if (user.BorrowedItems.Contains(item))
            {
                user.BorrowedItems.Remove(item);
                book.ReturnItem();
                if (DateTime.Now > book.DueDate)
                {
                    int daysLate = (DateTime.Now - book.DueDate).Days;
                    double fine = daysLate * FinePerDay;
                    Console.WriteLine($"You are {daysLate} days late. Your fine is: {fine} currency units.");
                }
                else
                {
                    Console.WriteLine($"Book '{book.Title}' returned on time. Thank you!");
                }
            }
            else
            {
                Console.WriteLine("Error: This user does not have this book in their borrowed list.");
            }

        }
    }
}
