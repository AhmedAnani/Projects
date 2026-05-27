using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Interfaces
{
    public interface IBorrowRecordRepository: IRepository<BorrowRecord>
    {
        IEnumerable<BorrowRecord> GetBorrowRecordsByUser(int userId);
        IEnumerable<BorrowRecord> GetActiveBorrowRecords();
        IEnumerable<BorrowRecord> GetOverdueBorrowRecords();
        public int GetActiveBorrowRecordsByUser(int userId);
        public BorrowRecord GetBookToReturnBorrowRecord(int userId, int libraryItemId);
    }
}
