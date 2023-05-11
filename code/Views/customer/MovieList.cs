namespace Views;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
                    SearchMovie();
                    break;

                case "3":
                    SortMovies();
                    break;

                case "4":
                    FilterMovies();
                    break;

                case "5":
                    return;

                default:
                    Helpers.WarningMessage("Invalid input");
                    Helpers.Continue();
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
2. Search for a movie.
3. Sort movie list.
4. Filter movie list.
5. Back to dashboard.");
        Helpers.Divider(false);
    }
    private List<string> Genres()
    {
        List<string> genres = new();
        genres.AddRange(new string[] { "Action", "Comedy", "Drama", "Horror", "Romance", "Thriller", "Sci-Fi", "Adventure", "Crime", "Fantasy", "Mystery" });
        return genres;
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

    private void SearchMovie()
    {
        var movies = _movieLogic.GetMovies();
        base.Render();
        Console.WriteLine(_movieLogic.GenerateModelTable<MovieModel>(movies));

        Helpers.Divider();
        Console.WriteLine("Search movies");
        Helpers.Divider();

        Console.WriteLine("Enter a search term:");
        Console.Write("> ");
        string SearchKey = Console.ReadLine();

        IEnumerable<MovieModel> FoundMovie =
            from movie in movies
            where Helpers.CaseInsensitiveContains(movie.Title, SearchKey) || Helpers.CaseInsensitiveContains(movie.Summary, SearchKey) || Helpers.CaseInsensitiveContains(movie.ReleaseDate, SearchKey) || Helpers.CaseInsensitiveContains(movie.ShowTime, SearchKey) || Helpers.CaseInsensitiveContains(movie.Director, SearchKey) || Helpers.CaseInsensitiveContains(movie.Duration.ToString(), SearchKey)
            select movie;

        IEnumerable<MovieModel> FoundMovie2 =
            from movie in movies
            where movie.Genres.Contains(SearchKey) == true
            select movie;

        List<MovieModel> FoundMovies = new();
        foreach (MovieModel movie in FoundMovie)
        {
            FoundMovies.Add(movie);
        }
        foreach (MovieModel movie in FoundMovie2)
        {
            FoundMovies.Add(movie);
        }

        base.Render();
        if (FoundMovies.Count == 0)
        {
            Helpers.WarningMessage("No movie(s) found.");
            Helpers.Continue();
        }
        else
        {
            Console.WriteLine(_movieLogic.GenerateModelTable<MovieModel>(FoundMovies));
            Helpers.Continue();
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

        if (propertyIndex != 7)
        {
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
                                                 orderby DateOnly.ParseExact(m.ReleaseDate, "dd-MM-yyyy", new CultureInfo("nl-NL"))
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
                                                 orderby DateOnly.ParseExact(m.ReleaseDate, "dd-MM-yyyy", new CultureInfo("nl-NL")) descending
                                                 select m;

                        foreach (MovieModel movie in OrderByReleaseDate)
                        {
                            FoundMovies.Add(movie);
                        }
                        break;

                    case "showtime":
                        var OrderByShowtime = from m in movies
                                              orderby DateTime.ParseExact(m.ShowTime, "HH:mm dd-MM-yyyy", new CultureInfo("nl-NL")) descending
                                              select m;
                        foreach (MovieModel film in OrderByShowtime)
                        {
                            FoundMovies.Add(film);
                        }
                        break;

                    case "exit":
                        break;

                    default:
                        return;
                }
            }
        }

        else { return; }
        base.Render();
        if (FoundMovies.Count == 0)
        {
            Helpers.WarningMessage("No movie(s) found with that name.");
            Helpers.Continue();
        }
        else
        {
            Console.WriteLine(_movieLogic.GenerateModelTable<MovieModel>(FoundMovies));
            Helpers.Continue();
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
        }
        else
        {
            Console.WriteLine(_movieLogic.GenerateModelTable<MovieModel>(FoundMovies));
            Helpers.Continue();
        }
    }
}