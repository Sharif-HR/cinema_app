static class UserRegistration
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    
    
    public static void Start()
    {
        Console.WriteLine("Welcome to the registration page.");
        string id = accountsLogic.GenerateUUID();
        
        
        Console.WriteLine("Please enter a username.");
        string username = Console.ReadLine();
        Console.WriteLine("Please enter a password.");
        string password = Console.ReadLine();
        Console.WriteLine("Please enter your email address.");
        string emailAddress = Console.ReadLine();
        Console.WriteLine("Please enter your phone number.");
        string phoneNumber = Console.ReadLine();
        Console.WriteLine("Please enter your first name.");
        string firstName = Console.ReadLine();
        Console.WriteLine("Please enter your last name.");
        string lastName = Console.ReadLine();

        AccountModel acc = new AccountModel(id, username, password, emailAddress, phoneNumber, firstName, lastName, false, "Customer");
        accountsLogic.UpdateList(acc);
        
        Menu.Start();
    }






}