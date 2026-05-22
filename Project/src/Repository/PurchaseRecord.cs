using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Repository
{
    public class PurchaseRecord : GenericRepository<PurchaseRecord>
    {
        public PurchaseRecord(Models.AppDbContext context) : base(context)
        {
        }
    }
}
