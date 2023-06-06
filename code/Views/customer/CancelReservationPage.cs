namespace Views;

public class CancelReservationPage : ViewTemplate{
    private ReservationLogic _reservationLogic = new();
    public CancelReservationPage(ShowModel show = null) : base($"Cancel Reservation"){}

    public override void Render()
    {
        base.Render();

        List<ReservationModel> reservations = _reservationLogic.GetReservations();

        while(true){
            Console.WriteLine("Please enter your unique reservation code or leave blank to return:");
            string uniqueCode = Console.ReadLine();

            if(uniqueCode == ""){
                break;
            }

            var reservation = from res in reservations
                            where (res.ID == uniqueCode)
                            select res;

            if(reservation.Count() == 0){
                Console.WriteLine("No reservation was found with this code...");
                Thread.Sleep(5000);
                continue;
            }

            ReservationModel customerReservation = reservation.ElementAt(0);

            if(customerReservation.User.Id != LocalStorage.GetAuthenticatedUser().Id){
                Console.WriteLine("No reservation was found with this code...");
                Thread.Sleep(5000);
                continue;
            }

            if((customerReservation.Show.Timestamp > Helpers.DateToUnixTimeStamp(DateTime.Now.ToString()))){
                _reservationLogic.DeleteReservation(customerReservation, reservations);
                Console.WriteLine("Reservation cancelled...");
                Thread.Sleep(5000);
                break;
            }else{
                Console.WriteLine("You can't cancel this reservation anymore...");
                Thread.Sleep(5000);
            }
        }
    }
}
