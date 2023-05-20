namespace Views;

public class ReservationPage : ViewTemplate{
    public ShowModel? Show;
    public ReservationPage(ShowModel show = null) : base($"Make Reservation") { this.Show = show; }

    public override void Render()
    {
        base.Render();
        // code for the auditorium which returns seats
        // List<string> seats = Auditorium.GetSeats();

        //test code

    }
}
