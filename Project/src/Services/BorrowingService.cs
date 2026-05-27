using Project.src.Exceptions;
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
        private readonly INotificationService _notificationService;


        private const int MaxBorrowLimit = 3; // Max Limit of borrow
        private const int BorrowDaysLimit = 14; // Max Days of Borrowing
        private const double FinePerDay = 10.0;// fees for lating Days ya D8of XD
        public BorrowingService(IBorrowRecordRepository borrowRecordRepository, ILibraryItemRepository libraryItemRepository, IAuthorizationService authorizationService,INotificationService notificationService)
        {
            _borrowRecordRepository = borrowRecordRepository;
            _libraryItemRepository = libraryItemRepository;
            _authorizationService = authorizationService;
            _notificationService = notificationService;
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
            try
            {
                var dueDate = DateTime.Now.AddDays(BorrowDaysLimit);
                var borrowRecord = new BorrowRecord(
                  user.Id,
                  item.Id,
                 dueDate
                 );
                _borrowRecordRepository.Add(borrowRecord);
               var notification= _notificationService.SendBorrowNotification(user.Id, item, dueDate);
                return Result.Success("Item borrowed successfully.", notification);
            }
            catch (Exception ex)
            {
                BorrowableItem.ReturnItem(); // Rollback the borrow action if adding the record fails
                throw new BorrowRecordSaveException(user.Id, item.Id, ex);
            }
        }

        public Result Process_Of_Return(User user, LibraryItem item)
        {
            if(item is not IBorrowable BorrowableItem) return Result.Failure("Item cannot be returned.");
            var borrowRecord = _borrowRecordRepository.GetBookToReturnBorrowRecord(user.Id, item.Id);
            if (borrowRecord == null) return Result.Failure("No active borrow record found for this item.");
            try
            {
                BorrowableItem.ReturnItem();

                _borrowRecordRepository.Update(borrowRecord.Id, record =>
                {
                    record.MarkReturned();
                });

                var updatedRecord = _borrowRecordRepository.GetById(borrowRecord.Id);

                var fine = updatedRecord.CalculateFine(FinePerDay);

                if (fine > 0)
                {
                   var notificationfine= _notificationService.SendReturnNotification(user.Id, item, true, fine);
                  
                    return Result.Success($"Item returned late. Fine amount: {fine} EGP.", notificationfine);
                }

                var notification = _notificationService.SendReturnNotification(user.Id, item, false, 0);
                return Result.Success("Item returned successfully.", notification);
            }
            catch (Exception ex)
            {
                BorrowableItem.BorrowItem(); //  Rollback ReturnItem
                throw new BorrowRecordUpdateException(user.Id, item.Id, ex);
            }
        }
    }
}
