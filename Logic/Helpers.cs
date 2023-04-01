using System.Security;
static class Helpers
{
    public static void Continue() {
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    public static void Divider(){
        Console.WriteLine("\n----------------------------------------");
    }


    public static string GenUid()
    {
        return Guid.NewGuid().ToString();
    }

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) {
            WarningMessage("Please enter a valid email.");
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


    public static void WarningMessage(string message){
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
}
