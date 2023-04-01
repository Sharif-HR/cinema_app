using System.Security;
static class Helpers
{
    public static String PasswordToAstriks(){
        SecureString pass = new SecureString();
        string pass2 = string.Empty;
        ConsoleKeyInfo keyInfo;

        do
        {
            keyInfo = Console.ReadKey(true);
            if(!char.IsControl(keyInfo.KeyChar)){
                pass.AppendChar(keyInfo.KeyChar);
                pass2 += keyInfo.KeyChar;
                Console.Write("*");
            }
            else if(keyInfo.Key == ConsoleKey.Backspace && pass.Length > 0){
                pass.RemoveAt(pass.Length - 1);
                Console.Write("\b \b");
            }
        }
        while(keyInfo.Key != ConsoleKey.Enter);
        {
            Console.WriteLine();
            return pass2;
        }
    }

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
