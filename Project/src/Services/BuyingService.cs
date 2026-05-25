using Project.src.Exceptions;
using Project.src.Interfaces;
using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Services
{
     public class BuyingService : IBuyingServise
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IPurchaseRecordRepository _purchaseRecordRepository;
        private readonly INotificationService _notificationService;

        public BuyingService(IAuthorizationService authorizationService, IPurchaseRecordRepository purchaseRecordRepository, INotificationService notificationService)
        {
            _authorizationService = authorizationService;
            _purchaseRecordRepository = purchaseRecordRepository;
            _notificationService = notificationService;
        }
        public Result ProcessBuy(User user, LibraryItem item)
        {
            if(!_authorizationService.CanBuy(user)) return Result.Failure("User does not have permission to buy items.");
            if(item is not IBuyable buyableItem) return Result.Failure("Item is not available for purchase.");

            if (!buyableItem.BuyItem()) return Result.Failure("Failed to process purchase. Please try again.");
            // Record the purchase and the date of purchase is Now (default)

            try
            {
                
                var purchaseRecord = new PurchaseRecord(user.Id, item.Id);
                _purchaseRecordRepository.Add(purchaseRecord);
                var notification = _notificationService.SendPurchaseNotification(user.Id, item);
                
                return Result.Success("Purchase processed successfully.", notification);
            }
            catch (Exception ex)
            {
               buyableItem.UndoBuy(); // Undo the purchase if saving the record fails
                // make custom exception to handle purchase record saving errors
                throw new PurchaseRecordSaveException(user.Id, item.Id, ex);
            }

        }
    }
}
