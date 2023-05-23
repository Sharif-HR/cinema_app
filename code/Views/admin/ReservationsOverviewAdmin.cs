namespace Views;

public class ReservationsOverviewAdmin : ViewTemplate {
    private ReservationAccess _reservationAccess = new();
    public ReservationsOverviewAdmin() : base("All the reservations") {}

    public override void Render()
    {
        base.Render();

        List<ReservationModel> reservations = _reservationAccess.LoadAll();
        List<ReservationModel> reservationsPastX = new();
        int timestampNow = Convert.ToInt32(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());

        while (true)
        {
            string days = InputField("How many days do you want to look back? (Enter * to see everything)");
            int daysInSeconds;
            if(days == "*") {
                foreach (ReservationModel reservation in reservations)
                {
                    Console.WriteLine($"ID: {reservation.ID}, Show: {reservation.Show.Movie.Title}, Customer: {reservation.User.FirstName} {reservation.User.LastName}");
                }
                break;
            } else {
                try
                {
                    // Convert the given days into seconds (86400 seconds per day)
                    daysInSeconds = Convert.ToInt32(days) * 86_400;
                    // get the reservations based on the timestamp from today - the given days in seconds
                    reservationsPastX = reservations.Where(e => (timestampNow - daysInSeconds) < e.Show.Timestamp).ToList();

                    // misschien in een tabel maken
                    foreach (ReservationModel reservation in reservationsPastX)
                    {
                        Console.WriteLine($"ID: {reservation.ID}, Show: {reservation.Show.Movie.Title}, Customer: {reservation.User.FirstName} {reservation.User.LastName}");
                    }
                    break;
                }
                catch (OverflowException)
                {
                    Helpers.ErrorMessage("The number you have entered is to big. Try again with another, smaller number");
                }
            }
        }
    }
}
