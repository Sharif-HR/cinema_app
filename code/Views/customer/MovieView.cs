namespace Views;

public class MovieView : ViewTemplate
{
    private MovieLogic _movieLogic = new();
    public MovieView() : base("View movie lists") {}

    public override void Render()
    {
        base.Render();
        CustomerOptions();
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
                    base.Render();
                    var movies = _movieLogic.GetMovies();
                    Console.WriteLine(_movieLogic.GenerateModelTable<MovieModel>(movies));
                    Helpers.Continue();
                    break;

                case "2":
                    Console.WriteLine("Coming soon");
                    Helpers.Continue();
                    break;

                case "3":
                    return;

                default:
                    Helpers.Divider();
                    Helpers.WarningMessage("Invalid input. Please enter one of the numbers shown above.");
                    Helpers.Continue();
                    break;
            }
        }

    }
    private void ShowCustomerMenu()
    {
        Console.WriteLine(@"
1. View list of current movies.
2. View list of movies available in the future.
3. Back to dashboard.");
        Helpers.Divider(false);
    }
}