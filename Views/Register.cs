namespace Views;

class Register : ViewTemplate
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public Register() : base("Register") { }

    public override void Render()
    {
        base.Render();
        string? userName = base.InputField("Enter your username");
        string? firstName = base.InputField("Enter your first name:");
        string? lastName = base.InputField("Enter your last name:");
        string? email = base.InputField("Enter your email:");
        string? phoneNumber = base.OptionalInput("Enter your phonenumber (optional):");
        string? password = base.InputPassword("Enter you password:", true);
        bool isStudent = base.CheckboxInput("Are you a student?");

        AccountModel acc = new AccountModel(userName, password, email, phoneNumber, firstName, lastName, isStudent, "customer");
        accountsLogic.UpdateList(acc);

        Helpers.Divider();
        Console.WriteLine("Registration complete.");
        Helpers.Continue();
    }






}
