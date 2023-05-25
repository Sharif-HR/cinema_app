public class ReservationLogic : LogicTemplate
{
    private ReservationAccess _reservationAccess = new();
    public ReservationLogic() { }

    public void PrintReceipt(ReservationModel reservation)
    {
        string movieName = CheckStringLengthForReceiptFormat(reservation.Show.Movie.Title, "🎥 Movie");
        string seats = CheckStringLengthForReceiptFormat(reservation.Seats, "💺 Seats");
        string date = CheckStringLengthForReceiptFormat(Helpers.TimeStampToGMEFormat(reservation.Show.Timestamp, "dd-MM-yyyy"), "📆 Date");
        string timeOfShow = CheckStringLengthForReceiptFormat(Helpers.TimeStampToGMEFormat(reservation.Show.Timestamp, "HH:mm"), "⌚️ Time");
        string totalCosts = CheckStringLengthForReceiptFormat(Math.Round(reservation.Costs, 3).ToString(), "TOTAL");

        string tickets = CheckStringLengthForReceiptFormat(seats.Split(',').Count() + $" X 15        {seats.Split(',').Count() * 15}", "Tick costs");
        //refreshments
        List<string> refreshmentsNamesList = new();

        foreach (RefreshmentModel refreshment in reservation.Refreshments)
        {
            refreshmentsNamesList.Add(refreshment.Name);
        }

        var stringCounts = refreshmentsNamesList
            .GroupBy(str => str)
            .ToDictionary(group => group.Key, group => group.Count());

        var refreshmentDict = new Dictionary<string, Dictionary<RefreshmentModel, int>>();

        foreach (RefreshmentModel refreshment in reservation.Refreshments)
        {
            if (!refreshmentDict.ContainsKey(refreshment.Name))
            {
                if (stringCounts.ContainsKey(refreshment.Name))
                {
                    Dictionary<RefreshmentModel, int> innerDict = new();
                    innerDict[refreshment] = stringCounts[refreshment.Name];

                    refreshmentDict[refreshment.Name] = innerDict;
                }
            }
        }

        string receipt = $@"
+-------------------------------------+
|                                     |
|           Silver Cinema             |
|                                     |
|          3011 WN Rotterdam          |
|            Wijnhaven 107            |
|           (010) 794-4000            |
|                                     |
|  Reservation Details:               |
{movieName}
{seats}
{date}
{timeOfShow}
|                                     |
|  Ticket info 🎫                     |
{tickets}
{RefreshmentsReceipt(refreshmentDict)}
|                                     |
{totalCosts}
|                                     |
|                                     |
|             THANK YOU!         🦦   |
+-------------------------------------+
";
        Console.WriteLine(receipt);
    }

    private string CheckStringLengthForReceiptFormat(string str, string label)
    {
        string format = $"|  ";

        if (label.Count() <= 10)
        {
            format += label + ": ";
        }
        else
        {
            format += label.Substring(0, 10) + ": ";
        }

        if (str.Count() >= 29)
        {
            str = str.Substring(0, 28);
            format += str;
        }
        else if (str.Count() < 28)
        {
            format += str;
            for (int i = 0; format.Count() < 38; i++)
            {
                format += " ";
            }
        }

        return format + "|";
    }

    private string RefreshmentsReceipt(Dictionary<string, Dictionary<RefreshmentModel, int>> refreshmentDict)
    {
        string lastItem;
        string endResult = "";
        try
        {
            lastItem = refreshmentDict.Last().Key;
            foreach (var item in refreshmentDict)
            {
                string key = item.Key;

                foreach (var refreshment in refreshmentDict[key])
                {
                    RefreshmentModel rfm = refreshment.Key;
                    int occurrence = refreshment.Value;
                    endResult += CheckStringLengthForReceiptFormat($"{occurrence} x {rfm.Price}           {Math.Round(occurrence * rfm.Price, 2)}", rfm.Name);
                    if (lastItem != key) endResult += "\n";
                }
            }
        }
        catch (System.Exception)
        {
            return null;
        }


        return endResult;
    }

    public void UpdateReservations(List<ReservationModel> reservations) {
        _reservationAccess.WriteAll(reservations);
    }

    public List<ReservationModel> GetReservations() {
        return _reservationAccess.LoadAll();
    }
}
