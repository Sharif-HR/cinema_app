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
}
