namespace test;
public class UnitTest1
{
    private string _currentPath = @"C:\Users\shari\source\repos\ProjectB_2023\cinema_app\test";


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