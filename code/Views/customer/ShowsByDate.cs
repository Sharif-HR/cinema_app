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
        ShowWeekOverview showWeekOverview = new ShowWeekOverview();
        while (true)
        {
            try
            {
                return showWeekOverview.getWeekOverview();
            }
            catch (Exception)
            {
                Helpers.ErrorMessage("Please enter a valid date in this format: dd-mm-yyyy");
                continue;
            }
        }
    }
}
