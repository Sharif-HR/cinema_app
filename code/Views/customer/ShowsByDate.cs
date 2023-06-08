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

                int timestampOfGivenDate = Helpers.DateToUnixTimeStamp(date);
                int timestampNextDay = Helpers.DateToUnixTimeStamp(Convert.ToString(Convert.ToDateTime(date).AddDays(1.0)));

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
