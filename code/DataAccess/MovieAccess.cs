public class MovieAccess : Access<MovieModel>
{
    public MovieAccess(string overwritePath = null) : base("data/movies.json", overwritePath) { }

}
