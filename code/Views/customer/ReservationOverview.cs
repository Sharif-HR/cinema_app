namespace Views;
using System;
public class ReservationOverviewCustomer : ViewTemplate
{
    private ReservationLogic _reservationLogic = new();
    public ReservationOverviewCustomer() : base("My Reservations") { }

    public override void Render()
    {
        base.Render();

        List<ReservationModel> reservations = _reservationLogic.GetReservations();
        //if (reservations != null)
        Console.WriteLine("Select the show you want to inspect");

        Dictionary<string, ReservationModel> menuDict = new() { };

        AccountModel authUser = LocalStorage.GetAuthenticatedUser();

        long timestampNow = Helpers.DateToUnixTimeStamp(DateTime.Now.ToString());

        List<ReservationModel> reservationCustomer = reservations.Where(c => c.User.Id == authUser.Id).ToList();

        List<ReservationModel> availableReservations = reservationCustomer.Where(c => c.Show.Timestamp >= timestampNow).ToList();

        foreach (ReservationModel reservation in availableReservations)
        {
            string seatString = "";
            foreach (SeatModel seat in reservation.Seats)
            {
                string rowLetter = Helpers.AlphabetArray()[seat.Row];
                int seatNumber = seat.Column + 1;
                seatString += $"[{rowLetter},{seatNumber}],";
            }
            menuDict[$"{reservation.Show.Movie.Title} | Starts: {Helpers.TimeStampToGMEFormat(reservation.Show.Timestamp, "HH:mm dd-MM-yyyy")} | Seats: {seatString}"] = reservation;
        }

        int index = MenuList<ReservationModel>(menuDict, this, this);

        ReservationModel element = menuDict.ElementAt(index).Value;

        bool choice = CheckboxInput("Do you want to copy the reservation ID?");

        if (choice)
        {
            TextCopy.ClipboardService.SetText(element.ID);
            Helpers.SuccessMessage($"Copied {element.ID} to your clipboard!");
        }

        _reservationLogic.PrintReceipt(element);

        Helpers.Continue();

        RouteHandeler.View("DashboardPage");
    }
}
