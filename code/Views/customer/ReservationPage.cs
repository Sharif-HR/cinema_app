namespace Views;

public class ReservationPage : ViewTemplate{
    public ShowModel? Show;
    private ReservationLogic _reservationLogic = new();
    public ReservationPage(ShowModel show = null) : base($"Make Reservation") { this.Show = show; }

    public override void Render()
    {
        base.Render();
        // code for the auditorium which returns seats
        // List<string> seats = Auditorium.GetSeats();

        //test code
        List<ReservationModel> reservations = _reservationLogic.GetReservations();

        List<string> seats = new(){"a1", "a2", "a3"};

        // create all the data
        string id = GenRandId();
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

        if(chosenRefreshments.Count > 0) {
            for (int i = 0; i < chosenRefreshments.Count; i++)
            {
                costs += chosenRefreshments[i].Price;
            }
        }

        ReservationModel reservation = new(id, seatsString, costs, chosenRefreshments, (AccountModel)LocalStorage.GetAuthenticatedUser(), this.Show);

        reservations.Add(reservation);

        _reservationLogic.UpdateReservations(reservations);

        Helpers.SuccessMessage($"You have made a reservation for {reservation.Show.Movie.Title} {Helpers.TimeStampToGMEFormat(reservation.Show.Timestamp, "HH:mm")}");
        Console.WriteLine("Creating your receipt...");
        Thread.Sleep(2000);
        _reservationLogic.PrintReceipt(reservation);
    }

    private string GenRandId()
    {
        List<ReservationModel> vals = _reservationLogic.GetReservations();
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string nums = "0123456789";

        string ID;
        ID = new string(Enumerable.Repeat(chars, 3).Select(s => s[random.Next(s.Length)]).ToArray());
        ID += new string(Enumerable.Repeat(nums, 3).Select(s => s[random.Next(s.Length)]).ToArray());
        ID += new string(Enumerable.Repeat(chars, 1).Select(s => s[random.Next(s.Length)]).ToArray());

        var IDCheck = from val in vals
                        where val.ID == ID
                        select val;

        if(IDCheck.Count() != 0){
                return GenRandId();
        }
        else{
            return ID;
        }
        return ID;
    }
}
