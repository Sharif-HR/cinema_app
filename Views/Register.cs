namespace View;

static class Register
{
    static private AccountsLogic accountsLogic = new AccountsLogic();


    public static void Start()
    {
        Console.WriteLine("Welcome to the registration page.");
        string id = accountsLogic.GenerateUUID();

        string? userName = Inputs.InputField("Enter your username");
        string? firstName = Inputs.InputField("Enter your first name:");
        string? lastName = Inputs.InputField("Enter your last name:");
        string? email = Inputs.InputField("Enter your email:");
        string? phoneNumber = Inputs.OptionalInput("Enter your phonenumber (optional):");
        string? password = Inputs.PasswordInput();
        // string? isEligible = Inputs.InputField("Eligible (Y/n):", true, new List<string>(){"y", "n", "Y", "N"});
        bool isEligible = Inputs.CheckboxInput("Are you a student or Elderly?");

        AccountModel acc = new AccountModel(id, userName, password, email, phoneNumber, firstName, lastName, isEligible, "Customer");
        accountsLogic.UpdateList(acc);


        Menu.Start();
    }






}
