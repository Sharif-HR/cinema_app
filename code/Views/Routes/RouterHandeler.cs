namespace Views;
public abstract class RouteHandeler {
    public static void View(string routeName) {
        Routes.RouteNameToView(routeName);
    }

    public static void LastView() {
        Routes.RouteNameToView(LocalStorage.LastVisitedPage());
    }
}
