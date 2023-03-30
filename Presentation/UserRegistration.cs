static class UserRegistration
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        Console.WriteLine("Welcome to the registration page.");
        string id = accountsLogic.GenerateUUID();


        Console.WriteLine("Welcome to the register page");
        string userName = Inputs.InputField("Enter your username");
        string firstName = Inputs.InputField("Enter your first name:");
        string lastName = Inputs.InputField("Enter your last name:");
        string email = Inputs.InputField("Enter your email:");
        string phoneNumber = Inputs.InputField("Enter your phonenumber (optional):");
        Console.WriteLine("Enter your password");
        string password = Helpers.PasswordToAstriks();
        string isEligible = Inputs.InputField("Eligible (Y/n):", true, new List<string>(){"y", "n", "Y", "N"});

        AccountModel acc = new AccountModel(id, userName, password, email, phoneNumber, firstName, lastName, isEligible, "Customer");
        accountsLogic.UpdateList(acc);

        Menu.Start();
    }






}
