public class MovieLogic : LogicTemplate
{
    private MovieAccess _movieAccess;
    private List<MovieModel> _movieList;

    public MovieLogic(string overwritePath = null) : base()
    {
        _movieAccess = new(overwritePath);
        ReloadMovies();
    }

    private void ReloadMovies() => _movieList = _movieAccess.LoadAll();

    public void AddMovie(MovieModel movie)
    {
        _movieList.Add(movie);
        _movieAccess.WriteAll(_movieList);
    }

    public void EditMovie(int index, string propertyName, object updatedValue)
    {
        // case example:  _movieList[0].Duration = 120;
        var property = _movieList[index].GetType().GetProperty(propertyName);

        property.SetValue(_movieList[index], updatedValue);
        _movieAccess.WriteAll(_movieList);        
    }


    public void DeleteMovie(int id){
        _movieList.RemoveAt(id);
        _movieAccess.WriteAll(_movieList);
    }

    public void SaveMovies(List<MovieModel> movieList = null)
    {
        if (movieList != null)
        {
            _movieAccess.WriteAll(movieList);
        }
        else
        {
            _movieAccess.WriteAll(_movieList);
        }
    }

    public List<MovieModel> GetMovies()
    {
        ReloadMovies();
        return _movieList;
    }
}