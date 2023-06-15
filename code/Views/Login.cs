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
            base.GoBackMsg();

            // if(Environment.GetEnvironmentVariable("ENVIRONMENT") == "development") {
            //     RouteHandeler.View("DashboardPage");
            // }

            string Email = base.InputField("Enter your email: ");
            string Password = base.InputPassword("Enter password: ");

            AccountModel UserAccount = accountsLogic.CheckLogin(Email, Password);


            if (UserAccount != null)
            {
                LocalStorage.WriteToStorage(UserAccount);
                RouteHandeler.View("DashboardPage");
            }
            else
            {
                Helpers.ErrorMessage("Login failed.");
                Helpers.Continue();
            }

        }
    }
}
