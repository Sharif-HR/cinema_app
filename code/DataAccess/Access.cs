using System.Text.Json;

public abstract class Access<Model>
{
    private static string _path;
    public Access(string path){
        _path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @$"{path}"));
    }
    // Data/accounts.json
    public virtual List<Model> LoadAll()
    {
        string json = File.ReadAllText(_path);
        return JsonSerializer.Deserialize<List<Model>>(json);
    }

    public virtual void WriteAll(List<Model> objects)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(objects, options);
        File.WriteAllText(_path, json);
    }
}