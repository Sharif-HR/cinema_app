namespace View;

static class Login
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        Console.WriteLine("Welcome to the login page");
        Console.WriteLine("Please enter your email address");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your password");
        string password = Console.ReadLine();
        AccountModel acc = accountsLogic.CheckLogin(email, password);
        if (acc != null)
        {
            //Console.WriteLine($"Welcome back {acc.FirstName} {acc.LastName}");
            //Console.WriteLine("Your email number is " + acc.EmailAddress);

            Dashboard.Start(acc);
        }
        else
        {
            Console.WriteLine("No account found with that email and/or password");
        }
    }
}