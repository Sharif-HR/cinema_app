namespace Views;

class Menu : ViewTemplate
{
    private Register RegisterPage = new();
    private Login LoginPage = new();
    public Menu() : base("Menu") { }

    //This shows the menu. You can call back to this method to show the menu again
    //after another presentation method is completed.
    //You could edit this to show different menus depending on the user's role
    public override void Render()
    {
        bool loop = true;
        while(loop) {
            base.Render();

            Console.WriteLine("Enter 1 to login");
            Console.WriteLine("Enter 2 to register");

            string input = Console.ReadLine();
            if (input == "1")
            {
                LoginPage.Render();
            }
            else if (input == "2")
            {
                RegisterPage.Render();
            }
            else
            {
                Console.WriteLine("Invalid input");
                Render();
            }
        }

    }
}
