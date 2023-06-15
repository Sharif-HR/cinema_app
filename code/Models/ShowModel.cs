using System.Text.Json.Serialization;

public class ShowModel
{
    [JsonPropertyName("showId")]
    public string showId { get; set; }

    [JsonPropertyName("timestamp")]
    public int Timestamp { get; set; } // timestamp
    [JsonPropertyName("takenSeats")]
    public List<string> TakenSeats { get; set; }

    [JsonPropertyName("numberOfSeats")]
    public int NumberOfSeats { get; set; }

    [JsonPropertyName("movie")]
    public MovieModel Movie { get; set; }

    public ShowModel(int timestamp, int numberOfSeats, List<string> takenSeats, MovieModel movie, string showId = null)
    {

        if (showId == null)
        {
            this.showId = Helpers.GenUid();
        }
        else
        {
            this.showId = showId;
        }

        this.Timestamp = timestamp;
        this.NumberOfSeats = numberOfSeats;
        this.TakenSeats = takenSeats;
        this.Movie = movie;
    }
}
