namespace CinemaApp;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

[TestClass]
public class MovieLogicTests
{
    private MovieLogic _movieLogic;
    private MovieAccess _movieAccess;
    private List<MovieModel> _initialMovieList;

    [TestInitialize]
    public void Initialize()
    {
        _movieLogic = new MovieLogic();
        _movieAccess = new MovieAccess();

        // Create initial movie list for testing
        _initialMovieList = new List<MovieModel>()
        {
            new MovieModel("Movie 1", 120, "Summary 1", new List<string>{"Genre 1", "Genre 2"}, "Director 1", "2021-01-01"),
            new MovieModel("Movie 2", 90, "Summary 2", new List<string>{"Genre 3", "Genre 4"}, "Director 2", "2022-02-02"),
            new MovieModel("Movie 3", 150, "Summary 3", new List<string>{"Genre 1", "Genre 4"}, "Director 3", "2023-03-03")
        };

        // Save initial movie list
        _movieLogic.SaveMovies(_initialMovieList);
    }

    [TestMethod]
    public void TestAddMovie()
    {
        // Arrange
        var newMovie = new MovieModel("New Movie", 180, "Summary", new List<string>{"Genre 5", "Genre 6"}, "Director 4", "2024-04-04");

        // Act
        _movieLogic.AddMovie(newMovie);
        var movies = _movieLogic.GetMovies();

        // Assert
        Assert.AreEqual(_initialMovieList.Count + 1, movies.Count);
    }

    [TestMethod]
    public void TestEditMovie()
    {
        // Arrange
        int index = 0; // Edit the first movie
        string propertyName = "Duration";
        int updatedValue = 150;

        // Act
        _movieLogic.EditMovie(index, propertyName, updatedValue);
        var movies = _movieLogic.GetMovies();
        var editedMovie = movies[index];

        // Assert
        Assert.AreEqual(updatedValue, editedMovie.Duration);
    }

    [TestMethod]
    public void TestDeleteMovie()
    {
        // Arrange
        int id = 1; // Delete the second movie

        // Act
        _movieLogic.DeleteMovie(id);
        var movies = _movieLogic.GetMovies();

        // Assert
        Assert.AreEqual(_initialMovieList.Count, movies.Count);
        Assert.IsFalse(movies.Exists(m => m.Id == _initialMovieList[id].Id));
    }
}
