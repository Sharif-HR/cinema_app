public class ReservationModel {
    public string ID;
    public string MovieId;
    public string Seats;
    public double Costs;
    public List<RefreshmentModel> Refreshments;

    public ReservationModel(string id, string movieID, string seats, double costs, List<RefreshmentModel> refreshments) {
        this.ID = id;
        this.MovieId = movieID;
        this.Seats = seats;
        this.Costs = costs;
        this.Refreshments = refreshments;
    }
}
