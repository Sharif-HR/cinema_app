abstract class ViewTemplate {
    private string Title;

    public ViewTemplate(string title) {
        this.Title = title;
    }
    public virtual void Render() {
        Console.Clear();
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
}
