namespace Views;

public class ShowWeekOverview{
    private ShowLogic _showLogic = new();
    
    public List<ShowModel> getWeekOverview(){
        List<ShowModel> shows = _showLogic.GetShows();

        var reservation = from show in shows
                                where (show.Timestamp > Helpers.DateToUnixTimeStamp(DateTime.Now.ToString()) && show.Timestamp < (Helpers.DateToUnixTimeStamp(DateTime.Now.ToString()) + (86400 * 7)))
                                select show;
        
        return reservation.ToList();
    }
}