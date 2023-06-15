namespace Views;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class ManageRefreshments : ViewTemplate, IManage
{
    private RefreshmentsLogic _refreshmentLogic = new();
    public ManageRefreshments() : base("Manage Refreshments") { }

    public override void Render()
    {
        base.Render();
        Options();
    }


    private void Options()
    {
        while (true)
        {
            base.Render();

            Dictionary<string, string> listDict = new() {
                {"Add a refreshment.", ""},
                {"Edit refreshments.", ""},
                {"Delete refreshment.", ""},
                {"Show refreshments.", ""},
            };

            string UserInput = Convert.ToString(MenuList(listDict, this, this) + 1);

            switch (UserInput)
            {
                case "1":
                    AddForm();
                    break;

                case "2":
                    EditForm();
                    break;

                case "3":
                    DeleteForm();
                    break;


                case "4":
                    ShowRefreshmentTable("yes");
                    break;

                default:
                    Helpers.WarningMessage("Invalid input.");
                    Helpers.Continue();
                    break;
            }
        }
    }

    public void AddForm()
    {
        while (true)
        {
            base.Render();
            base.GoBackMsg();

            string name = base.InputField("Refreshment name:");
            bool isDrink = base.InputBool("Is refreshment a drink(enter true or false):");
            double price = base.InputDecimalNumber("Refreshment price:");

            RefreshmentModel newRefreshment = new RefreshmentModel(name: name, isDrink: isDrink, price: price);

            _refreshmentLogic.AddRefreshment(newRefreshment);
            Helpers.SuccessMessage("refreshment added!");
            Helpers.Continue();
        }
    }

    public void EditForm()
    {
        var refreshments = _refreshmentLogic.GetRefreshments();

        if (refreshments.Count == 0)
        {
            Helpers.WarningMessage("You have no refreshments to edit.");
            Helpers.Continue();
            return;
        }

        while (true)
        {
            ShowRefreshmentTable("null");
            Helpers.WarningMessage($"In order to select a refreshment to edit enter a number between 1 and {refreshments.Count}");
            base.GoBackMsg();
            int refreshmentId = SelectFromModelList<RefreshmentModel>(refreshments, true);
            var refreshmentProperties = RefreshmentProperties();
            refreshmentProperties.Add("Exit");

            bool loopSelectedRefreshment = true;
            while (loopSelectedRefreshment)
            {
                refreshments = _refreshmentLogic.GetRefreshments();

                base.Render();
                Helpers.WarningMessage("Selected refreshment:");
                Console.WriteLine($@"Name: {refreshments[refreshmentId].Name}
IsDrink: {refreshments[refreshmentId].IsDrink}
Price: {refreshments[refreshmentId].Price}
");
                Helpers.Divider(false);


                Helpers.WarningMessage($"Enter a number between 1 and {refreshmentProperties.Count} to update a property of this refreshment or to exit:");
                // Print property of model
                for (int i = 0; i < refreshmentProperties.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {refreshmentProperties[i]}");
                }

                int propertyIndex;
                string chosenProperty;

                propertyIndex = base.SelectFromModelList<string>(refreshmentProperties, true, "property id");

                chosenProperty = refreshmentProperties[propertyIndex];

                object updatedValue = null;
                switch (chosenProperty.ToLower())
                {
                    case "name":
                        updatedValue = InputField("Enter new name:");
                        break;

                    case "isDrink":
                        updatedValue = InputBool("Enter true or false:");
                        break;

                    case "price":
                        updatedValue = InputDecimalNumber("Enter new Price:");
                        break;

                    case "exit":
                        loopSelectedRefreshment = false;
                        break;

                    default:
                        break;
                }


                if (loopSelectedRefreshment)
                {
                    _refreshmentLogic.EditRefreshment(refreshmentId, chosenProperty, updatedValue);
                    Helpers.SuccessMessage("Refreshment updated!");
                    Helpers.Continue();
                }

            }
        }
    }

    public void DeleteForm()
    {
        while (true)
        {
            var refreshments = _refreshmentLogic.GetRefreshments();
            if (refreshments.Count == 0)
            {
                Helpers.WarningMessage("You have no refreshments to delete.");
                Helpers.Continue();
                return;
            }

            ShowRefreshmentTable("null");
            refreshments = _refreshmentLogic.GetRefreshments();
            base.GoBackMsg();
            int refreshmentId = base.SelectFromModelList<RefreshmentModel>(refreshments, true);

            _refreshmentLogic.DeleteRefreshment(refreshmentId);
            Helpers.SuccessMessage("Refreshment Deleted!");
            Helpers.Continue();
        }
    }

    public void ShowRefreshmentTable(string showmenu)
    {
        base.Render();
        var refreshment = _refreshmentLogic.GetRefreshments();
        Console.WriteLine(_refreshmentLogic.GenerateModelTable<RefreshmentModel>(refreshment));

        if (showmenu == "yes")
        {
            SearchSortFilterMenu();

            Dictionary<string, string> listDict = new() {
                {"Search for a refreshment", ""},
                {"Sort refreshments list", ""},
                {"Filter refreshments list", ""},
            };

            // string UserInput = Convert.ToString(MenuList(listDict, this, this) + 1);
            string UserInput = Console.ReadLine();

            switch (UserInput)
            {
                case "1":
                    SearchRefreshment();
                    break;

                case "2":
                    SortRefreshments();
                    break;

                case "3":
                    FilterRefreshments();
                    break;

                case "4":
                    return;

                default:
                    Helpers.WarningMessage("Invalid input.");
                    Helpers.Continue();
                    break;
            }
        }
        if (showmenu == "continue")
        {
            Helpers.Continue();
        }
        if (showmenu == "null") { }
    }

    public List<string> RefreshmentProperties()
    {
        List<string> refreshmentProperties = new();
        foreach (var prop in typeof(RefreshmentModel).GetProperties())
        {
            if (prop.Name != "Id")
            {
                refreshmentProperties.Add(prop.Name);
            }
        }

        return refreshmentProperties;
    }
    private void SearchSortFilterMenu()
    {
        Console.Write(@"Please select an option:
1. Search for a refreshment
2. Sort refreshments list
3. Filter refreshments list
4. Exit
> ");
    }

    private void SearchRefreshment()
    {
        var refreshments = _refreshmentLogic.GetRefreshments();
        base.Render();
        Console.WriteLine(_refreshmentLogic.GenerateModelTable<RefreshmentModel>(refreshments));

        Helpers.Divider();
        Console.WriteLine("Search refreshments by name");
        Helpers.Divider();

        Console.WriteLine("Enter the name of the refreshment:");
        Console.Write("> ");
        string InputTitle = "empty";
        InputTitle = Console.ReadLine();

        IEnumerable<RefreshmentModel> FoundRefreshment =
            from refr in refreshments
            where Helpers.CaseInsensitiveContains(refr.Name, InputTitle)
            select refr;

        List<RefreshmentModel> FoundRefreshments = new List<RefreshmentModel>();
        foreach (RefreshmentModel Refreshment in FoundRefreshment)
        {
            FoundRefreshments.Add(Refreshment);
        }

        base.Render();
        if (FoundRefreshments.Count == 0)
        {
            Helpers.WarningMessage("No refreshment(s) found with that name.");
            Helpers.Continue();
            ShowRefreshmentTable("yes");
        }
        else
        {
            Console.WriteLine(_refreshmentLogic.GenerateModelTable<RefreshmentModel>(FoundRefreshments));
            Helpers.Continue();
            ShowRefreshmentTable("yes");
        }
    }

    private void SortRefreshments()
    {
        var refreshments = _refreshmentLogic.GetRefreshments();
        base.Render();
        Console.WriteLine(_refreshmentLogic.GenerateModelTable<RefreshmentModel>(refreshments));

        Helpers.Divider();
        Console.WriteLine("Sort refreshments");
        Helpers.Divider();

        var refreshmentProperties = RefreshmentProperties();
        refreshmentProperties.Add("Exit");

        Helpers.WarningMessage($"Enter a number between 1 and {refreshmentProperties.Count} to sort or to exit:");

        for (int i = 0; i < refreshmentProperties.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {refreshmentProperties[i]}");
        }

        int propertyIndex;
        string chosenProperty;

        propertyIndex = base.SelectFromModelList<string>(refreshmentProperties, true, "property id");
        chosenProperty = refreshmentProperties[propertyIndex];

        List<RefreshmentModel> FoundRefreshments = new();

        if (propertyIndex != 3)
        {
            Console.Write(@"1. Ascending
2. Descending");
            int AscendingDescending = base.InputNumber("\nEnter 1 to sort in ascending order or 2 to sort in descending order.");


            if (AscendingDescending == 1)
            {
                switch (chosenProperty.ToLower())
                {
                    case "name":
                        var OrderByName = from m in refreshments
                                          orderby m.Name
                                          select m;

                        foreach (RefreshmentModel refreshment in OrderByName)
                        {
                            FoundRefreshments.Add(refreshment);
                        }
                        break;

                    case "isdrink":
                        FilterRefreshments(1);
                        break;

                    case "price":
                        var OrderByPrice = from m in refreshments
                                           orderby m.Price
                                           select m;

                        foreach (RefreshmentModel refreshment in OrderByPrice)
                        {
                            FoundRefreshments.Add(refreshment);
                        }
                        break;

                    case "exit":
                        break;

                    default:
                        return;
                }
            }
            if (AscendingDescending == 2)
            {
                switch (chosenProperty.ToLower())
                {
                    case "name":
                        var OrderByName = from m in refreshments
                                          orderby m.Name descending
                                          select m;

                        foreach (RefreshmentModel refreshment in OrderByName)
                        {
                            FoundRefreshments.Add(refreshment);
                        }
                        break;

                    case "isdrink":
                        FilterRefreshments(2);
                        break;

                    case "price":
                        var OrderByPrice = from m in refreshments
                                           orderby m.Price descending
                                           select m;

                        foreach (RefreshmentModel refreshment in OrderByPrice)
                        {
                            FoundRefreshments.Add(refreshment);
                        }
                        break;

                    case "exit":
                        break;

                    default:
                        return;
                }
            }
            base.Render();
            if (FoundRefreshments.Count == 0)
            {
                Helpers.WarningMessage("No refreshment(s) found with that name.");
                Helpers.Continue();
                ShowRefreshmentTable("null");
            }
            else
            {
                Console.WriteLine(_refreshmentLogic.GenerateModelTable<RefreshmentModel>(FoundRefreshments));
                Helpers.Continue();
                ShowRefreshmentTable("yes");
            }
        }
    }

    private void FilterRefreshments(int isAsc = 0)
    {
        var refreshments = _refreshmentLogic.GetRefreshments();
        base.Render();
        Console.WriteLine(_refreshmentLogic.GenerateModelTable<RefreshmentModel>(refreshments));

        Helpers.Divider();
        Console.WriteLine("Filter refreshments by type");
        Helpers.Divider();

        Console.WriteLine("1. Food");
        Console.WriteLine("2. Drinks");

        bool o = true;
        if (isAsc == 0)
        {
            while (true)
            {
                string UserInput = Console.ReadLine();
                if (UserInput == "1")
                {
                    o = false;
                    break;
                }
                else if (UserInput == "2")
                {
                    o = true;
                    break;
                }
            }
        }
        else if (isAsc == 1)
        {
            o = true;
        }
        else if (isAsc == 2)
        {
            o = false;
        }

        IEnumerable<RefreshmentModel> FoundRefreshment =
            from refr in refreshments
            where refr.IsDrink == o
            select refr;

        List<RefreshmentModel> FoundRefreshments = new List<RefreshmentModel>();
        foreach (RefreshmentModel refreshment in FoundRefreshment)
        {
            FoundRefreshments.Add(refreshment);
        }

        base.Render();
        if (FoundRefreshments.Count == 0)
        {
            Helpers.WarningMessage("No refreshment(s) found of that type.");
            Helpers.Continue();
            ShowRefreshmentTable("yes");
        }
        else
        {
            Console.WriteLine(_refreshmentLogic.GenerateModelTable<RefreshmentModel>(FoundRefreshments));
            Helpers.Continue();
            ShowRefreshmentTable("yes");
        }
    }
}
