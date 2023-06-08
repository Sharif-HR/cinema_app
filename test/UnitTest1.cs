namespace test;
public class UnitTest1
{
    private string _currentPath = @"C:\Users\shari\source\repos\ProjectB_2023\cinema_app\test";


    [Fact]
    public void ExampleTest()
    {
        // Your function here
        List<string> fruits = new List<string>(){
            "apple", "banana", "orange", "berries"
        };

        fruits.Remove("banana");

        bool hasBanana = fruits.Contains("banana");

        // this is a scenario where we excpect that the result is true
        Assert.Equal(false, hasBanana);
    }

    [Fact]
    public void TestAddMovie(){
        MovieLogic movieLogic = new MovieLogic(_currentPath);
        MovieModel movie = new MovieModel("test", 120, "test movie description", new List<string>{"Horror"}, "Test Dir", "07-06-2015", "08-06-2023");
        string movieId = movie.Id;

        movieLogic.AddMovie(movie);

        bool foundMovie = movieLogic.GetMovies().Any(m => m.Id == movieId);

        Assert.Equal(true, foundMovie);
    }


    [Fact]
    public void TestLoginCorrectCredentials()
    {
        bool loginSuccess = false;
        AccountsLogic accountLogic = new(_currentPath);

        var hasUser = accountLogic.CheckLogin("hans@jwz.com", "123Welkom!@#");

        if (hasUser != null)
        {
            loginSuccess = true;
        }

        Assert.Equal(true, loginSuccess);
    }

    [Fact]
    public void TestLoginIncorrectCredentials()
    {
        bool loginSuccess = false;
        AccountsLogic accountLogic = new(_currentPath);

        var hasUser = accountLogic.CheckLogin("notexists@mail.com", "!@#welom");

        if (hasUser != null)
        {
            loginSuccess = true;
        }

        Assert.Equal(false, loginSuccess);
    }
}