public class ReservationAccess : Access<ReservationModel> {
    public ReservationAccess(string overwritePath = null) : base("data/reservations.json", overwritePath) {}


}
