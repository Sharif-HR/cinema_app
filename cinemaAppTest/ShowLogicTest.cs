namespace CinemaApp;

[TestClass]
public class ShowLogicTest
{
    private ShowLogic _showLogic;
    private ShowAccess _showAccess;
    
    
    [TestInitialize]
    public void Initialize(){
        _showLogic = new ShowLogic();
        _showAccess = new ShowAccess();
    }

    [TestMethod]
    public void TestGetShow(){
        List<ShowModel> ShowsList = _showAccess.LoadAll();
        Assert.IsNotNull(ShowsList);
    }

    [TestMethod]
    public void TestAddShow(){
        MovieModel chosenMovie = new MovieModel("title", 12, "testSummary", new List<string>{"Genre 1", "Genre 2"}, "testDir", "12-6-2023");
        int timestamp = Helpers.DateToUnixTimeStamp(DateTime.Now.ToString());
        ShowModel createdShow = new(Helpers.GenUid(), timestamp, 150, new List<string>(), chosenMovie);
        List<ShowModel> showList = new List<ShowModel>{createdShow};
        _showLogic.AddShow(showList);
        List<ShowModel> ShowsList = _showAccess.LoadAll();
        Assert.IsNotNull(ShowsList);
    }

    [TestMethod]
    public void TestDeleteShow(){
        MovieModel chosenMovie = new MovieModel("title", 12, "testSummary", new List<string>{"Genre 1", "Genre 2"}, "testDir", "12-6-2023");
        int timestamp = Helpers.DateToUnixTimeStamp(DateTime.Now.ToString());
        ShowModel createdShow = new("id1", timestamp, 150, new List<string>(), chosenMovie);
        ShowModel createdShow2 = new("id2", timestamp, 150, new List<string>(), chosenMovie);
        List<ShowModel> showList = new List<ShowModel>{createdShow, createdShow2};
        _showLogic.AddShow(showList);
        List<ShowModel> ShowsList = _showAccess.LoadAll();

        ShowModel show = ShowsList[0];
        _showLogic.DeleteShow(show.showId);
        Assert.AreEqual(1, ShowsList.Count);
    }

    [TestMethod]
    public void TestEditShow(){
        MovieModel chosenMovie = new MovieModel("title", 12, "testSummary", new List<string>{"Genre 1", "Genre 2"}, "testDir", "12-6-2023");
        int timestamp = Helpers.DateToUnixTimeStamp(DateTime.Now.ToString());
        ShowModel createdShow = new("id1", timestamp, 150, new List<string>(), chosenMovie);
        List<ShowModel> showList = new List<ShowModel>{createdShow};
        _showLogic.AddShow(showList);

        ShowModel updatedShow = new ShowModel("id1", timestamp, 120, new List<string>(), chosenMovie);
        _showLogic.EditShow(updatedShow);

        List<ShowModel> ShowsList = _showAccess.LoadAll();
        Assert.AreEqual(ShowsList[0].NumberOfSeats, 120);
    }

    [TestMethod]
    public void TestCheckShowOverlapping(){
        MovieModel chosenMovie = new MovieModel("title", 12, "testSummary", new List<string>{"Genre 1", "Genre 2"}, "testDir", "12-6-2023");
        int timestamp = Helpers.DateToUnixTimeStamp(DateTime.Now.ToString());
        ShowModel createdShow = new("id1", timestamp, 150, new List<string>(), chosenMovie);
        List<ShowModel> showList = new List<ShowModel>{createdShow};
        _showLogic.AddShow(showList);

        Assert.AreEqual(_showLogic.CheckShowOverlapping(11), false);
    }

    [TestMethod]
    public void TestUpdateSeats(){
        MovieModel chosenMovie = new MovieModel("title", 12, "testSummary", new List<string>{"Genre 1", "Genre 2"}, "testDir", "12-6-2023");
        int timestamp = Helpers.DateToUnixTimeStamp(DateTime.Now.ToString());
        ShowModel createdShow = new("id1", timestamp, 150, new List<string>(), chosenMovie);
        List<ShowModel> showList = new List<ShowModel>{createdShow};
        _showLogic.AddShow(showList);

        _showLogic.UpdateSeats("id1", new List<string>{"1,1","1,2"});

        List<ShowModel> ShowsList = _showAccess.LoadAll();
        Assert.AreEqual(ShowsList[0].TakenSeats, new List<string>{"1,1","1,2"});
    }
}