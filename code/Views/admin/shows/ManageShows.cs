using System.Globalization;

namespace Views;

public class ManageShows : ViewTemplate, IManage
{
    private ShowLogic _showLogic = new();
    private List<MovieModel> _movies = new();
    private List<ShowModel> _shows = new();

    public ManageShows() : base("Manage shows")
    {
        this._movies = _showLogic.GetMovies();
        this._shows = _showLogic.GetShows();
    }

    public override void Render()
    {
        while (true)
        {
            base.Render();
            List<ShowModel> shows = _showLogic.GetShows();

            Dictionary<string, string> listDict = new() {
                {"Add a show.", ""},
                {"Edit a show.", ""},
                {"Delete a show.", ""},
                {"See all shows.", ""}
            };

            string UserInput = Convert.ToString(MenuList(listDict, this, this) + 1);

            switch (UserInput)
            {
                case "1":
                    AddForm();
                    break;
                case "2":
                    EditForm();
                    break;
                case "3":
                    DeleteForm();
                    break;
                case "4":
                    ShowAllShows();
                    break;
                default:
                    Helpers.ErrorMessage("Something went wrong while we wanted to redirect you. Try again later.");
                    RouteHandeler.LastView();
                    break;
            }
        }
    }

    public void ShowAllShows()
    {
        Dictionary<string, ShowModel> showsDict = new();

        if (_shows.Count == 0)
        {
            Helpers.ErrorMessage("There are no shows.");
            Helpers.Continue();
            return;
        }

        foreach (ShowModel show in _shows)
        {
            if (show.Timestamp >= Helpers.DateToUnixTimeStamp(DateTime.Now.ToString()))
            {
                showsDict[$"{show.Movie.Title} {Helpers.TimeStampToGMEFormat(show.Timestamp, "HH:mm dd-MM-yyyy")}"] = show;
            }
        }

        int index = MenuList(showsDict, this, this);
    }


    private int SelectMovieFromList()
    {

        Dictionary<string, MovieModel> moviesDict = new();

        foreach (MovieModel movie in _movies)
        {
            moviesDict[movie.Title] = movie;
        }

        int index = MenuList(moviesDict, this, this);
        return index;
    }

    private ShowModel SelectShowFromList()
    {
        Dictionary<string, ShowModel> showsDict = new();

        foreach (ShowModel show in _shows)
        {
            if (show.Timestamp >= Helpers.DateToUnixTimeStamp(DateTime.Now.ToString()))
            {
                showsDict[$"{show.Movie.Title} {Helpers.TimeStampToGMEFormat(show.Timestamp, "HH:mm dd-MM-yyyy")}"] = show;
            }
        }

        int index = MenuList(showsDict, this, this);

        ShowModel selectedShow = showsDict.ElementAt(index).Value;
        return selectedShow;

    }

    public void AddForm()
    {
        if (_movies.Count == 0)
        {
            Helpers.WarningMessage("Currently there are no movies.");
            Helpers.Continue();
            return;
        }

        MovieModel selectedMovie = _movies[SelectMovieFromList()];

        while (true)
        {
            var date = base.InputDateTime("Enter a date time when to show takes place");

            int timestamp = Helpers.DateToUnixTimeStamp(date.ToString());

            if (_showLogic.CheckShowOverlapping(timestamp))
            {
                Helpers.ErrorMessage("this show overlaps with another show. try another date.");
                Helpers.Continue();
                continue;
            }



            if (timestamp < Helpers.DateToUnixTimeStamp(DateTime.Now.ToString()))
            {
                Helpers.ErrorMessage("Please enter another that has not been passed.");
                Helpers.Continue();
            }


            if (timestamp > Helpers.DateToUnixTimeStamp(DateTime.Now.ToString()))
            {
                ShowModel createdShow = new(timestamp, 150, new List<string>(), selectedMovie);
                _shows.Add(createdShow);
                _showLogic.AddShow(_shows);
                return;
            }


        }

    }

    public void EditForm()
    {
        if (_shows.Count == 0)
        {
            Helpers.ErrorMessage("There are no shows.");
            Helpers.Continue();
            return;
        }

        while (true)
        {
            var show = SelectShowFromList();
            var date = base.InputDateTime("Enter a date time when to show takes place");

            int timestamp = Helpers.DateToUnixTimeStamp(date.ToString());

            if (_showLogic.CheckShowOverlapping(timestamp))
            {
                Helpers.ErrorMessage("this show overlaps with another show. try another date.");
                Helpers.Continue();
                continue;
            }



            if (timestamp < Helpers.DateToUnixTimeStamp(DateTime.Now.ToString()))
            {
                Helpers.ErrorMessage("Please enter another that has not been passed.");
                Helpers.Continue();
            }

            show.Timestamp = timestamp;
            _showLogic.SaveShow(_shows);
            Helpers.SuccessMessage("Show updated!");
            Helpers.Continue();
            return;
        }
    }

    public void DeleteForm()
    {

        if (_shows.Count == 0)
        {
            Helpers.ErrorMessage("There are no shows.");
            Helpers.Continue();
            return;
        }

        var show = SelectShowFromList();
        _showLogic.DeleteShow(show.showId);
        _shows = _showLogic.GetShows();

        Helpers.SuccessMessage("Show deleted.");
        Helpers.Continue();
    }







    // private void AddShow()
    // {
    //     Dictionary<string, MovieModel> moviesDict = new();

    //     foreach (MovieModel movie in _movieList)
    //     {
    //         moviesDict[movie.Title] = movie;
    //     }

    //     int index = MenuList(moviesDict, this, this);

    //     var element = moviesDict.ElementAt(index);
    //     MovieModel chosenMovie = element.Value;

    //     while (true)
    //     {
    //         try
    //         {
    //             Helpers.WarningMessage("NOTE: Due to the timezone of the Netherlands (+ 2hrs), the result will be 2 hours later then you enter. Example: 17:00 -> 19:00");
    //             string userInput = InputField("Enter the date when the show will take place: ");

    //             if (userInput.Contains(":"))
    //             {
    //                 DateTime date = Convert.ToDateTime(userInput);
    //                 int timestamp = Helpers.DateToUnixTimeStamp(date.ToString());

    //                 if (_showLogic.CheckShowOverlapping(timestamp))
    //                 {
    //                     throw new ArgumentException();
    //                 }

    //                 if (timestamp > Helpers.DateToUnixTimeStamp(DateTime.Now.ToString()))
    //                 {
    //                     ShowModel createdShow = new(Helpers.GenUid(), timestamp, 150, new List<string>(), chosenMovie);
    //                     _showList.Add(createdShow);
    //                     _showLogic.AddShow(_showList);
    //                     break;
    //                 }

    //                 throw new InvalidTimeZoneException();
    //             }
    //             throw new FormatException();
    //         }
    //         catch (ArgumentException)
    //         {
    //             Helpers.ErrorMessage("This show is overlapping with another show. Try again.");
    //         }
    //         catch (InvalidTimeZoneException)
    //         {
    //             Helpers.ErrorMessage("You cannot enter a date that is already past.");
    //         }
    //         catch (FormatException)
    //         {
    //             Helpers.ErrorMessage("Enter a valid date: dd-mm-yyyy H:i");
    //         }
    //     }
    // }

    // private void EditShow(ShowModel show)
    // {
    //     while (true)
    //     {
    //         try
    //         {
    //             Helpers.WarningMessage("NOTE: Due to the timezone of the Netherlands (+ 2hrs), the result will be 2 hours later then you enter. Example: 17:00 -> 19:00");
    //             Console.WriteLine($"Current show time: {Helpers.TimeStampToGMEFormat(show.Timestamp)}");
    //             string userInput = InputField("Enter the new date when the show will take place:");

    //             if (userInput.Contains(":"))
    //             {
    //                 DateTime date = Convert.ToDateTime(userInput);
    //                 int timestamp = Helpers.DateToUnixTimeStamp(date.ToString());

    //                 if (timestamp > Helpers.DateToUnixTimeStamp(DateTime.Now.ToString()))
    //                 {
    //                     ShowModel editedShow = new(show.showId, timestamp, 150, new List<string>(), show.Movie);
    //                     _showLogic.EditShow(editedShow);
    //                     break;
    //                 }

    //                 throw new InvalidTimeZoneException();
    //             }
    //             throw new FormatException();
    //         }
    //         catch (InvalidTimeZoneException)
    //         {
    //             Helpers.ErrorMessage("You cannot enter a date that has already past.");
    //         }
    //         catch (FormatException)
    //         {
    //             Helpers.ErrorMessage("Enter a valid date: dd-mm-yyyy H:i");
    //         }
    //     }

    //     Thread.Sleep(2000);
    //     Helpers.WarningMessage("Redirecting you back");
    //     RouteHandeler.LastView();

    //     // int newTimestamp = Helpers.DateToUnixTimeStamp();s
    // }

    // private void DeleteShow(ShowModel show)
    // {
    //     _showLogic.DeleteShow(show.showId);
    //     _showList = _showLogic.GetShows();
    // }

}
