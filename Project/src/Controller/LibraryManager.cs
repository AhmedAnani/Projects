using Project.src.Exceptions;
using Project.src.Interfaces.IService;
using Project.src.Models;

namespace Project.src.Controller
{
    public class LibraryManager
    {
        private readonly IBuyingServise _buyingServise;
        private readonly IBorrowingService _borrowingService;

        public LibraryManager(IBuyingServise buyingServise, IBorrowingService borrowingService)
        {
            _buyingServise = buyingServise;
            _borrowingService = borrowingService;
        }

        public void BuyItem(User user, LibraryItem item)
        {
            try
            {
                ConsolePrinter.Header("BUY ITEM");
                var result = _buyingServise.ProcessBuy(user, item);

                if (result.IsSuccess)
                {
                    ConsolePrinter.Success(result.Message);
                    if (result.Notifications != null && result.Notifications.Any())
                        foreach (var n in result.Notifications)
                            ConsolePrinter.Notification(n.Message);
                }
                else
                {
                    ConsolePrinter.Warning(result.Message);
                }
            }
            catch (PurchaseRecordSaveException ex)
            {
                ConsolePrinter.Error($"Purchase failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                ConsolePrinter.Error($"Unexpected error: {ex.Message}");
            }
        }

        public void BorrowItem(User user, LibraryItem item)
        {
            try
            {
                ConsolePrinter.Header("BORROW ITEM");
                var result = _borrowingService.Process_Of_Borrow(user, item);

                if (result.IsSuccess)
                {
                    ConsolePrinter.Success(result.Message);
                    if (result.Notifications != null && result.Notifications.Any())
                        foreach (var n in result.Notifications)
                            ConsolePrinter.Notification(n.Message);
                }
                else
                {
                    ConsolePrinter.Warning(result.Message);
                }
            }
            catch (BorrowRecordSaveException ex)
            {
                ConsolePrinter.Error($"Borrowing failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                ConsolePrinter.Error($"Unexpected error: {ex.Message}");
            }
        }

        public void ReturnItem(User user, LibraryItem item)
        {
            try
            {
                ConsolePrinter.Header("RETURN ITEM");
                var result = _borrowingService.Process_Of_Return(user, item);

                if (result.IsSuccess)
                {
                    ConsolePrinter.Success(result.Message);
                    if (result.Notifications != null && result.Notifications.Any())
                        foreach (var n in result.Notifications)
                            ConsolePrinter.Notification(n.Message);
                }
                else
                {
                    ConsolePrinter.Warning(result.Message);
                }
            }
            catch (BorrowRecordUpdateException ex)
            {
                ConsolePrinter.Error($"Return failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                ConsolePrinter.Error($"Unexpected error: {ex.Message}");
            }
        }
        public void ShowUserBorrowRecords()
        {
            ConsolePrinter.Header("MY BORROWED ITEMS");

            var records = _borrowingService.GetBorrowRecords();

            if (!records.Any())
            {
                ConsolePrinter.Warning("No borrow records found.");
                return;
            }

            foreach (var record in records)
            {
                bool isReturned = record.ReturnedAt != null;
                bool isOverdue = !isReturned && record.DueDate < DateTime.Now;

                string status = isReturned ? "Returned" : isOverdue ? "OVERDUE" : "Active";
                string line = $"[{status}] '{record.LibraryItem.Title}' | Borrowed: {record.BorrowedAt:dd/MM/yyyy} | Due: {record.DueDate:dd/MM/yyyy}";

                if (isOverdue)
                    ConsolePrinter.Error(line);
                else if (isReturned)
                    ConsolePrinter.Info(line);
                else
                    ConsolePrinter.Success(line);
            }
        }
    }
}