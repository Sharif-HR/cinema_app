namespace CinemaApp;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

[TestClass]
public class ShowLogicTests
{
    private ShowLogic _showLogic;
    private List<ShowModel> _initialShowList;

    [TestInitialize]
    public void Initialize()
    {
        // Initialize ShowLogic
        _showLogic = new ShowLogic();

        // Create initial show list for testing
        _initialShowList = new List<ShowModel>()
        {
            new ShowModel(1, 100, new List<string>{"A1", "A2"}, new MovieModel("Movie 1", 120, "Summary 1", new List<string>{"Genre 1", "Genre 2"}, "Director 1", "2021-01-01")),
            new ShowModel(2, 200, new List<string>{"B1", "B2", "B3"}, new MovieModel("Movie 2", 90, "Summary 2", new List<string>{"Genre 3", "Genre 4"}, "Director 2", "2022-02-02")),
            new ShowModel(3, 150, new List<string>{"C1", "C2", "C3", "C4"}, new MovieModel("Movie 3", 150, "Summary 3", new List<string>{"Genre 1", "Genre 4"}, "Director 3", "2023-03-03"))
        };

        // Save initial show list
        _showLogic.SaveShow(_initialShowList);
    }

    // [TestMethod]
    // public void TestAddShow()
    // {
    //     // Arrange
    //     var newShow = new ShowModel(4, 300, new List<string>{"D1", "D2", "D3", "D4", "D5"}, new MovieModel("New Movie", 180, "Summary", new List<string>{"Genre 5", "Genre 6"}, "Director 4", "2024-04-04"));

    //     // Act
    //     _showLogic.AddShow(new List<ShowModel> { newShow });
    //     var shows = _showLogic.GetShows();

    //     // Assert
    //     Assert.AreEqual(_initialShowList.Count + 1, shows.Count);
    //     CollectionAssert.Contains(shows, newShow);
    // }

    [TestMethod]
    public void TestEditShow()
    {
        // Arrange
        int index = 0; // Edit the first show
        var updatedShow = new ShowModel(1, 100, new List<string>{"A3", "A4"}, new MovieModel("Updated Movie", 150, "Updated Summary", new List<string>{"Genre 1", "Genre 2"}, "Director 1", "2021-01-01"));

        // Act
        _showLogic.EditShow(updatedShow, false);
        var shows = _showLogic.GetShows();
        var editedShow = shows[index];

        // Assert
        Assert.AreEqual(updatedShow.TakenSeats, editedShow.TakenSeats);
        Assert.AreEqual(updatedShow.Movie.Title, editedShow.Movie.Title);
    }

    [TestMethod]
    public void TestDeleteShow()
    {
        // Arrange
        string showId = _initialShowList[1].showId; // Delete the second show

        // Act
        _showLogic.DeleteShow(showId);
        var shows = _showLogic.GetShows();

        // Assert
        Assert.AreEqual(_initialShowList.Count - 1, shows.Count);
        Assert.IsFalse(shows.Exists(s => s.showId == showId));
    }

    [TestMethod]
    public void TestCheckShowOverlapping_NoOverlap()
    {
        // Arrange
        int timestamp = 5000; // Set a timestamp that doesn't overlap with any shows

        // Act
        bool hasOverlap = _showLogic.CheckShowOverlapping(timestamp);

        // Assert
        Assert.IsFalse(hasOverlap);
    }

    [TestMethod]
    public void TestCheckShowOverlapping_Overlap()
    {
        // Arrange
        int timestamp = 121 * 60; // Set a timestamp that overlaps with the first show

        // Act
        bool hasOverlap = _showLogic.CheckShowOverlapping(timestamp);

        // Assert
        Assert.IsTrue(hasOverlap);
    }

    [TestMethod]
    public void TestUpdateSeats()
    {
        // Arrange
        string showId = _initialShowList[0].showId; // Update seats for the first show
        var newSeats = new List<string> { "A3", "A4", "A5" };

        // Act
        _showLogic.UpdateSeats(showId, newSeats);
        var shows = _showLogic.GetShows();
        var updatedShow = shows.Find(s => s.showId == showId);

        // Assert
        CollectionAssert.AreEqual(newSeats, updatedShow.TakenSeats);
    }

    [TestCleanup]
    public void Cleanup()
    {
        // Clean up any test data or files if necessary
    }
}
