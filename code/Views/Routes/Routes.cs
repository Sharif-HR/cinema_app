namespace Views;
public static class Routes {
    // protected static string[]? routes = GetAllRoutes();
    public static string[]? GetAllRoutes() {
        string[] baseFiles = Directory.GetFiles("Views");
        string[] adminFiles = Directory.GetFiles("Views/admin");
        string[] customerFiles = Directory.GetFiles("Views/customer");
        string[] allFiles = baseFiles.Concat(adminFiles).Concat(customerFiles).ToArray();

        return null;
    }

    public static void RouteNameToView(string routeName) {
        switch (routeName)
        {
            case "ManageMoviesPageAdmin":
                new ManageMovies().Render();
                break;
            case "MovieViewCustomerPage":
                new MovieView().Render();
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
            default:
                Console.WriteLine("Page doesn't exist");
                break;
        }
    }
}
