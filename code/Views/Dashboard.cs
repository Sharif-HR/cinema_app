namespace Views;

public class Dashboard : ViewTemplate
{
    //Shows the dashboard(similar to the Menu).
    //Options shown depends on the role of the user.
    private const string ADMINROLE = "admin";
    private const string CUSTOMERROLE = "customer";
    private string _userRole;
    private ManageMovies ManageMoviesPage = new();
    private MovieView CustomerMoviePage = new();
    public Dashboard(string role) : base($"{Helpers.CapitalizeFirstLetter(role)} - Dashboard")
    {
        this._userRole = role;
    }

    public override void Render()
    {
        base.Render();

        switch (_userRole.ToLower())
        {
            case ADMINROLE:
                AdminOptions();
                break;

            case CUSTOMERROLE:
                CustomerOptions();
                break;

            default:
                ShowRedirect();
                return;
        }
    }


    private void AdminOptions()
    {
        while (true)
        {
            base.Render();
            ShowAdminMenu();

            string AdminInput = InputField("Enter a number:");
            switch (AdminInput)
            {
                case "1":
                    Helpers.WarningMessage("Coming soon!");
                    Helpers.Continue();
                    break;

                case "2":
                    ManageMoviesPage.Render();
                    break;

                case "3":
                    Helpers.WarningMessage("Coming soon!");
                    Helpers.Continue();
                    break;
                case "4":
                    LogOutMsg();
                    return;

                default:
                    Helpers.Divider();
                    Helpers.WarningMessage("Invalid input. Please enter one of the number shown above.");
                    Helpers.Continue();
                    break;
            }
        }
    }



    private void CustomerOptions()
    {
        while (true)
        {
            base.Render();
            ShowCustomerMenu();

            string CustomerInput = InputField("Enter a number:");
            switch (CustomerInput)
            {
                case "1":
                    CustomerMoviePage.Render();
                    break;
                
                case "2":
                    Console.WriteLine("Coming soon");
                    Helpers.Continue();
                    break;

                case "3":
                    LogOutMsg();
                    return;

                default:
                    Helpers.Divider();
                    Helpers.WarningMessage("Invalid input. Please enter one of the numbers shown above.");
                    Helpers.Continue();
                    break;
            }
        }

    }


    private void LogOutMsg()
    {
        Console.WriteLine("Logging out...");
        Thread.Sleep(1200);
    }


    private void ShowRedirect()
    {
        Helpers.Divider();
        Console.WriteLine("Error: Something went wrong. You will be redirected to the main menu.");
        Helpers.Continue();
    }

    private void ShowAdminMenu()
    {
        Console.WriteLine(@"1. View reservations.
2. Manage movies.
3. Manage refreshments.
4. Log out.");
        Helpers.Divider(false);
    }

    private void ShowCustomerMenu()
    {
        Console.WriteLine(@"
1. View movies.
2. View reservations.
3. Log out.");
        Helpers.Divider(false);

    }
}