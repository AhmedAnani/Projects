using Microsoft.EntityFrameworkCore;
using Project.src.Interfaces.IRepository;
using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Repositories
{
    public class PurchaseRecordRepository : GenericRepository<PurchaseRecord>, IPurchaseRecordRepository
    {
        public PurchaseRecordRepository(AppDbContext context) : base(context)
        {
        }

        //This method retrieves all purchase records for a specific user, ordered by purchase date in descending order.
        public IEnumerable<PurchaseRecord> GetPurchasesByUser(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("User id must be positive.", nameof(userId));

            return _context.PurchaseRecords
                .Include(pr => pr.User)
                .Include(pr => pr.LibraryItem)
                .AsNoTracking()
                .Where(pr => pr.UserId == userId)
                .OrderByDescending(pr => pr.PurchasedAt)
                .ToList();
        }
    }
}
