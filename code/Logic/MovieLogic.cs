using ConsoleTables;

public class MovieLogic
{
    private MovieAccess _movieAccess;
    private List<MovieModel> _movieList;

    public MovieLogic(string overwritePath=null){
        _movieAccess = new(overwritePath);
        ReloadMovies();
    }

    private void ReloadMovies() => _movieList = _movieAccess.LoadAll();

    public void AddMovie(MovieModel movie)
    {
        _movieList.Add(movie);
        _movieAccess.WriteAll(_movieList);
        Console.WriteLine("Movie Added!");
    }

    public void SaveMovies()
    {
        _movieAccess.WriteAll(_movieList);
    }

    public List<MovieModel> GetMovies()
    {
        ReloadMovies();
        return _movieList;
    }


    public ConsoleTable GenerateMoviesTable(bool consolePrint = false)
    {
        ConsoleTable movieTable;
        ReloadMovies();

        var options = new ConsoleTableOptions
        {
            Columns = new[] { "ID", "Title", "Duration", "Summary", "Genres", "Releasedate" },
            EnableCount = false
        };

        movieTable = new ConsoleTable(options);
        for(int i = 0; i < _movieList.Count; i++){
            movieTable.AddRow(
                i+1,
                _movieList[i].Title,
                _movieList[i].Duration,
                Helpers.TruncateString(_movieList[i].Summary, 15),
                Helpers.TruncateString(Helpers.ListToString(_movieList[i].Genres), 15),
                _movieList[i].ReleaseDate
            );
        }
        
        if(consolePrint) Console.WriteLine(movieTable);
        
        return movieTable;
    }


}