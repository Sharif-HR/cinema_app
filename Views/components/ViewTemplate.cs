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
}
