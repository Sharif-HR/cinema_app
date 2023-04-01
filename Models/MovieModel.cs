using System.Text.Json.Serialization;


class MovieModel
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

    // genres
    // releaseDate


    public MovieModel(string title, int duration, string summary, string id = null)
    {
        Id = string.IsNullOrWhiteSpace(id) ? Helpers.GenUid() : id;
        Title = title;
        Duration = duration;
        Summary = summary;
    }
}