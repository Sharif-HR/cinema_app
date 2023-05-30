namespace Views;

public class ReservationsAdmin: ViewTemplate {
    public ReservationsAdmin() : base("Reservations") {}

    public override void Render()
    {
        base.Render();
        List<string> menuOptions = new(){"See all reservations", "See reservations by customer name", "Cancel a reservation"};
        MenuList(menuOptions, this);
    }
}
