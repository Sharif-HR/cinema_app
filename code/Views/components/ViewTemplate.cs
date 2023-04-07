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
                if (!containsSC || hasError){
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
                string userInput = Console.ReadLine();
                if (DateOnly.TryParse(userInput, out DateOnly result))
                {
                    if (isDateOnly)
                    {
                        return result;
                    }
                    else
                    {
                        return result.ToString();
                    }
                }
                else
                {
                    Helpers.WarningMessage("Invalid Date input. Date format must be (DD-MM-YYYY) -> (20-10-2020).");

                }
            }
            catch
            {
                Helpers.WarningMessage("Invalid Date input. Date format must be (DD-MM-YYYY) -> (20-10-2020).");
            }
        }
    }

    public virtual List<T> EditModelFromList<T>(List<T> modelList)
    {
        if (modelList.Count == 0)
        {
            return modelList;
        }

        bool loop = true;
        while (loop)
        {
            int modelIndex = InputNumber("Enter movie ID:");
            if (modelIndex == 0)
            {
                modelIndex++;
            }
            else
            {
                modelIndex--;
            }

            if (modelList.Count < modelIndex || modelIndex < 0)
            {
                Helpers.WarningMessage("Enter a correct ID");
            }
            else
            {

                Dictionary<string, Type> properties = new Dictionary<string, Type>();
                foreach (var prop in typeof(T).GetProperties())
                {
                    properties[prop.Name] = prop.PropertyType;
                }

                Console.WriteLine("Choose what you want to edit:");
                foreach (var attr in properties)
                {
                    if (attr.Key != "Id")
                    {
                        Console.WriteLine(attr.Key);
                    }
                }


                string userInput;
                while (true)
                {
                    userInput = InputField("");
                    if (!properties.ContainsKey(userInput))
                    {
                        Helpers.WarningMessage("Incorrect input.");
                        continue;
                    }
                    break;
                }


                string editInput = InputField($"Edit {userInput}:");


                Type chosenProperty = properties[userInput];
                object toUpdatedValue;
                switch (chosenProperty)
                {

                    case var t when t == typeof(int):
                        toUpdatedValue = Int32.Parse(editInput);
                        break;

                    case var t when t == typeof(string):
                        break;
                }

                PropertyInfo propertyInfo = typeof(T).GetProperty("Title");
                propertyInfo.SetValue(modelList[modelIndex], userInput);
                loop = false;
            }
        }
        Helpers.SuccessMessage("Movie updated!");
        return modelList;
    }

    public virtual List<MovieModel> EditMovie(List<MovieModel> movieList) {
        if (movieList.Count == 0)
        {
            return movieList;
        }

        bool loop = true;
        while (loop)
        {
            int modelIndex = InputNumber("Enter movie ID:");
            if (modelIndex == 0)
            {
                modelIndex++;
            }
            else
            {
                modelIndex--;
            }

            if (movieList.Count < modelIndex || modelIndex < 0)
            {
                Helpers.WarningMessage("Enter a correct ID");
            }
            else
            {

                Dictionary<string, Type> properties = new Dictionary<string, Type>();
                foreach (var prop in typeof(MovieModel).GetProperties())
                {
                    properties[prop.Name] = prop.PropertyType;
                }

                Console.WriteLine("Choose what you want to edit:");
                foreach (var attr in properties)
                {
                    if (attr.Key != "Id")
                    {
                        Console.WriteLine(attr.Key);
                    }
                }


                string userInput;
                while (true)
                {
                    userInput = InputField("");
                    if (!properties.ContainsKey(userInput))
                    {
                        Helpers.WarningMessage("Incorrect input.");
                        continue;
                    }
                    break;
                }
                string editInput;
                object toUpdatedValue;
                toUpdatedValue = "";

                while(true) {
                    editInput = InputField($"Edit {userInput}:");

                    Type chosenProperty = properties[userInput];
                    switch (chosenProperty)
                    {
                        case var t when t == typeof(int):
                            try {
                                toUpdatedValue = Int32.Parse(editInput);
                            }
                            catch (Exception e){
                                Helpers.WarningMessage(e.Message);
                                continue;
                            }
                            break;

                        case var t when t == typeof(string):
                            toUpdatedValue = editInput;
                            break;
                    }
                    break;
                }

                PropertyInfo propertyInfo = typeof(MovieModel).GetProperty(userInput);
                propertyInfo.SetValue(movieList[modelIndex], toUpdatedValue);
                loop = false;
            }
        }
        return movieList;
    }
    public virtual List<T> DeleteModelFromList<T>(List<T> modelList)
    {
        if (modelList.Count == 0)
        {
            return modelList;
        }
        bool loop = true;
        while (loop)
        {
            int modelIndex = InputNumber("Enter movie ID:");

            if (modelList.Count < modelIndex || modelIndex < 0)
            {
                Helpers.WarningMessage("Enter a correct ID");
            }
            else
            {

                if (modelIndex == 0)
                {
                    modelIndex++;
                }
                else
                {
                    modelIndex--;
                }

                modelList.RemoveAt(modelIndex);
                loop = false;
            }
        }
        Helpers.SuccessMessage("Movie Deleted!");
        return modelList;
    }

    public virtual List<T> AddModel<T>(List<T> modelList, T model)
    {
        modelList.Add(model);
        return modelList;
    }

    private void CinemaLogo()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(@"


 ____          ___                               ____
/\  _`\    __ /\_ \                             /\  _`\    __
\ \,\L\_\ /\_\\//\ \    __  __     __   _ __    \ \ \/\_\ /\_\     ___       __     ___ ___       __
 \/_\__ \ \/\ \ \ \ \  /\ \/\ \  /'__`\/\`'__\   \ \ \/_/_\/\ \  /' _ `\   /'__`\ /' __` __`\   /'__`\
   /\ \L\ \\ \ \ \_\ \_\ \ \_/ |/\  __/\ \ \/     \ \ \L\ \\ \ \ /\ \/\ \ /\  __/ /\ \/\ \/\ \ /\ \L\.\_
   \ `\____\\ \_\/\____\\ \___/ \ \____\\ \_\      \ \____/ \ \_\\ \_\ \_\\ \____\\ \_\ \_\ \_\\ \__/.\_\
    \/_____/ \/_/\/____/ \/__/   \/____/ \/_/       \/___/   \/_/ \/_/\/_/ \/____/ \/_/\/_/\/_/ \/__/\/_/



");
        Console.ResetColor();
    }
}
