static class Dashboard
{
    //Shows the dashboard(similar to the Menu).
    //Options shown depends on the role of the user.
    static private AccountsLogic accountsLogic = new AccountsLogic();    
    static public void Start(AccountModel acc)
    {
        if (acc.Role == "Admin")
        {
            Console.WriteLine("Enter 1 to see a list of available movies.");
            Console.WriteLine("Enter 2 to view your reservations.");
            Console.WriteLine("Enter 3 to edit movies");

            string input = Console.ReadLine();
            if (input == "1")
            {
                Console.WriteLine("Needs to be implemented.");
            }
            else if (input == "2")
            {
                Console.WriteLine("Needs to be implemented.");
            }
            else if (input == "3")
            {
                Console.WriteLine("Needs to be implemented.");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
        else if (acc.Role == "Customer")
        {
            Console.WriteLine("Enter 1 to see a list of available movies.");
            Console.WriteLine("Enter 2 to view your reservations.");
            Console.WriteLine("Enter 3 to log out");

            string input = Console.ReadLine();
            if (input == "1")
            {
                Console.WriteLine("Needs to be implemented.");
            }
            else if (input == "2")
            {
                Console.WriteLine("Needs to be implemented.");
            }
            else if (input == "3")
            {
                Console.WriteLine("Needs to be implemented.");
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
        else
        {
            Console.WriteLine($"Error: Role cannot be {acc.Role}");
        }
    }


}