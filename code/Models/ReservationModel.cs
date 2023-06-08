using System.Collections.Generic;
using System.Text.Json.Serialization;

public class ReservationModel {
    [JsonPropertyName("id")]
    public string ID {get; set;}

    [JsonPropertyName("seats")]
    public List<SeatModel> Seats {get; set;}

    [JsonPropertyName("costs")]
    public double Costs {get; set;}

    [JsonPropertyName("refreshments")]
    public List<RefreshmentModel> Refreshments {get; set;}

    [JsonPropertyName("user")]
    public AccountModel User {get; set;}

    [JsonPropertyName("show")]
    public ShowModel Show {get; set;}

    public ReservationModel(string id, List<SeatModel> seats, double costs, List<RefreshmentModel> refreshments, AccountModel user, ShowModel show) {
        this.ID = id;
        this.Seats = seats;
        this.Costs = costs;
        this.Refreshments = refreshments;
        this.User = user;
        this.Show = show;
    }
}
