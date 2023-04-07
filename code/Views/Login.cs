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
            string GoBack = Helpers.GoBack("logging in");
            if (GoBack == "back")
            {
                return;
            }
            if (GoBack == "continue")
            {
                string Email = base.InputField("Enter your email: ");
                string Password = base.InputPassword("Enter password: ");

                AccountModel UserAccount = accountsLogic.CheckLogin(Email, Password);

                if (UserAccount != null)
                {
                    Dashboard DasboardPage = new(role: UserAccount.Role);
                    DasboardPage.Render();
                    return;
                }
                else
                {
                    Helpers.Divider();
                    Helpers.WarningMessage("Login failed. Do you want to try again?");
                }

                bool tryLogin = CheckboxInput("Press 'y' for Yes or 'n' for No");
                if(tryLogin == false){
                    return;
                }
            }
        }
    }
}
