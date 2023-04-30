using System.Globalization;
using System.Reflection;

public abstract class ViewTemplate
{
    private string Title;

    public ViewTemplate(string title)
    {
        this.Title = title;
    }
    public virtual void Render()
    {
        Console.Clear();
        this.CinemaLogo();
        Helpers.Divider(false);
        Console.WriteLine(this.Title);
        Console.WriteLine("Press the ESC button to go back at anytime");
        Helpers.Divider(false);
    }

    public string InputField(string label)
    {
        while (true)
        {
            Console.WriteLine(label);
            Console.Write("> ");
            string userInput = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(userInput))
            {
                return userInput;
            }
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

            if (key == ConsoleKey.Enter && !string.IsNullOrWhiteSpace(pass))
            {
                Console.WriteLine();
                return pass;
            }
        }
    }

    public string InputPassword(string label, bool mustBeStrong = false, int minLength = 8, int maxLength = 32)
    {
        bool loop = true;
        string input = "";

        while (loop)
        {
            Console.WriteLine(label);
            Console.Write("> ");
            input = this.PasswordToAstriks();

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
                    Console.WriteLine($"Password must be at least {minLength} characters and max {maxLength} characters long, must contain at least one uppercase, one lowercase and one special character, can't contain a white space.");
                }
            }
            else
            {
                loop = false;
            }
        }
        return input;
    }


    public string InputPhoneNumber(string label, bool isOptional = false)
    {
        Console.WriteLine(label);
        Helpers.WarningMessage("optional: Press enter to skip this field.");
        string? input;
        while (true)
        {
            Console.Write("> ");
            input = Console.ReadLine();

            if (Helpers.ContainsLetters(input))
            {
                Helpers.WarningMessage("Letters are not allowed.");
                continue;
            }

            if (isOptional)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    return null;
                }
            }

            break;
        }

        return input;
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
            string userInput = Console.ReadLine();
            string warningMessage = "Invalid input. Please enter y or n.";

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

    public int InputNumber(string label)
    {
        while (true)
        {
            try
            {
                // enter moive duration in minutes
                Console.WriteLine(label);
                Console.Write("> ");
                string numberStr = Console.ReadLine();

                if (Helpers.IsDigitsOnly(numberStr) && !string.IsNullOrWhiteSpace(numberStr))
                {
                    return int.Parse(numberStr);
                }
                else
                {
                    Helpers.WarningMessage("This field accepts numbers only.");
                }
            }
            catch
            {
                Helpers.WarningMessage("Number is to long enter a smaller number");
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
            userInput = Console.ReadLine();
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
                string enteredDate = Console.ReadLine();

                DateOnly dateObject = DateOnly.ParseExact(enteredDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                if(isDateOnly == true){
                    return dateObject;
                }

                if(isDateOnly == false){
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

                string enterDateTime = Console.ReadLine();
                DateTime dateObject = DateTime.ParseExact(enterDateTime, "HH:mm dd-MM-yyyy", CultureInfo.InvariantCulture);

                if(isDateOnly == true){
                    return dateObject;
                }

                if(isDateOnly == false){
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

        if(customName == null){
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

            if(substractOne){
                return modelIndex-1;
            }

            return modelIndex;
        }
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
