
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Interfaces
{
    public interface IBorrowable
    {
        public DateTime DueDate { get; set; }
        bool BorrowItem();
        void ReturnItem();
    }
}
