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
        while(true) {
            base.Render();

            Console.Write(@"Please select an option:
1. Login
2. Register
3. Exit (Temporary for Development)");

            Helpers.Divider();
            string UserInput = InputField("Enter a number:");
            switch(UserInput){
                case "1":
                    RouteHandeler.View("LoginPage");
                    break;

                case "2":
                    RouteHandeler.View("RegisterPage");
                    break;

                case "3":
                    Console.WriteLine("Closing Cinema-app...");
                    return;

                default:
                    Console.WriteLine("Invalid input");
                    Helpers.Continue();
                    break;
            }
        }
    }
}
