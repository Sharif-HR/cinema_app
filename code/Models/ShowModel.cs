using System.Text.Json.Serialization;

public class ShowModel {
    [JsonPropertyName("showId")]
    public string showId {get; set;}

    [JsonPropertyName("timestamp")]
    public int Timestamp {get; set;} // timestamp
    [JsonPropertyName("numberOfSeats")]
    public int NumberOfSeats {get; set;}
    [JsonPropertyName("movie")]
    public MovieModel Movie {get; set;}

    public ShowModel(string showId, int timestamp, int numberOfSeats, MovieModel movie) {
        this.showId = showId;
        this.Timestamp = timestamp;
        this.NumberOfSeats = numberOfSeats;
        this.Movie = movie;
    }
}
