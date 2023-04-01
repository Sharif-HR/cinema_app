namespace Views;

class Login : ViewTemplate
{
    static private AccountsLogic accountsLogic = new AccountsLogic();

    public Login() : base("Login") { }

    public override void Render()
    {
        base.Render();
        string Email = base.InputField("Enter your email: ");
        string Password = base.InputPassword("Enter password: ");

        AccountModel UserAccount = accountsLogic.CheckLogin(Email, Password);


        if (UserAccount != null)
        {
            Dashboard DasboardPage = new(role:UserAccount.Role);
            DasboardPage.Render();
        }
        else
        {
            Helpers.Divider();
            Console.WriteLine("No account found with that email and/or password.");
            Helpers.Continue();
        }
    }
}
