namespace Views;

public class Dashboard : ViewTemplate
{
    //Shows the dashboard(similar to the Menu).
    //Options shown depends on the role of the user.
    private const string ADMINROLE = "admin";
    private const string CUSTOMERROLE = "customer";
    private string _userRole;
    private ManageMovies ManageMoviesPage = new();
    private ManageRefreshments ManageRefreshmentsPage = new();
    private MovieList CustomerMoviePage = new();
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
        }
    }



    private void CustomerOptions()
    {
        while (true)
        {
            base.Render();
            ShowCustomerMenu();
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
        List<string> options = new() { "Manage Reservations", "Manage Movies", "Manage Refreshments", "Manage Shows", "Logout" };
        MenuList(options, this, "DashboardPage");
        Helpers.Divider(false);
    }

    private void ShowCustomerMenu()
    {
        List<string> options = new() { "Make Reservation", "My Reservations", "Cancel Reservation", "Movies Overview", "Logout" };
        MenuList(options, this, "DashboardPage");
    }
}
