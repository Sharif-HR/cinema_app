using System.Text.Json.Serialization;
using System.Reflection;


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

    // [JsonPropertyName("showtime")]
    // public object ShowTime {get;set;}
    // Moet datatime zijn of alleen time?




    public MovieModel(string title, int duration, string summary, List<string> genres, object releasedate, string id = null)
    {
        Id = string.IsNullOrWhiteSpace(id) ? Helpers.GenUid() : id;
        Title = title;
        Duration = duration;
        Summary = summary;
        Genres = genres;
        ReleaseDate = releasedate;
    }


    public List<string> GetAttributes()
    {
        List<string> attributes = new List<string>();
        PropertyInfo[] propertyInfos = this.GetType().GetProperties();
        foreach (PropertyInfo propertyInfo in propertyInfos)
        {
            if(propertyInfo.Name != "Id"){
                attributes.Add(propertyInfo.Name);
            }
        }
        return attributes;
    }
}