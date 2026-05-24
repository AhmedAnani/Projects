using Microsoft.EntityFrameworkCore;
using Project.src.Interfaces;
using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project.src.Repositories
{
    public class BorrowRecordRepository : GenericRepository<BorrowRecord>, IBorrowRecordRepository
    {
        public BorrowRecordRepository(AppDbContext context) : base(context)
        {
        }

        // This method is used to get all borrow records for a specific user
        public IEnumerable<BorrowRecord> GetBorrowRecordsByUser(int userId)
        {
            if(userId <=0)
                throw new ArgumentException("User ID must be greater than zero.", nameof(userId));

            return _context.BorrowRecords
                .Include(br => br.User)
                .Include(br => br.LibraryItem)
                .AsNoTracking()
                .Where(br => br.UserId == userId)
                .OrderByDescending(br => br.BorrowedAt)
                .ToList();
        }

        //This method returns all borrow records for books/items that are still borrowed and not returned yet.
        public IEnumerable<BorrowRecord> GetActiveBorrowRecords()
        {
            return _context.BorrowRecords
                .Include(br => br.User)
                .Include(br => br.LibraryItem)
                .AsNoTracking()
                .Where(br => br.ReturnedAt == null)
                .OrderBy(br => br.DueDate)
                .ToList();
        }

        //This method gets all borrow records where the item is late and still not returned.
        public IEnumerable<BorrowRecord> GetOverdueBorrowRecords()
        {
            return _context.BorrowRecords
                .Include(br => br.User)
                .Include(br => br.LibraryItem)
                .AsNoTracking()
                .Where(br => br.ReturnedAt == null && br.DueDate < DateTime.Now)
                .OrderBy(br => br.DueDate)
                .ToList();
        }
    }
}
