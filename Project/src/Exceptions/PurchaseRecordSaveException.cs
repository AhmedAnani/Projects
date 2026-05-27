using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Exceptions
{
    public class PurchaseRecordSaveException : RecordSaveException
    {
        public PurchaseRecordSaveException(int userId, int itemId, Exception inner)
       : base("purchase", userId, itemId, inner) { }
    }
}
    

