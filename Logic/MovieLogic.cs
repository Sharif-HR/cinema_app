class MovieLogic
{
    private MovieAccess _movieAccess = new();
    private List<MovieModel> _movieList;

    public MovieLogic() => ReloadMovies();

    private void ReloadMovies() => _movieList = _movieAccess.LoadAll();

    public void AddMovie(MovieModel movie)
    {
        _movieList.Add(movie);
        _movieAccess.WriteAll(_movieList);
        Console.WriteLine("Movie Added!");
    }
}