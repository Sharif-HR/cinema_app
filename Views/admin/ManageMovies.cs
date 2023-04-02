namespace Views;
using System.Collections.Generic;

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
                    ShowMovies();
                    break;

                case "5":
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
        string title = base.InputField("Movie title:");
        int duration = base.InputNumber("Movie duration (in minutes):");
        string summary = base.InputField("Movie summary:");
        List<string> genreList = base.InputMultiple("Movie genres:");
        var releaseDate = base.InputDate("Movie release date:", false);


        MovieModel NewMovie = new(title: title, duration: duration, summary: summary, genres: genreList, releasedate: releaseDate);
        _movieLogic.AddMovie(NewMovie);
        Helpers.Continue();
    }


    private void ShowMovies()
    {
        base.Render();
        var movies = _movieLogic.GetMovies();


        for (int i = 0; i < movies.Count; i++)
        {
            // Console.WriteLine("{0,-10} {1,-30} {2,-10} {3,-50} {4,-50} {5,-50}", i + 1, movies[i].Title, movies[i].Duration, movies[i].Summary, Helpers.ListToString(movies[i].Genres), movies[i].ReleaseDate);
            string row = String.Format("|{0,5}|{1,30}|{2,5}|{3,10}|{4,50}|{4,12}|", i + 1, movies[i].Title, movies[i].Duration, movies[i].Summary, Helpers.ListToString(movies[i].Genres), movies[i].ReleaseDate);
            Console.WriteLine(row);
        }
        Helpers.Continue();
        // SelectMovie();
    }
    

    private void SelectMovie()
    {
        bool loop = true;
        while (loop)
        {
            int movieIndex = base.InputNumber("Enter movie ID:");
            var movies = _movieLogic.GetMovies();

            if (movies.Count < movieIndex || movieIndex < 0)
            {
                Helpers.WarningMessage("Enter a correct ID");
            }
            else
            {
                Console.WriteLine("edit title");
                string input = Console.ReadLine();
                movies[movieIndex].Title = input;
                _movieLogic.SaveMovies();
                loop = false;
            }

        }
    }


    private void ShowMenu()
    {
        Console.Write(@"1. Add a movie
2. Edit Movie
3. Delete Movie
4. Show movies.
5. Back to dashboard.
> ");
    }
}
