namespace Views;

class Register : ViewTemplate
{
    static private AccountsLogic accountsLogic = new AccountsLogic();
    // private Menu MenuPage = new();

    public Register() : base("Register") { }

    public void Render()
    {
        Console.WriteLine("Welcome to the registration page.");
        string id = accountsLogic.GenerateUUID();

        string? userName = Inputs.InputField("Enter your username");
        string? firstName = Inputs.InputField("Enter your first name:");
        string? lastName = Inputs.InputField("Enter your last name:");
        string? email = Inputs.InputField("Enter your email:");
        string? phoneNumber = Inputs.OptionalInput("Enter your phonenumber (optional):");
        string? password = Inputs.PasswordInput();
        bool isStudent = Inputs.CheckboxInput("Are you a student?");

        AccountModel acc = new AccountModel(id, userName, password, email, phoneNumber, firstName, lastName, isStudent, "Customer");
        accountsLogic.UpdateList(acc);
        Console.WriteLine("Registration complete.");
        Helpers.Continue();

        // MenuPage.Render();
    }






}
