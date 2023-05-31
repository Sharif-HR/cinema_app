namespace Views;

public class ShowEventHandeler : ViewTemplate {
    private ShowLogic _showLogic = new();
    private List<MovieModel> _movieList = new();
    private List<ShowModel> _showList = new();
    private string _option;
    public ShowEventHandeler(string option) : base(option) {
        this._option = option;
        this._movieList = _showLogic.GetMovies();
        this._showList = _showLogic.GetShows();
    }

    public override void Render()
    {
        base.Render();

        List<ShowModel> shows = _showLogic.GetShows();

        Dictionary<string, ShowModel> showsDict = new();

        ShowModel? chosenShow = null;

        if(_option != "Add Show") {
            foreach (ShowModel show in _showList)
            {
                if(show.Timestamp >= Helpers.DateToUnixTimeStamp(DateTime.Now.ToString())) {
                    showsDict[show.Movie.Title + " " + Helpers.TimeStampToGMEFormat(show.Timestamp)] = show;
                }
            }

            int index = MenuList(showsDict, this, this);

            var element = showsDict.ElementAt(index);
            chosenShow = element.Value;
        }

        switch(_option) {
            case "Add Show":
                AddShow();
                break;
            case "Edit Show":
                EditShow(chosenShow);
                break;
            case "Delete Show":
                DeleteShow(chosenShow);
                break;
            default:
                Helpers.ErrorMessage("Something went wrong while we wanted to redirect you. Try again later.");
                break;
        }

        LocalStorage.localStorage.Remove("SHOW_OPTION");
    }

    // [JsonPropertyName("showId")]
    // public string showId {get; set;}

    // [JsonPropertyName("timestamp")]
    // public int Timestamp {get; set;} // timestamp
    // [JsonPropertyName("takenSeats")]
    // public List<string> TakenSeats {get; set;}

    // [JsonPropertyName("numberOfSeats")]
    // public int NumberOfSeats {get; set;}

    // [JsonPropertyName("movie")]
    // public MovieModel Movie {get; set;}

    private void AddShow() {
        Dictionary<string, MovieModel> moviesDict = new();

        foreach (MovieModel movie in _movieList)
        {
            moviesDict[movie.Title] = movie;
        }

        int index = MenuList(moviesDict, this, this);

        var element = moviesDict.ElementAt(index);
        MovieModel chosenMovie = element.Value;

        while(true) {
            try
            {
                Helpers.WarningMessage("NOTE: Due to the timezone of the Netherlands (+ 2hrs), the result will be 2 hours later then you enter. Example: 17:00 -> 19:00");
                string userInput = InputField("Enter the date when the show will take place: ");

                if(userInput.Contains(":")) {
                    DateTime date = Convert.ToDateTime(userInput);
                    int timestamp = Helpers.DateToUnixTimeStamp(date.ToString());

                    if(!_showLogic.CheckShowOverlapping(timestamp)) {
                        throw new ArgumentException();
                    }

                    if(timestamp > Helpers.DateToUnixTimeStamp(DateTime.Now.ToString())) {
                        ShowModel createdShow = new(Helpers.GenUid(), timestamp, 150, new List<string>(), chosenMovie);
                        _showList.Add(createdShow);
                        _showLogic.AddShow(_showList);
                        break;
                    }

                    throw new InvalidTimeZoneException();
                }
                throw new FormatException();
            }
            catch(ArgumentException) {
                Helpers.ErrorMessage("This show is overlapping with another show. Try again.");
            }
            catch(InvalidTimeZoneException) {
                Helpers.ErrorMessage("You cannot enter a date that is already past.");
            }
            catch (FormatException)
            {
                Helpers.ErrorMessage("Enter a valid date: dd-mm-yyyy H:i");
            }
        }
    }

    private void EditShow(ShowModel show) {
        while(true) {
            try
            {
                Helpers.WarningMessage("NOTE: Due to the timezone of the Netherlands (+ 2hrs), the result will be 2 hours later then you enter. Example: 17:00 -> 19:00");
                Console.WriteLine($"Current show time: {Helpers.TimeStampToGMEFormat(show.Timestamp)}");
                string userInput = InputField("Enter the new date when the show will take place:");

                if(userInput.Contains(":")) {
                    DateTime date = Convert.ToDateTime(userInput);
                    int timestamp = Helpers.DateToUnixTimeStamp(date.ToString());

                    if(timestamp > Helpers.DateToUnixTimeStamp(DateTime.Now.ToString())) {
                        ShowModel editedShow = new(show.showId, timestamp, 150, new List<string>(), show.Movie);
                        _showLogic.EditShow(editedShow);
                        break;
                    }

                    throw new InvalidTimeZoneException();
                }
                throw new FormatException();
            }
            catch(InvalidTimeZoneException) {
                Helpers.ErrorMessage("You cannot enter a date that has already past.");
            }
            catch (FormatException)
            {
                Helpers.ErrorMessage("Enter a valid date: dd-mm-yyyy H:i");
            }
        }

        Thread.Sleep(2000);
        Helpers.WarningMessage("Redirecting you back");
        RouteHandeler.LastView();

        // int newTimestamp = Helpers.DateToUnixTimeStamp();s
    }

    private void DeleteShow(ShowModel show) {

    }
}
