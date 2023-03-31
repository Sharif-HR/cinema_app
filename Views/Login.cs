namespace Views;

class Login : ViewTemplate
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public Login() : base("Login") { }

    public override void Render()
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
            Dashboard dashboard = new();
            dashboard.Render(acc);
        }
        else
        {
            Console.WriteLine("No account found with that email and/or password");
            Helpers.Continue();
        }
    }
}
