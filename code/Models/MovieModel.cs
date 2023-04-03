using System.Text.Json.Serialization;


public class MovieModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    // Duration in minutes
    [JsonPropertyName("duration")]
    public int Duration { get; set; }
    
    [JsonPropertyName("summary")]
    public string Summary { get; set; }
    
    [JsonPropertyName("genres")]
    public List<string> Genres { get; set;}
    
    [JsonPropertyName("releasedate")]
    public object ReleaseDate {get;set;}


    public MovieModel(string title, int duration, string summary, List<string> genres, object releasedate, string id = null)
    {
        Id = string.IsNullOrWhiteSpace(id) ? Helpers.GenUid() : id;
        Title = title;
        Duration = duration;
        Summary = summary;
        Genres = genres;
        ReleaseDate = releasedate;
    }
}