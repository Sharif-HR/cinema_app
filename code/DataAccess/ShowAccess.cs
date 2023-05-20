public class ShowAccess : Access<ShowModel> {
    public ShowAccess(string overwritePath = null) : base("data/shows.json", overwritePath) {}

}
