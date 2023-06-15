namespace Views;
public abstract class RouteHandeler
{
    public static void View(string viewNameString)
    {
        if (viewNameString == "MenuPage")
        {
            if (LocalStorage.localStorage["history"].Contains("MenuPage") == false)
            {
                LocalStorage.AddToHistory(viewNameString);
                LocalStorage.AddToHistory(viewNameString);
            }
        }
        if (viewNameString != LocalStorage.GetLastViewName())
        {
            LocalStorage.AddToHistory(viewNameString);
        }

        Routes.RouteNameToView(viewNameString);
    }

    public static void LastView()
    {
        Console.WriteLine("Directing you back...");
        Thread.Sleep(800);
        Routes.RouteNameToView(LocalStorage.LastVisitedPage());
        return;
    }

    // History history = new History();
    //     Routes.RouteNameToView(history.history.render());
    //     return;
}
