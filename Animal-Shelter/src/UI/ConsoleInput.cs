using System;
namespace animal_Shelter.UI
{
    public class ConsoleInput
    {
        //Reads an integer within [min, max] and loops until valid.
        public static int ReadInt(string prompt, int min, int max)
        {
 
            while (true)
            {
                Console.Write("  " + prompt);
                string raw = Console.ReadLine() ?? string.Empty;
                if (int.TryParse(raw, out int value) && value >= min && value <= max)
                    return value;

                Console.WriteLine($"Please enter a number between {min} and {max}.");
            }
        }

        //Reads a non-empty string and loops until valid.
        public static string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write("  " + prompt);
                string value = Console.ReadLine()?.Trim() ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(value))
                    return value;
                Console.WriteLine("This field cannot be empty. Please try again.");
            }
        }

        //Reads y/n and loops until valid.
        public static bool ReadBool(string prompt)
        {
            while (true)
            {
                Console.Write("  " + prompt);
                string raw = Console.ReadLine()?.Trim().ToLower() ?? string.Empty;
                if (raw == "y" || raw == "yes") return true;
                if (raw == "n" || raw == "no") return false;
                Console.WriteLine("Please enter y (yes) or n (no).");
            }
        }
    }
}
