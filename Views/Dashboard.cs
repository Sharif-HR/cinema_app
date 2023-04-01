namespace Views;

class Dashboard : ViewTemplate
{
    //Shows the dashboard(similar to the Menu).
    //Options shown depends on the role of the user.
    private const string ADMINROLE = "admin";
    private const string CUSTOMERROLE = "customer";
    private string _userRole;
    private ManageMovies ManageMoviesPage = new();
    public Dashboard(string role) : base($"{role} - Dashboard")
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
        while(true){
            base.Render();
            ShowAdminMenu();

            string AdminInput = Console.ReadLine();
            switch(AdminInput){
                case "1":
                    Console.WriteLine("Comingsoon");
                    Helpers.Continue();
                    break;

                case "3":
                    ManageMoviesPage.Render();
                    break;
                
                case "5":
                    LogOutMsg();
                    return;
                
                default:
                    Helpers.Divider();
                    Console.WriteLine("Invalid input.");
                    Helpers.Continue();
                    break;
            }
        }
    }



    private void CustomerOptions()
    {
        while(true){
            base.Render();
            ShowCustomerMenu();

            string CustomerInput = Console.ReadLine();
            switch (CustomerInput)
            {
                case "1":
                    Console.WriteLine("Comingsoon");
                    Helpers.Continue();
                    break;

                case "3":
                    LogOutMsg();
                    return;

                default:
                    Helpers.Divider();
                    Console.WriteLine("Invalid input.");
                    Helpers.Continue();
                    break;
            }
        }

    }


    private void LogOutMsg(){
        Console.WriteLine("Logging out...");
        Thread.Sleep(700);
    }


    private void ShowRedirect()
    {
        Helpers.Divider();
        Console.WriteLine("Error: Something went wrong. You will be redirected to the main menu.");
        Helpers.Continue();
    }

    private void ShowAdminMenu(){
        Console.Write(@"1. View list of current movies.
2. View reservations.
3. Manage movies.
4. Manage refreshments.
5. Log out.
> ");
    }

    private void ShowCustomerMenu(){
        Console.Write(@"
1. View list of current movies.
2. View reservations.
3. Log out.
> ");
    }
}
