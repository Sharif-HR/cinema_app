namespace Views;
using System.Globalization;
using System.Reflection;

public abstract class ViewTemplate
{
    private string Title;

    public ViewTemplate(string title)
    {
        this.Title = title;
    }

    public void GoBackMsg()
    {
        Helpers.SuccessMessage("To go back keep your input empty and PRESS enter");
    }

    public virtual void Render()
    {
        Console.Clear();
        this.CinemaLogo();
        Helpers.Divider(false);
        Console.WriteLine(this.Title);

        // if (LocalStorage.localStorage["history"].Count > 0) {
        //     Console.WriteLine("Leave a field empty to go back to the previous page");
        // }

        Helpers.Divider(false);
    }

    public string InputField(string label)
    {
        while (true)
        {
            Console.WriteLine(label);
            Console.Write("> ");
            string? userInput = Console.ReadLine() ?? null;

            if (!string.IsNullOrWhiteSpace(userInput))
            {
                return userInput;
            }

            RouteHandeler.LastView();
        }
    }

    public string PasswordToAstriks()
    {
        var pass = string.Empty;
        ConsoleKey key;

        while (true)
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && pass.Length > 0)
            {
                Console.Write("\b \b");
                pass = pass[0..^1];
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write("*");
                pass += keyInfo.KeyChar;
            }

            if (key == ConsoleKey.Enter)
            {
                Console.WriteLine();

                if (!string.IsNullOrWhiteSpace(pass)) return pass;
                RouteHandeler.LastView();
            }
        }
    }

    public string InputPassword(string label, bool mustBeStrong = false, int minLength = 8, int maxLength = 32)
    {
        string passwordMsg = $"Password must be at least {minLength} characters and max {maxLength} characters long\n must contain at least one uppercase, one lowercase and one special character, can't contain a white space.";
        bool loop = true;
        string input = "";

        while (loop)
        {
            if (mustBeStrong)
            {
                Helpers.WarningMessage(passwordMsg);

            }
            Console.WriteLine(label);
            Console.Write("> ");
            input = this.PasswordToAstriks();

            if (input == null || input == "") RouteHandeler.LastView();

            if (mustBeStrong)
            {

                bool hasError = false;
                bool containsSC = false;
                if (input.Length < minLength || input.Length > maxLength)
                {
                    hasError = true;
                }

                if (!input.Any(char.IsUpper))
                {
                    hasError = true;
                }

                if (!input.Any(char.IsLower))
                {
                    hasError = true;
                }

                if (input.Contains(" "))
                {
                    hasError = true;
                }

                string specialChars = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
                char[] specialCh = specialChars.ToCharArray();
                foreach (char ch in specialCh)
                {
                    if (input.Contains(ch))
                    {
                        containsSC = true;
                        return input;
                    }
                }
                if (!containsSC || hasError)
                {
                    Console.WriteLine();
                    Helpers.ErrorMessage("Please try again with a password that meets the minimum safety requirements.");
                }
            }
            else
            {
                loop = false;
            }
        }
        return input;
    }


    public string InputPhoneNumber(string label)
    {
        return this.InputField($"{label} (enter x to skip this field)");
    }

    public string OptionalInput(string label)
    {
        Console.WriteLine(label);
        Helpers.WarningMessage("optional: Press enter to skip this field.");
        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }

        return input;
    }


    public bool CheckboxInput(string label)
    {
        string[] options = { "y", "n" };
        while (true)
        {
            Console.WriteLine(label);
            Console.Write("> ");
            string userInput = Console.ReadLine() ?? null;
            string warningMessage = "Invalid input. Please enter y or n";

            if (String.IsNullOrEmpty(userInput))
            {
                RouteHandeler.LastView();
            }

            if (string.IsNullOrWhiteSpace(userInput) == true)
            {
                Helpers.WarningMessage(warningMessage);
                continue;
            }

            string userInputLower = userInput.ToLower();
            if (!options.Contains(userInputLower))
            {
                Helpers.WarningMessage(warningMessage);
                continue;
            }

            if (userInputLower == "y")
            {
                return true;
            }
            else if (userInputLower == "n")
            {
                return false;
            }
        }


    }

    public int InputNumber(string label, bool allowZero = true)
    {
        while (true)
        {
            try
            {
                // enter moive duration in minutes
                Console.WriteLine(label);
                Console.Write("> ");
                string numberStr = Console.ReadLine() ?? null;

                if (numberStr == null || numberStr == "") RouteHandeler.LastView();

                if (!Helpers.IsDigitsOnly(numberStr) && !string.IsNullOrWhiteSpace(numberStr))
                {
                    Helpers.WarningMessage("This field accepts numbers only.");
                    continue;

                }

                if (string.IsNullOrWhiteSpace(numberStr))
                {
                    Helpers.WarningMessage("This field cannot be empty.");
                    continue;

                }

                if (allowZero == false && Helpers.IsDigitsOnly(numberStr) && numberStr == "0")
                {
                    Helpers.WarningMessage("This field does not accept 0.");
                    continue;

                }

                if (Helpers.IsDigitsOnly(numberStr) && !string.IsNullOrWhiteSpace(numberStr))
                {
                    return int.Parse(numberStr);
                }
            }
            catch
            {
                Helpers.WarningMessage("Number is to long enter a smaller number");
            }
        }
    }

    public double InputDecimalNumber(string label)
    {
        while (true)
        {
            try
            {
                Console.WriteLine(label);
                Console.Write("> ");
                string numberStr = Console.ReadLine();

                bool convertedToDouble = Double.TryParse(numberStr, out double decimalNumber);

                if (!convertedToDouble)
                {
                    Helpers.WarningMessage("This field accepts numbers only.");
                    continue;
                }

                return decimalNumber;
            }
            catch
            {
                Helpers.ErrorMessage("Number is to long enter a smaller number");
            }
        }
    }


    public bool InputBool(string label)
    {
        while (true)
        {
            Console.WriteLine(label);
            Helpers.WarningMessage("Enter yes or no");
            Console.Write("> ");
            string isbool = Console.ReadLine();

            if (isbool.ToLower() == "n" || isbool.ToLower() == "no")
            {
                return false;
            }
            else if (isbool.ToLower() == "y" || isbool.ToLower() == "yes")
            {
                return true;
            }
            else
            {
                Helpers.WarningMessage("This field accepts only yes, y, n or no.");
            }
        }
    }

    public List<string> InputMultiple(string label, string userInput = null)
    {
        Console.WriteLine(label);
        Helpers.WarningMessage("(Enter multiple values comma separated)");
        Console.Write("> ");

        if (userInput == null)
        {
            userInput = Console.ReadLine() ?? null;
            if (userInput == null || userInput == "") RouteHandeler.LastView();
        }

        List<string> values = new List<string>(userInput.Split(','));

        for (int i = 0; i < values.Count; i++)
        {
            values[i] = values[i].Trim();
        }

        return values;
    }


    public object InputDate(string label, bool isDateOnly = true)
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Enter date in this format (DD-MM-YYYY)");
                Console.WriteLine(label);

                string enteredDate = Console.ReadLine() ?? null;

                if (enteredDate == null || enteredDate == "") RouteHandeler.LastView();

                DateOnly dateObject = DateOnly.ParseExact(enteredDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                if (isDateOnly == true)
                {
                    return dateObject;
                }

                if (isDateOnly == false)
                {

                    return dateObject.ToString("dd-MM-yyyy");
                }
            }
            catch
            {
                Helpers.WarningMessage("Invalid Date input. Date format must be (DD-MM-YYYY) -> (20-10-2020).");
            }
        }
    }

    public object InputDateTime(string label, bool isDateOnly = true)
    {
        while (true)
        {
            try
            {
                Console.WriteLine("Enter date and time in this format (HH:mm DD-MM-YYYY)");
                Console.WriteLine(label);

                string enterDateTime = Console.ReadLine() ?? null;
                if (enterDateTime == null || enterDateTime == "") RouteHandeler.LastView();
                DateTime dateObject = DateTime.ParseExact(enterDateTime, "HH:mm dd-MM-yyyy", CultureInfo.InvariantCulture);

                if (isDateOnly == true)
                {
                    return dateObject;
                }

                if (isDateOnly == false)
                {
                    return dateObject.ToString("HH:mm dd-MM-yyyy");
                }

            }
            catch
            {
                Helpers.WarningMessage("Invalid Date input. Date format must be (HH:mm DD-MM-YYYY) -> (17:30 20-10-2020).");
            }
        }
    }

    public List<ReturnValue> InputDropdownMenu<ReturnValue>(string label, List<ReturnValue> choices)
    {
        List<ReturnValue> availableChoices = choices;
        List<ReturnValue> chosenValues = new() { };
        int index = 0;

        while (true)
        {
            Console.WriteLine("Select a option");
            Helpers.PrintListContent<ReturnValue>(choices, true);
            string userInput = this.InputField("");

            //convert string to int in order to add & remove the value based of an index
            try
            {
                index = Convert.ToInt32(userInput);
            }
            catch (FormatException)
            {
                Helpers.WarningMessage("You can only enter the numbers");
            }

            try
            {
                chosenValues.Add(availableChoices[index]);
                availableChoices.RemoveAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                Helpers.WarningMessage("You can only select the options that are shown.");
            }

            if (availableChoices.Count == 0) break;
        }

        return chosenValues;
    }

    protected int SelectFromModelList<T>(List<T> modelList, bool substractOne = false, string customName = null)
    {
        string modelName = customName;

        if (customName == null)
        {
            modelName = typeof(T).Name.ToLower();
            modelName = modelName.Substring(0, modelName.Length - 5);
        }

        while (true)
        {
            int modelIndex = InputNumber($"Enter {modelName} ID:");

            if (Helpers.HasIndexInList<T>(modelIndex, modelList, false) == false)
            {
                Helpers.WarningMessage("Please enter a valid ID.");
                continue;
            }

            if (substractOne)
            {
                return modelIndex - 1;
            }

            return modelIndex;
        }
    }

    public void MenuList(List<string> routesList, ViewTemplate page, string viewName)
    {
        Console.WriteLine("Use ⬆️  and ⬇️  to navigate and press Enter to select:");
        (int left, int top) = Console.GetCursorPosition();
        var option = 1;
        var decorator = "\u001b[32m";
        ConsoleKeyInfo key;
        bool isSelected = false;

        // Insert the go back option
        List<string> pagesWithGoBack = new(){
            "ManageShowsPageAdmin",
            "ManageMoviesPageAdmin",
            "ManageReservationsPageAdmin",
            "MakeReservationPageCustomer"
        };

        if (pagesWithGoBack.Contains(viewName))
        {
            routesList.Insert(0, "Go Back");
        }

        while (!isSelected)
        {
            Console.Clear();
            // render screen cant use the this.render due to an infinite while loop
            this.CinemaLogo();
            Helpers.Divider(false);
            Console.WriteLine(page.Title);
            Helpers.Divider(false);

            Console.SetCursorPosition(left, top);

            // Log all the options in the terminal
            for (int i = 0; i < routesList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {(option == (i + 1) ? decorator : "")}{routesList[i]}\u001b[0m");
            }

            key = Console.ReadKey(false);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    option = option == 1 ? routesList.Count : option - 1;
                    break;
                case ConsoleKey.DownArrow:
                    option = option == routesList.Count ? 1 : option + 1;
                    break;
                case ConsoleKey.Enter:
                    isSelected = true;
                    break;
            }
        }
        if (routesList[option - 1] == "Go Back")
        {
            RouteHandeler.LastView();
        }
        else if (new[] { "Register", "Login", "Dashboard", "Logout", "About us" }.Contains(routesList[option - 1]))
        {
            var routeName = routesList[option - 1];
            RouteHandeler.View(routeName.Replace(" ", "") + "Page");
        }
        else
        {
            var routeName = routesList[option - 1];

            // return the view from the routehandler using the chosen option + page + the role of the admin
            // if the role is null (not authenticated), no role will be added to the string
            RouteHandeler.View(routeName.Replace(" ", "") + "Page" + Helpers.CapitalizeFirstLetter(LocalStorage.GetAuthenticatedUser().Role) ?? "");
        }
    }

    public int MenuList<T>(Dictionary<string, T> routesDict, ViewTemplate page, ViewTemplate pageToGoWithID)
    {
        Console.WriteLine("Use ⬆️  and ⬇️  to navigate and press Enter to select:");
        (int left, int top) = Console.GetCursorPosition();
        var option = 1;
        var decorator = "\u001b[32m";
        ConsoleKeyInfo key;
        bool isSelected = false;
        List<string> routesList = routesDict.Keys.ToList();

        // Insert the go back option
        routesList.Insert(0, "Go Back");

        while (!isSelected)
        {
            Console.Clear();
            // render screen cant use the this.render due to an infinite while loop
            this.CinemaLogo();
            Helpers.Divider(false);
            Console.WriteLine(page.Title);
            Helpers.Divider(false);

            Console.SetCursorPosition(left, top);

            // Log all the options in the terminal
            for (int i = 0; i < routesList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {(option == (i + 1) ? decorator : "")}{routesList[i]}\u001b[0m");
            }

            key = Console.ReadKey(false);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    option = option == 1 ? routesList.Count : option - 1;
                    break;
                case ConsoleKey.DownArrow:
                    option = option == routesList.Count ? 1 : option + 1;
                    break;
                case ConsoleKey.Enter:
                    isSelected = true;
                    break;
            }
        }
        if (option == 1)
        {
            RouteHandeler.LastView();
        }
        //routing
        string keyOfDict = routesList[option - 1];
        var item = routesDict[keyOfDict];

        // return the index of the chosen option
        return option - 2;
    }



    public RefreshmentModel RefreshmentsList(ViewTemplate page)
    {
        var option = 1;
        var decorator = "\u001b[32m";
        ConsoleKeyInfo key;
        bool isSelected = false;

        List<RefreshmentModel> refreshments = new RefreshmentAccess().LoadAll();
        while (!isSelected)
        {

            Console.Clear();
            // render screen cant use the this.render due to an infinite while loop
            this.CinemaLogo();
            Helpers.Divider(false);
            Console.WriteLine(page.Title);
            Helpers.Divider(false);
            Console.WriteLine("Use ⬆️  and ⬇️  to navigate and press Enter to select:");
            (int left, int top) = Console.GetCursorPosition();
            Console.SetCursorPosition(left, top);

            // Log all the options in the terminal
            for (int i = 0; i < refreshments.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {(option == (i + 1) ? decorator : "")}{refreshments[i].Name} | {refreshments[i].Price}\u001b[0m");
            }

            key = Console.ReadKey(false);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    option = option == 1 ? refreshments.Count : option - 1;
                    break;
                case ConsoleKey.DownArrow:
                    option = option == refreshments.Count ? 1 : option + 1;
                    break;
                case ConsoleKey.Enter:
                    isSelected = true;
                    break;
            }
        }

        return refreshments[option - 1];
    }



    // TODO exit this function option


    private void CinemaLogo()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(@"

  ______   _____  _____  ____   ____  ________  _______
.' ____ \ |_   _||_   _||_  _| |_  _||_   __  ||_   __ \
| (___ \_|  | |    | |    \ \   / /    | |_ \_|  | |__) |
 _.____`.   | |    | |   _ \ \ / /     |  _| _   |  __ /
| \____) | _| |_  _| |__/ | \ ' /     _| |__/ | _| |  \ \_
 \______.'|_____||________|  \_/     |________||____| |___|
");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(@"  ______  _____  ____  _____  ________  ____    ____       _
 .' ___  ||_   _||_   \|_   _||_   __  ||_   \  /   _|     / \
/ .'   \_|  | |    |   \ | |    | |_ \_|  |   \/   |      / _ \
| |         | |    | |\ \| |    |  _| _   | |\  /| |     / ___ \
\ `.___.'\ _| |_  _| |_\   |_  _| |__/ | _| |_\/_| |_  _/ /   \ \_
 `.____ .'|_____||_____|\____||________||_____||_____||____| |____|

");
        Console.ResetColor();

    }
}
