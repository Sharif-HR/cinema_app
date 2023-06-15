namespace Views;

public class ShowOverview : ViewTemplate
{
    private ShowAccess _showAccess = new(null);
    public ShowOverview() : base("Overview of the shows") { }

    public override void Render()
    {
        base.Render();
        List<string> menuOptions = new() { "Shows by date" };
        MenuList(menuOptions, this, "ShowOverviewPageAdmin");
    }
}
