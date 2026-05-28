using Project.src.Controller;

public static class InputHelper
{
    public static int GetInt(string prompt)
    {
        Console.Write(prompt);

        while (true)
        {
            var input = Console.ReadLine();

            if (int.TryParse(input, out int value))
                return value;

            ConsolePrinter.Warning("Invalid number, try again:");
        }
    }

    public static string GetString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? "";
    }

    public static T GetEnum<T>(string prompt) where T : struct
    {
        Console.Write(prompt);

        while (true)
        {
            var input = Console.ReadLine();

            if (Enum.TryParse<T>(input, true, out var result))
                return result;

            ConsolePrinter.Warning("Invalid value, try again:");
        }
    }
}