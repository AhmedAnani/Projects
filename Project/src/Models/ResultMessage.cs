using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Models
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public List<Notification> Notifications { get; private set; }

        private Result(bool isSuccess, string message, List<Notification> notifications = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            Notifications = notifications ?? new List<Notification>();
        }

        public static Result Success(string message = "Operation completed successfully.", List<Notification> notifications = null)
            => new Result(true, message, notifications);

        public static Result Failure(string message)
            => new Result(false, message);
    }
}