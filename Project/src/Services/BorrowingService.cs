using Project.src.Interfaces;
using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IBorrowRecordRepository _borrowRecordRepository;
        private readonly ILibraryItemRepository _libraryItemRepository;
        private readonly IAuthorizationService _authorizationService    ;


        private const int MaxBorrowLimit = 3; // Max Limit of borrow
        private const int BorrowDaysLimit = 14; // Max Days of Borrowing
        private const double FinePerDay = 10.0;// fees for lating Days ya D8of XD
        public BorrowingService(IBorrowRecordRepository borrowRecordRepository, ILibraryItemRepository libraryItemRepository, IAuthorizationService authorizationService)
        {
            _borrowRecordRepository = borrowRecordRepository;
            _libraryItemRepository = libraryItemRepository;
            _authorizationService = authorizationService;
        }
        // I use Result class to return the result of the operation, it contains a boolean property IsSuccess to indicate if the operation was successful and a string property Message to provide additional information about the result.
        public Result Process_Of_Borrow(User user, LibraryItem item)
        {
            // adding Authorization Check for user to borrow items
            if (!_authorizationService.CanBorrow(user)) return Result.Failure("User is not authorized to borrow items.");

            if (item is not IBorrowable BorrowableItem)return Result.Failure("Item cannot be borrowed.");
            // get the number of  active borrow records for the user
            var activeBorrowRecord = _borrowRecordRepository.GetActiveBorrowRecordsByUser(user.Id);
            // check if the user has reached the maximum borrow limit
            if (activeBorrowRecord >= MaxBorrowLimit) return Result.Failure("User has reached the maximum borrow limit.");
            // check if the item is available for borrowing
            if (!BorrowableItem.BorrowItem()) return Result.Failure("Failed to borrow the item. It may not be available.");
        
            var borrowRecord = new BorrowRecord(
              user.Id,
              item.Id,
              DateTime.Now.AddDays(BorrowDaysLimit)
             );
            _borrowRecordRepository.Add(borrowRecord);
            return Result.Success("Item borrowed successfully.");
        }

        public Result Process_Of_Return(User user, LibraryItem item)
        {
            if(item is not IBorrowable BorrowableItem) return Result.Failure("Item cannot be returned.");
            var borrowRecord = _borrowRecordRepository.GetBookToReturnBorrowRecord(user.Id, item.Id);
            if (borrowRecord == null) return Result.Failure("No active borrow record found for this item.");
            BorrowableItem.ReturnItem();
            // Update the borrow record to mark it as returned and use delegate to update the record in the repository because it is private set
            _borrowRecordRepository.Update(borrowRecord.Id, record =>
            {
                record.MarkReturned();
            });
            // Calculate fine if the item is returned late 
            var fine = borrowRecord.CalculateFine(FinePerDay);
            if (fine > 0)
                return Result.Success($"Item returned late. Fine amount: {fine} EGP.");
            return Result.Success("Item returned successfully.");
        }
    }
}
