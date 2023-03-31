static class Inputs {
    public static string? InputField(string label, bool inputIsChoice = false, List<string>? choices = null) {
        bool loop = true;
        choices = choices ?? new List<string>();
        string? input = null;

        while(loop) {
            Console.WriteLine(label + "\n>");
            input = Console.ReadLine();

            if(input == "") {
                continue;
            }

            if(inputIsChoice) {
                if(!choices.Contains(input)) {
                    continue;
                }
            }

            if(input != null) {
                loop = false;
            }
        }
        return input;
    }

    public static string? PasswordInput(bool mustBeStrong = true, int minLength = 8, int maxLength = 32) {
        Console.WriteLine("Enter your password: \n>");
        bool loop = true;
        string input = "";

        while(loop) {
            input = Helpers.PasswordToAstriks();
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
            }
        }

        return input;
    }

    public static string? OptionalInput(string label) {
        Console.WriteLine(label + "\n>");
        string? input = Console.ReadLine();

        return input;
    }

    public static bool CheckboxInput(string label) {
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
}
