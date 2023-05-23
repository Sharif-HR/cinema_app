namespace Views;

public class ReservationByCustomerAdmin: ViewTemplate {
    private ReservationLogic _reservationLogic = new();
    public ReservationByCustomerAdmin() : base("Reservations") {}

    public override void Render()
    {
        base.Render();
        string customerEmail = InputField("Enter the customer's email: ");
        List<ReservationModel> reservations = _reservationLogic.GetReservations();
        List<ReservationModel> customerReservations = new();

        customerReservations = reservations.Where(r => r.User.EmailAddress == customerEmail).ToList();

        if(customerReservations.Count() == 0) {
            Console.WriteLine();
            bool tryAgain = CheckboxInput("No reservations have been found at this email. Do you want to try again? Y/n");

            if(tryAgain) {
                this.Render();
            }

            RouteHandeler.LastView();
        }

        foreach(ReservationModel reservation in customerReservations) {
            Console.WriteLine($"ID: {reservation.ID}, Email: {reservation.User.EmailAddress}");
        }
    }
}
