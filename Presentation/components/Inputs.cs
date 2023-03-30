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
}
