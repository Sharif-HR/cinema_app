namespace test;
public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        
        bool  isNumber = Helpers.IsDigitsOnly("12");
        Assert.Equal(true, isNumber);
    }
}