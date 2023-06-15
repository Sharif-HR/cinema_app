// namespace Views;

// public class ShowOverviewAdmin : ViewTemplate {
//     public ShowOverviewAdmin() : base("Select a option") {  }

//     public override void Render()
//     {
//         base.Render();
//         Dictionary<string, string> options = new(){
//             {"Add Show", "ShowEventHandeler"},
//             {"Edit Show", "ShowEventHandeler"},
//             {"Delete Show", "ShowEventHandeler"}
//         };

//         int index = MenuList(options, this, this);
//         var element = options.ElementAt(index);
//         LocalStorage.setItem("SHOW_OPTION", element.Key);
//         RouteHandeler.View("ShowEventHandeler");
//     }
// }

// namespace Views;

// public class ShowOverview : ViewTemplate
// {
//     private ShowAccess _showAccess = new(null);
//     public ShowOverview() : base("Overview of the shows") { }

//     public override void Render()
//     {
//         base.Render();
//         List<string> menuOptions = new() { "Shows by date" };
//         MenuList(menuOptions, this, "ShowOverviewPageAdmin");
//     }
// }