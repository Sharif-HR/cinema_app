using System.Text.Json;

public abstract class Access<Model>
{
    public string _path {get;set;}

    public Access(string dataPath, string overwriteRootPath= null){
        if(overwriteRootPath != null){
            _path = @$"{overwriteRootPath}/{dataPath}";
        }
        else{
            _path = System.IO.Path.GetFullPath(System.IO.Path.Combine(Environment.CurrentDirectory, @$"{dataPath}"));
        }

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