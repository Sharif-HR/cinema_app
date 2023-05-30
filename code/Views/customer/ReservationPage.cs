namespace Views;

public class ReservationPage : ViewTemplate
{
    public ShowModel? Show;
    private HallLogic _logic = new();
    private SeatModel[][] _emptyHall;


    private ReservationLogic _reservationLogic = new();
    public ReservationPage(ShowModel show = null) : base($"Make Reservation") { this.Show = show; }

    private void loadEmptyHall()
    {
        _emptyHall = _logic.GenerateHall();
    }

    private double totalAuditoriumPrice(List<SeatModel> seats)
    {
        double total = 0;
        foreach (var seat in seats)
        {
            total += seat.Price;
        }

        return total;
    }

    private string selectedSeatsList(List<SeatModel> seats)
    {
        List<string> seatLocations = new List<string>();
        foreach (var seat in seats)
        {
            seatLocations.Add($"{seat.Row + 1}-{seat.Column + 1}");
        }


        // if (toString)
        // {
        return string.Join(",", seatLocations);
        // }

        // return seatLocations;
    }



    public override void Render()
    {
        base.Render();
        // code for the auditorium which returns seats
        // List<string> seats = Auditorium.GetSeats();

        //test code
        List<ReservationModel> reservations = _reservationLogic.GetReservations();
        // List<string> takenseats = Show.TakenSeats;
        //TODO make if to check if aduitorium is empty;
        loadEmptyHall();
        var hall = _emptyHall;
        List<SeatModel> selectedSeats = SelectSeat(hall);
        string seatsString = selectedSeatsList(selectedSeats);
        Show.TakenSeats = seatsString.Split(',').ToList();

        double costs = totalAuditoriumPrice(selectedSeats);



        // create all the data
        string id = GenRandId();


        List<RefreshmentModel> chosenRefreshments = new();
        while (true)
        {
            bool wantsSnack = CheckboxInput("Do you want to add a snack? Enter y or n");
            if (!wantsSnack)
            {
                break;
            }
            // refreshment list
            chosenRefreshments.Add(RefreshmentsList(this));
        }

        if (chosenRefreshments.Count > 0)
        {
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
        // TODO update show model for takenseats;
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

        if (IDCheck.Count() != 0)
        {
            return GenRandId();
        }
        else
        {
            return ID;
        }
        return ID;
    }


    private bool selectAnotherSeat()
    {
        return base.InputBool("Would you like to select another seat?");
    }

    private List<SeatModel> SelectSeat(SeatModel[][] hall)
    {
        while (true)
        {
            try
            {
                base.Render();
                ShowHall(hall);
                Console.WriteLine("Select a seat by entering the [ROW],[SEAT NUMBER].");
                Console.WriteLine("You can also deselect a seat by entering the same location with [ROW],[SEAT NUMBER]");
                Helpers.WarningMessage("For example: 2,4");
                Console.Write("> ");
                var index = Console.ReadLine();
                var splitIndex = index.Split(",");

                bool validRow = int.TryParse(splitIndex[0], out int selectedRow);
                bool validColumn = int.TryParse(splitIndex[1], out int selectedColumn);


                if (!validRow || !validColumn)
                {
                    Helpers.WarningMessage("Invalid row or column number");
                    Helpers.Continue();

                    continue;
                }


                SeatModel selectedSeat;
                selectedSeat = hall[selectedRow - 1][selectedColumn - 1];


                if (selectedSeat.Reserved)
                {
                    Helpers.WarningMessage("Sorry this seat is reserved. Please choose another one.");
                    Helpers.Continue();
                    continue;
                }

                if (!selectedSeat.isSeat)
                {
                    Helpers.WarningMessage("Sorry there is not seat on this location. Please choose another one.");
                    Helpers.Continue();
                    continue;
                }


                switch (selectedSeat.Selected)
                {
                    case false:
                        selectedSeat.Selected = true;
                        selectedSeat.Type = "s";

                        break;

                    case true:
                        selectedSeat.Selected = false;
                        selectedSeat.Type = selectedSeat.OriginalType;
                        break;
                }

                base.Render();
                ShowHall(hall);
                Helpers.SuccessMessage($"You selected seat number {selectedColumn} on row {selectedRow}! \n\n");

                if (selectAnotherSeat())
                {
                    continue;
                }



                List<SeatModel> selectedSeatList = new List<SeatModel>();
                for (int row = 0; row < hall.Length; row++)
                {
                    for (int seat = 0; seat < hall[row].Length; seat++)
                    {
                        bool isSelectedSeat = hall[row][seat].Selected;
                        if (isSelectedSeat)
                        {
                            selectedSeatList.Add(hall[row][seat]);
                        }
                    }

                }

                return selectedSeatList;
            }
            catch (Exception e)
            {
                Helpers.WarningMessage("Something went wrong");
                Helpers.WarningMessage(e.Message + e.StackTrace + e.GetBaseException());
                Helpers.Continue();
            }
        }

    }


    public static SeatModel[][] ShowHall(SeatModel[][] hall)
    {

        XLine();
        Content(hall);
        Screen();

        return hall;
    }

    private static void XLine()
    {
        string marginLeft = new string(' ', 8);
        string div = new string(' ', 39);
        Console.Write($"{marginLeft}{div}SEAT NUMBER{div}");

        Console.WriteLine();
        Console.WriteLine();


        Console.Write(marginLeft);
        for (int i = 1; i < 13; i++)
        {

            if (i >= 10)
            {
                Console.Write($"|  {i} |");
            }
            else
            {
                Console.Write($"|  {i}  |");
            }

        }

        Console.WriteLine();

        string dashes = new string('-', 84);
        Console.WriteLine($"{marginLeft}{dashes}");
    }

    private static void Content(SeatModel[][] hall)
    {

        for (int row = 0; row < hall.Length; row++)
        {
            for (int column = 0; column < hall[row].Length; column++)
            {
                if (column == 0)
                {

                    if (row == 5)
                    {
                        Console.Write("R");
                        Console.Write($"  {row + 1} :  ");

                    }
                    if (row == 6)
                    {
                        Console.Write("O");
                        Console.Write($"  {row + 1} :  ");

                    }
                    if (row == 7)
                    {
                        Console.Write("W");
                        Console.Write($"  {row + 1} :  ");

                    }

                    if (row >= 9)
                    {
                        Console.Write($"   {row + 1}:  ");
                    }

                    if (row < 9 && row is not (5 or 6 or 7))
                    {
                        Console.Write($"   {row + 1} :  ");
                    }

                }
                var seat = hall[row][column];
                string visualSeat = $"|  {seat.Icon}  |";

                switch (seat.Type)
                {
                    case ("a"):
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(visualSeat);
                        Console.ResetColor();
                        break;

                    case ("p"):
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(visualSeat);
                        Console.ResetColor();
                        break;


                    case ("v"):
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(visualSeat);
                        Console.ResetColor();
                        break;


                    case ("s"):
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(visualSeat);
                        Console.ResetColor();
                        break;

                    case ("x"):
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(visualSeat);
                        Console.ResetColor();
                        break;

                    default:
                        Console.Write(new string(' ', 7));
                        break;

                }

            }
            Console.WriteLine();
        }
    }

    private static void Screen()
    {
        string sp = new string(' ', 39);
        string margin = new string(' ', 8);
        Console.WriteLine();
        Console.WriteLine();
        Console.Write(margin);
        Console.WriteLine(new string('_', 84));

        Console.WriteLine($"{margin}{sp}SCREEN{sp}");
        Console.WriteLine();
        Console.WriteLine();
    }

}
