using Project.src.Exceptions;

using Project.src.Interfaces;
using Project.src.Models;
using Project.src.Services;

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
                    if (result.Notification != null)
                        ConsolePrinter.Notification(result.Notification.Message);
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
                    if (result.Notification != null)
                        ConsolePrinter.Notification(result.Notification.Message);
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
                    if (result.Notification != null)
                        ConsolePrinter.Notification(result.Notification.Message);
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
    }
}