using System.Reflection;

public static class Helpers
{
    public static void Continue(string message = "Press any key to continue")
    {
        Console.WriteLine();
        Helpers.WarningMessage(message);
        Console.ReadKey();
    }

    public static bool GoBack(string action)
    {
        while (true)
        {
            Helpers.WarningMessage($"Enter 0 to go back or press Enter to continue {action}.");
            string uInput = Console.ReadLine();
            if (uInput == "0")
            {
                return true;
            }
            else if (uInput == "")
            {
                return false;
            }
            else
            {
                Helpers.WarningMessage("Invalid input.");
            }
        }
    }
    public static void Divider(bool hasEnter = true)
    {
        if (hasEnter)
        {
            Console.WriteLine("\n----------------------------------------");
        }
        else
        {
            Console.WriteLine("----------------------------------------");
        }
    }


    public static string GenUid()
    {
        return Guid.NewGuid().ToString();
    }

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            WarningMessage("Please enter a valid email.");
            return false;
        }

        bool atBeforeDot = false;
        bool HasDot = false;
        foreach (char c in email)
        {
            if (c == '@')
            {
                atBeforeDot = true;
            }

            if (c == '.' && atBeforeDot)
            {
                HasDot = true;
            }
        }

        if (!HasDot)
        {
            WarningMessage("Please enter a email with a '@' and a '.' after the @.");
            return false;
        }

        try
        {
            var mailAddress = new System.Net.Mail.MailAddress(email);

            return true;
        }
        catch
        {
            WarningMessage("Please enter a valid email.");
            return false;
        }
    }

    public static bool IsDigitsOnly(string str)
    {
        foreach (char c in str)
        {
            if (c < '0' || c > '9')
                return false;
        }

        return true;
    }

    public static bool IsDigitsOrDotOnly(string str)
    {
        foreach (char c in str)
        {
            if ((c < '0' || c > '9') && c != ',')
                return false;
        }

        return true;
    }
    public static bool ContainsLetters(string str)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(str, @"[a-zA-Z]");
    }


    public static void WarningMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void SuccessMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static string ListToString(List<string> list)
    {
        string value = string.Join(",", list);
        return value;
    }


    public static string TruncateString(string longString, int maxLength)
    {
        return longString.Substring(0, Math.Min(longString.Length, maxLength)) + "...";
    }

    public static string CapitalizeFirstLetter(string str)
    {
        return char.ToUpper(str[0]) + str.Substring(1);
    }


    public static bool IsValidDateOnly(string input)
    {
        if (DateOnly.TryParse(input, out DateOnly result))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsEmptyList<T>(List<T> list)
    {
        return (list.Count == 0);
    }

    public static List<string> GetProperties<Type>()
    {
        List<string> list = new();
        foreach (var prop in typeof(Type).GetProperties())
        {
            list.Add(prop.Name);
        }

        return list;
    }

    public static void PrintListContent<ReturnValue>(List<ReturnValue> list, bool isNumberd = false)
    {

        if (isNumberd)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"{i}. {list[i]}");
            }
        }
        else
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }
    }

    public static bool HasIndexInList<T>(int index, List<T> list, bool allowZero = true)
    {

        if (allowZero == false && index == 0)
        {
            return false;
        }

        if (index > list.Count || index < 0)
        {
            return false;
        }

        return true;
    }

    public static bool CaseInsensitiveContains(this string text, string value,
        StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
    {
        return text.IndexOf(value, stringComparison) >= 0;
    }
}
