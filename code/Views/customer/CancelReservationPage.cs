namespace Views;

public class CancelReservationPage : ViewTemplate{
    private ReservationLogic _reservationLogic = new();
    public CancelReservationPage(ShowModel show = null) : base($"Cancel Reservation"){}

    public override void Render()
    {
        base.Render();

        List<ReservationModel> reservations = _reservationLogic.GetReservations();
        
        Console.WriteLine("Please enter your unique reservation code:");
        string uniqueCode = Console.ReadLine();

        var reservation = from res in reservations
                        where (res.ID == uniqueCode)
                        select res;
        
        if(reservation.Count() == 0){
            Console.WriteLine("No reservation was found with this code");
            Thread.Sleep(5000);
            return;
        }

        ReservationModel customerReservation = reservation.ElementAt(0);

        if(DateTime.Parse(Helpers.TimeStampToGMEFormat(customerReservation.Show.Timestamp)) > DateTime.Now){
            //_reservationLogic.DeleteReservation(customerReservation, reservations);
            Console.WriteLine("Reservation cancelled");
            Thread.Sleep(5000);
        }else{
            Console.WriteLine("You can't cancel this reservation anymore");
            Thread.Sleep(5000);
        }
    }
}