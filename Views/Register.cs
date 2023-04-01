namespace Views;

class Register : ViewTemplate
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public Register() : base("Register") { }

    public override void Render()
    {
        base.Render();
        string? userName = Inputs.InputField("Enter your username");
        string? firstName = Inputs.InputField("Enter your first name:");
        string? lastName = Inputs.InputField("Enter your last name:");
        string? email = Inputs.InputField("Enter your email:");
        string? phoneNumber = Inputs.OptionalInput("Enter your phonenumber (optional):");
        string? password = Inputs.PasswordInput();
        bool isStudent = Inputs.CheckboxInput("Are you a student?");

        AccountModel acc = new AccountModel(userName, password, email, phoneNumber, firstName, lastName, isStudent, "customer");
        accountsLogic.UpdateList(acc);

        Helpers.Divider();
        Console.WriteLine("Registration complete.");
        Helpers.Continue();
    }






}
