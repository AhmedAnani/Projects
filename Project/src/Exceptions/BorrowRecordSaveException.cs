using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Exceptions
{
    public class BorrowRecordSaveException: RecordSaveException
    {
        public BorrowRecordSaveException(int userId, int itemId, Exception inner)
       : base("borrow", userId, itemId, inner) { }
    }
    public class BorrowRecordUpdateException : RecordSaveException
    {
        public BorrowRecordUpdateException(int userId, int itemId, Exception inner)
            : base("return", userId, itemId, inner) { }
    }
}
