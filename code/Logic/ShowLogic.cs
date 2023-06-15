public class ShowLogic : LogicTemplate
{
    private ShowAccess _showAccess = new();
    private List<ShowModel> _showList = new();

    private MovieLogic _movieLogic = new();
    private ReservationLogic _reservationLogic = new();

    public ShowLogic()
    {
        this._showList = GetShows();
    }

    public List<MovieModel> GetMovies() => _movieLogic.GetMovies();

    public List<ShowModel> GetShows() => _showAccess.LoadAll();

    public void AddShow(List<ShowModel> shows)
    {
        try
        {
            _showAccess.WriteAll(shows);
            Helpers.SuccessMessage("Show is added!");
            Helpers.Continue();
        }
        catch (System.Exception)
        {
            Helpers.ErrorMessage("Something went wrong while adding the show. Try again later");
        }
    }

    public void SaveShow(List<ShowModel> shows)
    {
        try
        {
            _showAccess.WriteAll(shows);
        }
        catch (System.Exception)
        {
            Helpers.ErrorMessage("Something went wrong while saving the show. Try again later");
        }
    }

    public void EditShow(ShowModel UpdatedShow , bool showMessage = true)
    {
        int indexShow = _showList.FindIndex(uS => uS.showId == UpdatedShow.showId);
        try
        {
            ShowModel test = _showList[indexShow] = UpdatedShow;

            _showAccess.WriteAll(_showList);

            List<ReservationModel> reservations = _reservationLogic.GetReservations();

            foreach (ReservationModel reservation in reservations)
            {
                if (reservation.Show.showId == UpdatedShow.showId)
                {
                    reservation.Show = UpdatedShow;
                }
            }

            _reservationLogic.UpdateReservations(reservations);

            if(showMessage){
                Helpers.SuccessMessage("Successfully edited the show!");
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            Helpers.ErrorMessage("No reservations at this show");
        }
    }

    public void DeleteShow(string id)
    {
        int index = _showList.FindIndex(s => s.showId == id);
        _showList.RemoveAt(index);

        _showAccess.WriteAll(_showList);
    }

    public bool CheckShowOverlapping(int timestamp)
    {
        var shows = _showList.Where(s => (s.Timestamp - 900) <= timestamp && timestamp <= s.Timestamp + (s.Movie.Duration * 60));
        return shows.Count() > 0;
    }

    public void UpdateSeats(string showId, List<string> newSeats)
    {
        int indexShow = _showList.FindIndex(shows => shows.showId == showId);

        try
        {
            ShowModel show = _showList[indexShow];
            foreach (string seat in newSeats)
            {
                show.TakenSeats.Add(seat);
            }
        }
        catch (System.Exception)
        {

            throw;
        }
        finally
        {
            _showAccess.WriteAll(_showList);
        }
    }
}
