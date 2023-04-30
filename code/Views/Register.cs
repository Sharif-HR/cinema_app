namespace Views;

public class Register : ViewTemplate
{
    static private AccountsLogic _accountsLogic = new AccountsLogic();
    private Dashboard customerDashboardPage= new("customer");

    public Register() : base("Register") { }

    public override void Render()
    {
        while (true)
        {
            base.Render();
            RouteHandeler.LastView();

            string? username = base.InputField("Enter your username");
            string? firstName = base.InputField("Enter your first name:");
            string? lastName = base.InputField("Enter your last name:");
            string? email = EmailInput();
            string? phoneNumber = base.InputPhoneNumber("Enter your phonenumber:", true);
            string? password = base.InputPassword("Enter you password:", true);
            bool isStudent = base.CheckboxInput("Are you a student? (Press 'y' for Yes or 'n' for No)");

            AccountModel acc = new(username, password, email, phoneNumber, firstName, lastName, isStudent, "customer");
            _accountsLogic.AddAccount(acc);

            Helpers.Divider();
            Helpers.SuccessMessage("Registration complete.");
            Helpers.Continue();

            RouteHandeler.View("DashboardPage");
            return;
        }
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
