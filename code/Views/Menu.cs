namespace Views;

public class Menu : ViewTemplate
{
    private Register RegisterPage = new();
    private Login LoginPage = new();
    public Menu() : base("Silver Cinema 2023") { }

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    public override void Render()
    {
        while (true)
        {
            base.Render();
            List<string> testList = new() { "Login", "Register", "About us" };
            MenuList(testList, this);
        }
    }
}
