using System.Globalization;

namespace Views;

public class ShowsByDate : ViewTemplate
{
    private ShowAccess _showAccess = new(null);
    public ShowsByDate() : base("Overview of the shows") { }

    public override void Render()
    {

        base.Render();
        List<ShowModel> shows = new();
        shows = GetShowModelsByDate();
        shows = shows.Where(s => s.Timestamp >= Helpers.DateToUnixTimeStamp(DateTime.Now.ToString())).ToList();

        Dictionary<string, ShowModel> showsDict = new();

        foreach (var show in shows)
        {
            showsDict[$"{show.Movie.Title} | Starts: {Helpers.TimeStampToGMEFormat(show.Timestamp, "HH:mm")}"] = show;
        }

        int index = MenuList(showsDict, this, new ReservationPage());
        var element = showsDict.ElementAt(index);
        // render the page with the ShowModel
        new ReservationPage(element.Value).Render();
    }

    private List<ShowModel> GetShowModelsByDate()
    {
        while (true)
        {
            try
            {
                string date = InputField("Enter the date of the movies you wish to see:");

                DateOnly inputDateOnly = DateOnly.ParseExact(date, "dd-MM-yyyy", new CultureInfo("nl-NL"));
                DateTime inputDateTime = inputDateOnly.ToDateTime(TimeOnly.Parse("00:00 AM"));

                DateTimeOffset dateToConvert = new DateTimeOffset(inputDateTime);
                int timestampOfGivenDate = (int)dateToConvert.ToUnixTimeSeconds();

                DateTime nextDay = inputDateTime.AddDays(1);
                DateTimeOffset nextDayConvert = new DateTimeOffset(nextDay);
                int timestampNextDay = (int)nextDayConvert.ToUnixTimeSeconds();


                List<ShowModel> shows = _showAccess.LoadAll();
                List<ShowModel> availableShows = shows.Where(s => s.Timestamp >= timestampOfGivenDate && s.Timestamp < timestampNextDay).ToList();

                return availableShows;
            }
            catch (Exception)
            {
                Helpers.ErrorMessage("Please enter a valid date in this format: dd-mm-yyyy");
                continue;
            }
        }
    }
}