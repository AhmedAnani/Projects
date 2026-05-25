using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Exceptions
{
    public class RecordSaveException : Exception
    {
        public int UserId { get; }
        public int ItemId { get; }

        public RecordSaveException(string action, int userId, int itemId, Exception inner)
            : base($"Failed to save {action} record for User {userId} and Item {itemId}.", inner)
        {
            UserId = userId;
            ItemId = itemId;
        }
    }
}
