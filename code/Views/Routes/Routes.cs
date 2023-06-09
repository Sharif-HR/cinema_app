using Views;
namespace Views;
public static class Routes
{
    // protected static string[]? routes = GetAllRoutes();
    public static string[]? GetAllRoutes()
    {
        string[] baseFiles = Directory.GetFiles("Views");
        string[] adminFiles = Directory.GetFiles("Views/admin");
        string[] customerFiles = Directory.GetFiles("Views/customer");
        string[] allFiles = baseFiles.Concat(adminFiles).Concat(customerFiles).ToArray();

        return null;
    }

    public static void RouteNameToView(string routeName)
    {
        switch (routeName)
        {
            case "ManageMoviesPageAdmin":
                new ManageMovies().Render();
                break;
            case "MoviesOverviewPageCustomer":
                new MovieList().Render();
                break;
            case "DashboardPage":
                new Dashboard(LocalStorage.GetAuthenticatedUser().Role).Render();
                break;
            case "LoginPage":
                new Login().Render();
                break;
            case "RegisterPage":
                new Register().Render();
                break;
            case "MenuPage":
                new Menu().Render();
                break;
            case "ManageReservationsPageAdmin":
                new ReservationsAdmin().Render();
                break;
            case "SeereservationsbycustomernamePageAdmin":
                new ReservationByCustomerAdmin().Render();
                break;
            case "SeeallreservationsPageAdmin":
                new ReservationsOverviewAdmin().Render();
                break;
            case "MakeReservationPageCustomer":
                new ShowOverview().Render();
                break;
            case "ShowsbydatePageCustomer":
                new ShowsByDate().Render();
                break;
            case "LogoutPage":
                new AccountsLogic().Logout();
                break;
            case "ManageRefreshmentsPageAdmin":
                new ManageRefreshments().Render();
                break;
            case "AboutusPage":
                new AboutUs().Render();
                break;
            case "CancelReservationPageCustomer":
                new CancelReservationPage().Render();
                break;
            case "ManageShowsPageAdmin":
                new ManageShows().Render();
                break;
            // case "ShowEventHandeler":
            //     new ShowEventHandeler(LocalStorage.GetItem("SHOW_OPTION").ToString()).Render();
            //     break;
            case "CancelareservationPageAdmin":
                new AdminCancelReservationPage().Render();
                break;
            case "MyReservationsPageCustomer":
                new ReservationOverviewCustomer().Render();
                break;
            default:
                Console.WriteLine("Page doesn't exist");
                List<string> history = LocalStorage.localStorage["history"];
                history.RemoveAt(history.Count - 1);
                LocalStorage.WriteToStorage();
                RouteHandeler.LastView();
                break;
        }
    }

    // public static void RouteNameToView(string name) {
    //     Activator.CreateInstance(name);
    // }
}
