namespace Views;

public class ReservationsOverviewAdmin : ViewTemplate
{
    private ReservationLogic _reservationLogic = new();
    public ReservationsOverviewAdmin() : base("All the reservations") { }

    public override void Render()
    {
        base.Render();

        List<ReservationModel> reservations = _reservationLogic.GetReservations();
        if (reservations.Count == 0)
        {
            Helpers.WarningMessage("Currently there are no reservations.");
            Helpers.Continue();

            RouteHandeler.LastView();
        }

        List<ReservationModel> reservationsPastX = new();
        int timestampNow = Convert.ToInt32(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());

        while (true)
        {
            string days = InputField("How many days do you want to look back? (Enter * to see everything): ");
            int daysInSeconds;
            if (days == "*")
            {
                foreach (ReservationModel reservation in reservations)
                {
                    Console.WriteLine($"ID: {reservation.ID}, Show: {reservation.Show.Movie.Title}, Customer: {reservation.User.FirstName} {reservation.User.LastName}");
                }
                Helpers.Continue();
                RouteHandeler.LastView();
                break;
            }
            else
            {
                // Convert the given days into seconds (86400 seconds per day)
                int convertedDays;
                bool validNumber = int.TryParse(days, out convertedDays);

                if (!validNumber)
                {
                    Helpers.WarningMessage("Days are invalid. Try again");
                    Helpers.Continue();
                    continue;
                }
                // get the reservations based on the timestamp from today - the given days in seconds
                daysInSeconds = convertedDays * 86_400;
                reservationsPastX = reservations.Where(e => (timestampNow - daysInSeconds) < e.Show.Timestamp).ToList();

                // misschien in een tabel maken
                foreach (ReservationModel reservation in reservationsPastX)
                {
                    Console.WriteLine($"ID: {reservation.ID}, Show: {reservation.Show.Movie.Title}, Customer: {reservation.User.FirstName} {reservation.User.LastName}");
                }
                RouteHandeler.LastView();
                break;
            }
        }
        Helpers.Continue();
    }
}
