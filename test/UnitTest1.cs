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
}