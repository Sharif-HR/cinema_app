namespace Views;

public class MovieList : ViewTemplate
{
    private MovieLogic _movieLogic = new();
    public MovieList() : base("Movies list") { }

    public override void Render()
    {
        while (true)
        {
            base.Render();
            RenderMovieList();

            ShowOptions();
            string CustomerInput = InputField("Enter a number:");
            switch (CustomerInput)
            {
                case "1":
                    int selectedMovieId = SelectMovie();
                    SelectedMovie SingleMoviePage = new(selectedMovieId);
                    SingleMoviePage.Render();
                    break;

                case "2":
                    return;

                default:
                    Helpers.WarningMessage("Invalid input");
                    break;
            }

        }

    }

    private void RenderMovieList()
    {
        base.Render();
        Console.WriteLine(_movieLogic.GenerateModelTable<MovieModel>(_movieLogic.GetMovies()));
    }

    private int SelectMovie()
    {
        while (true)
        {
            int selectedMovieID = base.InputNumber("Enter movie ID to select a movie:", false);

            var movies = _movieLogic.GetMovies();
            bool foundMovie = Helpers.HasIndexInList<MovieModel>(selectedMovieID, movies, false);
            selectedMovieID--;

            if (!foundMovie)
            {
                Helpers.WarningMessage("Movie not found.");
                Helpers.Continue();
                continue;
            }

            return selectedMovieID;
        }
    }


    private void ShowOptions()
    {
        Console.WriteLine(@"
1. Select a movie.
2. Back to dashboard.");
        Helpers.Divider(false);
    }



}