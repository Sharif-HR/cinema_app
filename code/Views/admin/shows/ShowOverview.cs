namespace Views;

public class ShowOverviewAdmin : ViewTemplate {
    public ShowOverviewAdmin() : base("Select a option") {  }

    public override void Render()
    {
        base.Render();
        Dictionary<string, string> options = new(){
            {"Add Show", "ShowEventHandeler"},
            {"Edit Show", "ShowEventHandeler"},
            {"Delete Show", "ShowEventHandeler"}
        };

        int index = MenuList(options, this, this);
        var element = options.ElementAt(index);
        LocalStorage.setItem("SHOW_OPTION", element.Key);
        RouteHandeler.View("ShowEventHandeler");
    }
}
