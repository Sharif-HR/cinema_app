namespace Views;

public class AdminCancelReservationPage : ViewTemplate
{
    private ReservationLogic _reservationLogic = new();
    private ShowLogic _showLogic = new();
    public AdminCancelReservationPage(ShowModel show = null) : base($"Cancel a reservation") { }

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

        while (true)
        {
            Console.WriteLine("Please enter your unique reservation code or leave blank to return:");
            string uniqueCode = Console.ReadLine();

            if (uniqueCode == "")
            {
                break;
            }

            var reservation = from res in reservations
                              where (res.ID == uniqueCode)
                              select res;

            if (reservation.Count() == 0)
            {
                Console.WriteLine("No reservation was found with this code...");
                Thread.Sleep(5000);
                continue;
            }

            ReservationModel customerReservation = reservation.ElementAt(0);

            if (DateTime.Parse(Helpers.TimeStampToGMEFormat(customerReservation.Show.Timestamp)) > DateTime.Now)
            {

                _reservationLogic.DeleteReservation(customerReservation, reservations);
                var bookedShow = _showLogic.GetShows().FirstOrDefault(s => s.showId == customerReservation.Show.showId);
                List<string> takenSeats = bookedShow.TakenSeats;

                foreach (SeatModel seat in customerReservation.Seats)
                {
                    var seatNumber = seat.Column + 1;
                    var row = seat.Row + 1;

                    bool foundSeat = takenSeats.Contains($"{row}-{seatNumber}");
                    if (foundSeat)
                    {
                        takenSeats.Remove($"{row}-{seatNumber}");
                        _showLogic.EditShow(bookedShow, false);
                    }
                }

                Console.WriteLine("Reservation cancelled...");
                Thread.Sleep(2000);
                break;
            }
            else
            {
                Console.WriteLine("You can't cancel this reservation anymore...");
                Thread.Sleep(2000);
            }
        }
    }
}