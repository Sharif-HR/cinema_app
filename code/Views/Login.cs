namespace Views;

public class Login : ViewTemplate
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public Login() : base("Login") { }

    public override void Render()
    {
        while (true)
        {

            base.Render();
            RouteHandeler.LastView();

            if(Environment.GetEnvironmentVariable("ENVIRONMENT") == "development") {
                RouteHandeler.View("DashboardPage");
            }

            string Email = base.InputField("Enter your email: ");
            string Password = base.InputPassword("Enter password: ");

            AccountModel UserAccount = accountsLogic.CheckLogin(Email, Password);


            if (UserAccount != null)
            {
                RouteHandeler.View("DashboardPage");
                return;
            }
            else
            {
                Helpers.Divider();
                Helpers.WarningMessage("Login failed. Do you want to try again?");
            }

            bool tryLogin = CheckboxInput("Press 'y' for Yes or 'n' for No");
            if (tryLogin == false)
            {
                return;
            }

        }
    }
}
