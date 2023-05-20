namespace Views;

public class ReservationOverview : ViewTemplate {
    private string id;
    public ReservationOverview() : base("Reservation") { this.id = id; }

    public override void Render()
    {
        base.Render();
        Console.WriteLine(id);
    }
}
