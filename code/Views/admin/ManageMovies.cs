namespace Views;
using System.Collections.Generic;

public class ManageMovies : ViewTemplate, IManage
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
                    AddForm();
                    break;

                case "2":
                    EditForm();
                    break;

                case "3":
                    DeleteForm();
                    break;


                case "4":
                    ShowMoviesTable(true);
                    break;

                case "5":
                    return;

                default:
                    Helpers.WarningMessage("Invalid input.");
                    Helpers.Continue();
                    break;
            }

        }

    }


    public void AddForm()
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
                string releaseDate = (string)base.InputDate("Movie release date:", false);
                string showTime = (string)base.InputDateTime("Next Movie Showtime:", false);

                MovieModel NewMovie = new(title: title, duration: duration, summary: summary, genres: genreList, releasedate: releaseDate, showtime: showTime);

                _movieLogic.AddMovie(NewMovie);
                Helpers.SuccessMessage("Movie added!");
                Helpers.Continue();
            }
        }
    }

    public void DeleteForm()
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
                ShowMoviesTable();
                string GoBack = Helpers.GoBack("deleting a movie");
                if (GoBack == "back")
                {
                    return;
                }
                if (GoBack == "continue")
                {
                    while (true)
                    {
                        movies = _movieLogic.GetMovies();
                        int movieId = InputNumber("Enter movie ID: ");

                        if (Helpers.HasIndexInList<MovieModel>(movieId, movies, false) == false)
                        {
                            Helpers.WarningMessage("Please enter a valid ID.");
                            continue;
                        }
                        _movieLogic.DeleteMovie(movieId--);
                        Helpers.SuccessMessage("Movie Deleted!");
                        Helpers.Continue();
                        break;
                    }
                }
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

    public List<string> MovieProperties()
    {
        List<string> movieProperties = new();
        foreach (var prop in typeof(MovieModel).GetProperties())
        {
            if (prop.Name != "Id")
            {
                movieProperties.Add(prop.Name);
            }
        }

        return movieProperties;
    }

    private List<string> Genres()
    {
        List<string> genres = new();
        genres.AddRange(new string[] { "Action", "Comedy", "Drama", "Horror", "Romance", "Thriller", "Sci-Fi", "Adventure", "Crime", "Fantasy" });
        return genres;
    }

    private List<string> InputGenre()
    {
        List<string> selectedGeneres = new();
        var genreList = Genres();

        while (true)
        {
            for (int i = 0; i < genreList.Count; i++)
            {
                Console.WriteLine($"{i+1}. {genreList[i]}");
            }

            int genresIndex = SelectFromModelList<string>(genreList, true, "genre");

            string currentGenre = genreList[genresIndex];

            if (selectedGeneres.Contains(currentGenre))
            {
                Helpers.WarningMessage("You already selected this genre please enter another one.");
                continue;
            }

            selectedGeneres.Add(currentGenre);
            bool extraGenre = CheckboxInput("Would you like to select another genre? Press 'y' for Yes or 'n' for No");
            if (extraGenre == false)
            {
                return selectedGeneres;
            }
        }
    }


    public void EditForm()
    {
        ShowMoviesTable();
        var movies = _movieLogic.GetMovies();
        int movieId = SelectFromModelList<MovieModel>(movies, true);
        var movieProperties = MovieProperties();

        // Print property of model
        for (int i = 0; i < movieProperties.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {movieProperties[i]}");
        }
        
        int propertyIndex;
        string chosenProperty;

        while (true)
        {
            propertyIndex = InputNumber("Select a property: (with number)");
            if (Helpers.HasIndexInList<string>(propertyIndex, movieProperties, false) == false)
            {
                Helpers.WarningMessage("please enter a valid property.");
                continue;
            }

            propertyIndex--;
            chosenProperty = movieProperties[propertyIndex];
            break;
        }


        object updatedValue = null;
        switch (chosenProperty.ToLower())
        {
            case "title":
                updatedValue = InputField("Enter title:");
                break;

            case "duration":
                updatedValue = InputNumber("Enter duration (in minutes):");
                break;

            case "summary":
                updatedValue = InputField("Enter movie description:");
                break;

            case "genres":
                updatedValue = InputGenre();
                break;

            case "releasedate":
                updatedValue = InputDate("Enter release date:", false);
                break;

            case "showtime":
                updatedValue = InputDateTime("Enter showtime:", false);
                break;

            default:
                break;
        }

        // ID-movie, property-name, updated-value
        _movieLogic.EditMovie(movieId, chosenProperty, updatedValue);
        Helpers.SuccessMessage("Movie updated!");
        Helpers.Continue();
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
                    // var updatedMovies = base.EditMovie(movies);
                    _movieLogic.SaveMovies();
                    Helpers.SuccessMessage("Movie updated!");
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
