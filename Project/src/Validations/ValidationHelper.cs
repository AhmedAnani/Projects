using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.src.Validations
{
    public static class ValidationHelper
    {
        /* (Description of CheckNotNullOrWhiteSpaceText method)
         * Checks if a string is not null, not empty, and not whitespace.
         * If the string is null, empty, or whitespace, it throws an ArgumentException with the provided error message and parameter name.
         * If the string is valid, it returns the trimmed version of the string.
        */
        public static string CheckNotNullOrWhiteSpaceText(string value, string errorMessage, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(errorMessage, parameterName);

            return value.Trim();
        }

        /* (Description of CheckEnumValue method)
         * Checks if a given value is a defined member of the specified enum type.
         * If the value is not a valid enum member, it throws an ArgumentException with the provided error message and parameter name.
         * If the value is valid, it returns the value cast to the enum type.
        */
        public static T CheckEnumValue<T>(T value, string errorMessage, string parameterName)
            where T : struct, Enum
        {
            if (!Enum.IsDefined(typeof(T), value))
                throw new ArgumentException(errorMessage, parameterName);

            return value;
        }

        /* (Description of CheckPositiveInteger method)
        * Checks if an integer value is positive (greater than zero).
        * If the value is not positive, it throws an ArgumentException with the provided error message and parameter name.
        * If the value is valid, it returns the value.
        */
        public static int CheckPositiveInteger(int value, string errorMessage, string parameterName)
        {
            if (value <= 0)
                throw new ArgumentException(errorMessage, parameterName);

            return value;
        }

        /* (Description of CheckValidEmail method)
         * Checks if a string is a valid email format by ensuring it is not null, not empty, and contains the "@" character.
         * If the string is null, empty, or does not contain "@", it throws an ArgumentException with the provided error message and parameter name.
         * If the string is a valid email format, it returns the trimmed version of the string.
        */
        public static string CheckValidEmail(string email, string errorMessage, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException(errorMessage, parameterName);

            return email.Trim();
        }
    }
}
