using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Exceptions
{
    public class EmailExistsException:Exception
    {
        public EmailExistsException() : base() { }
        public EmailExistsException(string message) : base(message) { }
        public EmailExistsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
