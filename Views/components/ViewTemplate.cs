using System.Globalization;

abstract class ViewTemplate {
    private string Title;

    public ViewTemplate(string title) {
        this.Title = title;
    }
    public virtual void Render() {
        Console.Clear();
        this.CinemaLogo();
        Console.WriteLine("---------------------------");
        Console.WriteLine(this.Title);
        Console.WriteLine("---------------------------");
    }

    public string InputField(string label) {
        while(true){
            Console.WriteLine(label);
            Console.Write("> ");
            string userInput = Console.ReadLine();

            if(!string.IsNullOrWhiteSpace(userInput)){
                return userInput;
            }
        }
    }

    public string PasswordToAstriks(){
        var pass = string.Empty;
        ConsoleKey key;

        while(true)
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

            if(key == ConsoleKey.Enter && !string.IsNullOrWhiteSpace(pass)){
                Console.WriteLine();
                return pass;
            }
        }
    }

    public string InputPassword(string label, bool mustBeStrong = false, int minLength = 8, int maxLength = 32) {
        bool loop = true;
        string input = "";

        while(loop) {
            Console.WriteLine(label);
            Console.Write("> ");
            input = this.PasswordToAstriks();

            if(mustBeStrong) {
                if(input.Length < minLength || input.Length > maxLength) {
                    Console.WriteLine($"Password must be at least {minLength} characters and max {maxLength} characters long.");
                    continue;
                }

                if(!input.Any(char.IsUpper)) {
                    Console.WriteLine("Password must contain at least one uppercase character.");

                    continue;
                }

                if(!input.Any(char.IsLower)) {
                    Console.WriteLine("Password must contain at least one lowercase character.");
                    continue;
                }

                if(input.Contains(" ")) {
                    Console.WriteLine("Password can't contain a white space.");
                    continue;
                }

                string specialChars = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
                char[] specialCh = specialChars.ToCharArray();
                foreach(char ch in specialCh) {
                    if(input.Contains(ch)) {
                        loop = false;
                        return input;
                    }
                }
            } else {
                loop = false;
            }
        }
        return input;
    }

    public string OptionalInput(string label) {
        Console.WriteLine(label + "\n>");
        string? input = Console.ReadLine();
        return input;
    }


    public bool CheckboxInput(string label) {
        List<string> choices = new(){"Y", "N"};
        bool loop = true;

        Console.WriteLine(label + "Y/n");
        string? input = Console.ReadLine() ?? "";
        while(loop) {
            if(!choices.Contains(input.ToUpper())) {
                Console.WriteLine($"{input} is not a valid option.");
            }
            loop = false;
        }
        return (input == "Y" || input == "y") ? true : false;
    }

    public int InputNumber(string label){
        while(true){
            // enter moive duration in minutes
            Console.WriteLine(label);
            Console.Write("> ");
            string numberStr = Console.ReadLine();

            if(Helpers.IsDigitsOnly(numberStr) && !string.IsNullOrWhiteSpace(numberStr)){
                return int.Parse(numberStr);
            }
            else{
                Helpers.WarningMessage("This field accepts numbers only.");
            }
        }
    }


    public List<string> InputMultiple(string label){
        Console.WriteLine(label);
        Helpers.WarningMessage("(Enter multiple values comma separated)");
        Console.Write("> ");
        string userInput = Console.ReadLine();

         
        List<string> values = new List<string>(userInput.Split(','));
        
        for(int i =0; i < values.Count; i++){
            values[i] = values[i].Trim();
        }

        return values;
    }


    public object InputDate(string label, bool isDateOnly=true){
        while (true)
        {
            try{
                Console.WriteLine("Enter date in this format (YYYY-MM-DD)");
                Console.WriteLine(label);
                string userInput = Console.ReadLine();
                if (DateOnly.TryParse(userInput, out DateOnly result))
                {
                    if(isDateOnly){
                        return result;
                    }
                    else{
                        return result.ToString();
                    }
                }
                else{
                    Helpers.WarningMessage("Invalid Date input. Date format must be (YYYY-MM-DD) -> (2020-10-20).");

                }
            }
            catch{
                Helpers.WarningMessage("Invalid Date input. Date format must be (YYYY-MM-DD) -> (2020-10-20).");
            }
        }
    }


    private void CinemaLogo() {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(@"
   _____ _ _                   _____ _
  / ____(_) |                 / ____(_)
 | (___  _| |_   _____ _ __  | |     _ _ __   ___ _ __ ___   __ _
  \___ \| | \ \ / / _ \ '__| | |    | | '_ \ / _ \ '_ ` _ \ / _` |
  ____) | | |\ V /  __/ |    | |____| | | | |  __/ | | | | | (_| |
 |_____/|_|_| \_/ \___|_|     \_____|_|_| |_|\___|_| |_| |_|\__,_|
        ");
        Console.ResetColor();
    }
}
