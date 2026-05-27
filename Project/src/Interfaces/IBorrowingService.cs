using Project.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Interfaces
{
    public interface IBorrowingService
    {
        public Result Process_Of_Borrow(User user, LibraryItem item);
        public Result Process_Of_Return(User user, LibraryItem item);
    }
}
