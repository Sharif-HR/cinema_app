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

    public string InputPassword(string label){
        Console.WriteLine(label);
        Console.Write("> ");

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
                return pass;
            }
        }
    }
}
