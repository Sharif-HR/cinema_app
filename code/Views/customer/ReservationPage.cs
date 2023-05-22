namespace Views;

public class ReservationPage : ViewTemplate{
    public ShowModel? Show;
    private ReservationAccess _reservationAccess = new();
    public ReservationPage(ShowModel show = null) : base($"Make Reservation") { this.Show = show; }

    public override void Render()
    {
        base.Render();
        // code for the auditorium which returns seats
        // List<string> seats = Auditorium.GetSeats();

        //test code
        List<ReservationModel> reservations = _reservationAccess.LoadAll();

        List<string> seats = new(){"a1", "a2", "a3"};

        // create all the data
        string id = Helpers.GenUid();
        string seatsString = string.Join(',', seats);
        double costs = 0.0;
        costs += 15 * seats.Count();

        List<RefreshmentModel> chosenRefreshments = new();
        while (true)
        {
            bool wantsSnack = CheckboxInput("Do you want to add a snack? Enter y or n");
            if(!wantsSnack) {
                break;
            }
            // refreshment list
            chosenRefreshments.Add(RefreshmentsList(this));
        }

        for (int i = 0; i < chosenRefreshments.Count; i++)
        {
            costs += chosenRefreshments[i].Price;
        }

        ReservationModel reservation = new(id, seatsString, costs, chosenRefreshments, (AccountModel)LocalStorage.GetAuthenticatedUser(), this.Show);

        reservations.Add(reservation);

        _reservationAccess.WriteAll(reservations);

        Helpers.SuccessMessage($"You have made a reservation for {reservation.Show.Movie.Title} {Helpers.TimeStampToGMEFormat(reservation.Show.Timestamp, "HH:mm")}");
        Console.WriteLine("Creating your receipt...");
        Thread.Sleep(2000);
        _reservationAccess.PrintReceipt(reservation);

    }


}
