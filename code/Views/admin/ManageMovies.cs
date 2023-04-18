namespace Views;
using System.Collections.Generic;

public class ManageMovies : ViewTemplate
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

                case "2":
                    ShowMoviesTable();
                    EditMovie();
                    break;

                case "3":
                    ShowMoviesTable();
                    DeleteMovie();
                    break;


                case "4":
                    ShowMoviesTable(true);
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
        while (true)
        {
            base.Render();
            string GoBack = Helpers.GoBack("adding a movie");
            if (GoBack == "back")
            {
                return;
            }
            if (GoBack == "continue")
            {
                string title = base.InputField("Movie title:");
                int duration = base.InputNumber("Movie duration (in minutes):");
                string summary = base.InputField("Movie summary:");
                List<string> genreList = base.InputMultiple("Movie genres:");
                string director = base.InputField("Movie director:");
                string releaseDate = (string)base.InputDate("Movie release date:", false);
                string showTime = (string)base.InputDate("Next Movie Showtime:", false);

                MovieModel NewMovie = new(title: title, duration: duration, summary: summary, genres: genreList, releasedate: releaseDate, showtime: showTime, director: director);

                var movies = _movieLogic.GetMovies();
                var newMovieList = AddModel<MovieModel>(movies, NewMovie);
                _movieLogic.SaveMovies(newMovieList);
                Helpers.SuccessMessage("Movie added!");
                Helpers.Continue();
            }
        }
    }


    private void ShowMoviesTable(bool pressContinue = false)
    {
        base.Render();
        var movies = _movieLogic.GetMovies();
        Console.WriteLine(_movieLogic.GenerateModelTable<MovieModel>(movies));

        if (pressContinue) Helpers.Continue();
    }


    private void EditMovie()
    {
        var movies = _movieLogic.GetMovies();
        if (movies.Count == 0)
        {
            Helpers.WarningMessage("You have no movies to edit.");
        }
        else
        {
            while (true)
            {
                string GoBack = Helpers.GoBack("editing a movie");
                if (GoBack == "back")
                {
                    return;
                }
                if (GoBack == "continue")
                {
                var updatedMovies = base.EditMovie(movies);
                _movieLogic.SaveMovies();
                Helpers.SuccessMessage("Movie updated!");
                }
            }
        }

        Helpers.Continue();
    }

    private void DeleteMovie()
    {
        var movies = _movieLogic.GetMovies();
        if (movies.Count == 0)
        {
            Helpers.WarningMessage("You have no movies to delete.");
        }
        else
        {
            while (true)
            {
                string GoBack = Helpers.GoBack("deleting a movie");
                if (GoBack == "back")
                {
                    return;
                }
                if (GoBack == "continue")
                {
                    base.DeleteModelFromList<MovieModel>(movies);
                    _movieLogic.SaveMovies();
                }
            }
        }

        Helpers.Continue();
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
