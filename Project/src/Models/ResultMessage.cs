using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Models
{
    // this class not mapped this is just a simple class to return the result of the operation, it contains a boolean property IsSuccess to indicate if the operation was successful and a string property Message to provide additional information about the result
    //i use in borrowing and purshaces services to return the result of the operation instead of throwing exceptions for expected failure scenarios, this approach allows for more graceful error handling and provides clear feedback to the caller about the outcome of the operation without relying on exception handling for control flow.
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public Notification Notification { get; private set; }

        private Result(bool isSuccess, string message, Notification notification = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            Notification = notification;
        }

        public static Result Success(string message = "Operation completed successfully.", Notification notification = null)
            => new Result(true, message, notification);
        public static Result Failure(string message)
            => new Result(false, message);
    }
}
