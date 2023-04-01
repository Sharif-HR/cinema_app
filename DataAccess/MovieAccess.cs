using System.Text.Json;

class MovieAccess : Access<MovieModel>
{
    public MovieAccess():base("Data/movies.json"){}

}