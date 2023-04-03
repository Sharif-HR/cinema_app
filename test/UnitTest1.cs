namespace test;
public class UnitTest1
{
    [Fact]
    public void TestLoginCorrectCredentials()
    {
        bool loginSuccess = false;
        AccountsLogic accountLogic = new(@"C:\Users\shari\source\repos\Project_b\cinema_app\code\");

        var hasUser = accountLogic.CheckLogin("admin@admin.com", "password");

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
        AccountsLogic accountLogic = new(@"C:\Users\shari\source\repos\Project_b\cinema_app\code\");

        var hasUser = accountLogic.CheckLogin("notexists@mail.com", "!@#welom");

        if (hasUser != null)
        {
            loginSuccess = true;
        }

        Assert.Equal(false, loginSuccess);
    }


    [Fact]
    public void testModelTable(){

        AccountsLogic accountsLogic = new(@"C:\Users\shari\source\repos\Project_b\cinema_app\code\");
        var accountsList = accountsLogic.GetAccounts();

        MovieLogic movieLogic = new(@"C:\Users\shari\source\repos\Project_b\cinema_app\code\");
        var movieList = movieLogic.GetMovies();

        var accountsTable = accountsLogic.GenerateModelTable<AccountModel>(accountsList);
        var moviesTable = movieLogic.GenerateModelTable<MovieModel>(movieList);
        
        Console.WriteLine(accountsTable);
        Console.WriteLine(moviesTable);
        

        Helpers.SuccessMessage("done!");
    }
}