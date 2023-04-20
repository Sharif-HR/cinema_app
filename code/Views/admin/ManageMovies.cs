namespace Views;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class ManageMovies : ViewTemplate, IManage
{
    private MovieLogic _movieLogic = new();
    public ManageMovies() : base("Manage Movies") { }

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
                    ShowMoviesTable("yes");
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

            if (Helpers.GoBack("adding a movie") == true) { return; }

            string title = base.InputField("Movie title:");
            int duration = base.InputNumber("Movie duration (in minutes):");
            string summary = base.InputField("Movie summary:");
            List<string> genreList = InputGenre();
            string director = base.InputField("Movie director:");
            string releaseDate = (string)base.InputDate("Movie release date:", false);
            string showTime = (string)base.InputDateTime("Next Movie Showtime:", false);

            MovieModel NewMovie = new(title: title, duration: duration, summary: summary, genres: genreList, releasedate: releaseDate, director: director, showtime: showTime);

            _movieLogic.AddMovie(NewMovie);
            Helpers.SuccessMessage("Movie added!");
            Helpers.Continue();
        }
    }

    public void EditForm()
    {

        var movies = _movieLogic.GetMovies();

        if (movies.Count == 0)
        {
            Helpers.WarningMessage("You have no movies to edit.");
            Helpers.Continue();
            return;
        }

        while (true)
        {
            ShowMoviesTable("null");
            if (Helpers.GoBack("editing a movie") == true) { return; }
            Helpers.WarningMessage($"In order to select a movie to edit enter a number between 1 and {movies.Count}");
            int movieId = SelectFromModelList<MovieModel>(movies, true);
            var movieProperties = MovieProperties();
            movieProperties.Add("Exit");

            bool loopSelectedMovie = true;
            while (loopSelectedMovie)
            {
                movies = _movieLogic.GetMovies();

                base.Render();
                Helpers.WarningMessage("Selected movie:");
                Console.WriteLine($@"Title: {movies[movieId].Title}
Duration: {movies[movieId].Duration}
Summary: {movies[movieId].Summary}
Genres: {Helpers.ListToString(movies[movieId].Genres)}
Director: {movies[movieId].Director}
Release date: {movies[movieId].ReleaseDate}
Show time: {movies[movieId].ShowTime}");
                Helpers.Divider(false);


                Helpers.WarningMessage($"Enter a number between 1 and {movieProperties.Count} to update a property of this movie or to exit:");
                // Print property of model
                for (int i = 0; i < movieProperties.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {movieProperties[i]}");
                }

                int propertyIndex;
                string chosenProperty;

                propertyIndex = base.SelectFromModelList<string>(movieProperties, true, "property id");

                chosenProperty = movieProperties[propertyIndex];

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

                    case "director":
                        updatedValue = InputField("Enter director:");
                        break;

                    case "exit":
                        loopSelectedMovie = false;
                        break;

                    default:
                        break;
                }


                if (loopSelectedMovie)
                {
                    // ID-movie, property-name, updated-value
                    _movieLogic.EditMovie(movieId, chosenProperty, updatedValue);
                    Helpers.SuccessMessage("Movie updated!");
                    Helpers.Continue();
                }

            }
        }
    }

    public void DeleteForm()
    {
        while (true)
        {
            var movies = _movieLogic.GetMovies();
            if (movies.Count == 0)
            {
                Helpers.WarningMessage("You have no movies to delete.");
                Helpers.Continue();
                return;
            }
            if (Helpers.GoBack("deleting a movie") == true) { return; }

            ShowMoviesTable("null");
            movies = _movieLogic.GetMovies();
            int movieId = base.SelectFromModelList<MovieModel>(movies, true);

            _movieLogic.DeleteMovie(movieId);
            Helpers.SuccessMessage("Movie Deleted!");
            Helpers.Continue();
        }
    }



    private void ShowMoviesTable(string showmenu)
    {
        base.Render();
        var movies = _movieLogic.GetMovies();
        Console.WriteLine(_movieLogic.GenerateModelTable<MovieModel>(movies));

        if (showmenu == "yes")
        {
            SearchSortFilterMenu();
            string UserInput = Console.ReadLine();
            switch (UserInput)
            {
                case "1":
                    SearchMovie();
                    break;

                case "2":
                    SortMovies();
                    break;

                case "3":
                    FilterMovies();
                    break;

                case "4":
                    return;

                default:
                    Helpers.WarningMessage("Invalid input.");
                    Helpers.Continue();
                    break;
            }
        }
        if (showmenu == "continue")
        {
            Helpers.Continue();
        }
        if (showmenu == "null") { }

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
        genres.AddRange(new string[] { "Action", "Comedy", "Drama", "Horror", "Romance", "Thriller", "Sci-Fi", "Adventure", "Crime", "Fantasy", "Mystery" });
        return genres;
    }

    private List<string> InputGenre()
    {
        List<string> selectedGeneres = new();
        var genreList = Genres();

        while (true)
        {
            Console.WriteLine("Movie genre(s):");
            Helpers.WarningMessage($"Enter a number between 1 and {genreList.Count} to select a genre from the list below.");

            Helpers.Divider();
            for (int i = 0; i < genreList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {genreList[i]}");
            }
            Helpers.Divider();
            Helpers.WarningMessage($"Selected genres [{Helpers.ListToString(selectedGeneres)}]");


            int genresIndex = SelectFromModelList<string>(genreList, true, "genre");

            string currentGenre = genreList[genresIndex];

            if (selectedGeneres.Contains(currentGenre))
            {
                Helpers.WarningMessage("You already selected this genre please enter another one.");
                Helpers.Continue();
                base.Render();

                continue;
            }

            selectedGeneres.Add(currentGenre);
            bool extraGenre = CheckboxInput("Would you like to select another genre? Press 'y' for Yes or 'n' for No");

            if (extraGenre == false)
            {
                return selectedGeneres;
            }
            else
            {
                base.Render();

            }
        }
    }

    private void ShowMenu()
    {
        Console.Write(@"1. Add a movie
2. Edit Movie
3. Delete Movie
4. Show movies
5. Back to dashboard
> ");
    }
    private void SearchSortFilterMenu()
    {
        Console.Write(@"Please select an option:
1. Search for a movie
2. Sort movie list
3. Filter movie list
4. Exit
> ");
    }

    private void SearchMovie()
    {
        var movies = _movieLogic.GetMovies();
        base.Render();
        Console.WriteLine(_movieLogic.GenerateModelTable<MovieModel>(movies));

        Helpers.Divider();
        Console.WriteLine("Search movies by title");
        Helpers.Divider();

        Console.WriteLine("Enter the title of the movie:");
        Console.Write("> ");
        string InputTitle = Console.ReadLine();

        IEnumerable<MovieModel> FoundMovie =
            from movie in movies
            where Helpers.CaseInsensitiveContains(movie.Title, InputTitle)
            select movie;

        List<MovieModel> FoundMovies = new();
        foreach (MovieModel movie in FoundMovie)
        {
            FoundMovies.Add(movie);
        }

        base.Render();
        if (FoundMovies.Count == 0)
        {
            Helpers.WarningMessage("No movie(s) found with that name.");
            Helpers.Continue();
            ShowMoviesTable("yes");
        }
        else
        {
            Console.WriteLine(_movieLogic.GenerateModelTable<MovieModel>(FoundMovies));
            Helpers.Continue();
            ShowMoviesTable("yes");
        }
    }

    private void SortMovies()
    {
        var movies = _movieLogic.GetMovies();
        base.Render();
        Console.WriteLine(_movieLogic.GenerateModelTable<MovieModel>(movies));

        Helpers.Divider();
        Console.WriteLine("Sort movies");
        Helpers.Divider();

        var movieProperties = MovieProperties();
        movieProperties.Add("Exit");

        Helpers.WarningMessage($"Enter a number between 1 and {movieProperties.Count} to sort or to exit:");

        for (int i = 0; i < movieProperties.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {movieProperties[i]}");
        }

        int propertyIndex;
        string chosenProperty;

        propertyIndex = base.SelectFromModelList<string>(movieProperties, true, "property id");
        chosenProperty = movieProperties[propertyIndex];

        List<MovieModel> FoundMovies = new();

        Console.Write(@"1. Ascending
2. Descending");
        int AscendingDescending = base.InputNumber("\nEnter 1 to sort in ascending order or 2 to sort in descending order.");


        if (AscendingDescending == 1)
        {
            switch (chosenProperty.ToLower())
            {
                case "title":
                    var OrderByTitle = from m in movies
                                       orderby m.Title
                                       select m;

                    foreach (MovieModel movie in OrderByTitle)
                    {
                        FoundMovies.Add(movie);
                    }
                    break;

                case "duration":
                    var OrderByDuration = from m in movies
                                          orderby m.Duration
                                          select m;

                    foreach (MovieModel movie in OrderByDuration)
                    {
                        FoundMovies.Add(movie);
                    }
                    break;

                case "summary":
                    var OrderBySummary = from m in movies
                                         orderby m.Summary
                                         select m;

                    foreach (MovieModel movie in OrderBySummary)
                    {
                        FoundMovies.Add(movie);
                    }
                    break;

                case "genres":
                    var OrderByGenres = from m in movies
                                        orderby m.Genres.Count
                                        select m;

                    foreach (MovieModel movie in OrderByGenres)
                    {
                        FoundMovies.Add(movie);
                    }
                    break;

                case "director":
                    var OrderByDirector = from m in movies
                                          orderby m.Director
                                          select m;

                    foreach (MovieModel movie in OrderByDirector)
                    {
                        FoundMovies.Add(movie);
                    }
                    break;

                case "releasedate":
                    var OrderByReleaseDate = from m in movies
                                             orderby DateOnly.Parse(m.ReleaseDate)
                                             select m;

                    foreach (MovieModel movie in OrderByReleaseDate)
                    {
                        FoundMovies.Add(movie);
                    }
                    break;

                case "showtime":
                    var OrderByShowtime = from m in movies
                                          orderby DateTime.ParseExact(m.ShowTime, "HH:mm dd-MM-yyyy", new CultureInfo("nl-NL"))
                                          select m;
                    foreach (MovieModel film in OrderByShowtime)
                    {
                        FoundMovies.Add(film);
                    }
                    break;

                case "exit":
                    ShowMoviesTable("yes");
                    break;

                default:
                    return;
            }
        }
        if (AscendingDescending == 2)
        {
            switch (chosenProperty.ToLower())
            {
                case "title":
                    var OrderByTitle = from m in movies
                                       orderby m.Title descending
                                       select m;

                    foreach (MovieModel movie in OrderByTitle)
                    {
                        FoundMovies.Add(movie);
                    }
                    break;

                case "duration":
                    var OrderByDuration = from m in movies
                                          orderby m.Duration descending
                                          select m;

                    foreach (MovieModel movie in OrderByDuration)
                    {
                        FoundMovies.Add(movie);
                    }
                    break;

                case "summary":
                    var OrderBySummary = from m in movies
                                         orderby m.Summary descending
                                         select m;

                    foreach (MovieModel movie in OrderBySummary)
                    {
                        FoundMovies.Add(movie);
                    }
                    break;

                case "genres":
                    var OrderByGenres = from m in movies
                                        orderby m.Genres.Count descending
                                        select m;

                    foreach (MovieModel movie in OrderByGenres)
                    {
                        FoundMovies.Add(movie);
                    }
                    break;

                case "director":
                    var OrderByDirector = from m in movies
                                          orderby m.Director descending
                                          select m;

                    foreach (MovieModel movie in OrderByDirector)
                    {
                        FoundMovies.Add(movie);
                    }
                    break;

                case "releasedate":
                    var OrderByReleaseDate = from m in movies
                                             orderby DateOnly.Parse(m.ReleaseDate) descending
                                             select m;

                    foreach (MovieModel movie in OrderByReleaseDate)
                    {
                        FoundMovies.Add(movie);
                    }
                    break;

                case "showtime":
                    var OrderByShowtime = from m in movies
                                          orderby DateTime.ParseExact(m.ShowTime, "HH:mm dd-MM-yyyy", new CultureInfo("nl-NL"))
                                          select m;
                    foreach (MovieModel film in OrderByShowtime)
                    {
                        FoundMovies.Add(film);
                    }
                    break;

                case "exit":
                    ShowMoviesTable("yes");
                    break;

                default:
                    return;
            }
        }
        base.Render();
        if (FoundMovies.Count == 0)
        {
            Helpers.WarningMessage("No movie(s) found with that name.");
            Helpers.Continue();
            ShowMoviesTable("null");
        }
        else
        {
            Console.WriteLine(_movieLogic.GenerateModelTable<MovieModel>(FoundMovies));
            Helpers.Continue();
            ShowMoviesTable("yes");
        }
    }

    private void FilterMovies()
    {
        var movies = _movieLogic.GetMovies();
        base.Render();
        Console.WriteLine(_movieLogic.GenerateModelTable<MovieModel>(movies));

        Helpers.Divider();
        Console.WriteLine("Filter movies by genre");
        Helpers.Divider();

        var genreList = Genres();

        Helpers.WarningMessage($"Enter a number between 1 and {genreList.Count} to select a genre from the list below.");
        Console.WriteLine("Movie genre(s):");

        Helpers.Divider();
        for (int i = 0; i < genreList.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {genreList[i]}");
        }

        int genresIndex = SelectFromModelList<string>(genreList, true, "genre");
        string selectedGenere = genreList[genresIndex];

        IEnumerable<MovieModel> FoundMovie =
            from movie in movies
            where movie.Genres.Contains(selectedGenere)
            select movie;

        List<MovieModel> FoundMovies = new();
        foreach (MovieModel movie in FoundMovie)
        {
            FoundMovies.Add(movie);
        }

        base.Render();
        if (FoundMovies.Count == 0)
        {
            Helpers.WarningMessage("No movie(s) found with that genre.");
            Helpers.Continue();
            ShowMoviesTable("yes");
        }
        else
        {
            Console.WriteLine(_movieLogic.GenerateModelTable<MovieModel>(FoundMovies));
            Helpers.Continue();
            ShowMoviesTable("yes");
        }
    }
}
