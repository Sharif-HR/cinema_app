namespace Views;

class Register : ViewTemplate
{
    static private AccountsLogic _accountsLogic = new AccountsLogic();

    public Register() : base("Register") { }

    public override void Render()
    {
        base.Render();
        string? username = base.InputField("Enter your username");
        string? firstName = base.InputField("Enter your first name:");
        string? lastName = base.InputField("Enter your last name:");
        string? email = EmailInput();
        string? phoneNumber = base.OptionalInput("Enter your phonenumber (optional):");
        string? password = base.InputPassword("Enter you password:", true);
        bool isStudent = base.CheckboxInput("Are you a student?");

        AccountModel acc = new(username, password, email, phoneNumber, firstName, lastName, isStudent, "customer");
        _accountsLogic.AddAccount(acc);

        Helpers.Divider();
        Helpers.SuccessMessage("Registration complete.");
        Helpers.Continue();
    }


    private string EmailInput(){
        while(true){
            string emailInput = base.InputField("Enter your email:");

            if(!_accountsLogic.EmailExists(emailInput) && Helpers.IsValidEmail(emailInput)){
                return emailInput;
            }
        }
    }
}
