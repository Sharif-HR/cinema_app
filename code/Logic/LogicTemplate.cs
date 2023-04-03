using ConsoleTables;

public abstract class LogicTemplate
{
    public virtual ConsoleTable GenerateModelTable<T>(List<T> modelList)
    {
        ConsoleTable modelTable;

        List<string> columns = typeof(T).GetProperties().Select(p => p.Name).ToList();
        var options = new ConsoleTableOptions
        {
            Columns = columns,
            EnableCount = false
        };

        modelTable = new ConsoleTable(options);

        for (int i = 0; i < modelList.Count; i++)
        {
            var rowContent = new List<string>();
            foreach (string column in columns)
            {
                string modelPropertyName = modelList[i].GetType().GetProperty(column).Name.ToString();
                if (modelPropertyName != "Id")
                {
                    object rawModelAttribute = modelList[i].GetType().GetProperty(column).GetValue(modelList[i]);

                    string strModelAttribute = rawModelAttribute.ToString();

                    if (rawModelAttribute is List<string>)
                    {
                        strModelAttribute = Helpers.ListToString((List<string>)rawModelAttribute);
                    }

                    if (strModelAttribute.Length > 15)
                    {
                        strModelAttribute = Helpers.TruncateString(strModelAttribute, 15);
                    }

                    rowContent.Add(strModelAttribute);
                }
                else{
                    rowContent.Add((i+1).ToString());
                }
            }

            string[] rowContentArray = rowContent.ToArray();
            modelTable.AddRow(rowContentArray);
        }

        return modelTable;
    }
}