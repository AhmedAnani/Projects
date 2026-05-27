using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Exceptions
{
    public class CategoryNotExistException : Exception
    {
        public CategoryNotExistException() { }

        public CategoryNotExistException(string message) : base(message) { }

        public CategoryNotExistException(string message, Exception inner) : base(message, inner) { }
    }
}
