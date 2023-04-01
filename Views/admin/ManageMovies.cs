namespace Views;

class ManageMovies : ViewTemplate
{

    private MovieLogic _movieLogic = new();
    public ManageMovies() : base("Manage Movies")
    {
    }

    public override void Render()
    {
        base.Render();
        Options();
    }


    private void Options()
    {
        while (true)
        {
            base.Render();
            ShowMenu();
            string UserInput = Console.ReadLine();

            switch (UserInput)
            {
                case "1":
                    CreateMovieForm();
                    break;

                case "4":
                    return;

                default:
                    Console.WriteLine("Invalid input.");
                    Helpers.Continue();
                    break;
            }

        }

    }


    private void CreateMovieForm()
    {
        base.Render();
        string title = Inputs.InputField("Movie title:");
        int duration = Inputs.InputNumber("Movie duration (in minutes):");
        string summary = Inputs.InputField("Movie summary:");

        MovieModel NewMovie = new(title: title, duration: duration, summary: summary);
        _movieLogic.AddMovie(NewMovie);
        Helpers.Continue();
    }


    private void ShowMenu(){
        Console.Write(@"1. Add a movie
4. Back to dashboard.
> ");
    }



}