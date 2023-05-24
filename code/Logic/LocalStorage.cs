using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Reflection;

public static class LocalStorage {
    // Create the storage
    public static Dictionary<string, dynamic> localStorage = new();

    public static void setItem(string key, object value) => localStorage.Add(key, value);

    public static object? GetItem(string key) => LocalStorageKeyCheck(key) ? localStorage[key] : null;

    public static void WriteToStorage(AccountModel? loggedInUser = null) {
        AccountModel? user = loggedInUser ?? GetAuthenticatedUser();
        JsonNode mainJson = JsonNode.Parse("{}");

        // goed
        mainJson["authenticated"] = (user != null) ? true : false;

        JsonObject userJson = new();

        try {
            // create a list of the properties of the AccountModel class
            PropertyInfo[] properties = user.GetType().GetProperties();

            // loop through the properties
            foreach (PropertyInfo property in properties) {
                // assign the property name as the json key with the value of the property as its value. Example: "firstName": "test"
                userJson[Helpers.DecapitalizeString(property.Name)] = JsonNode.Parse(JsonSerializer.Serialize(property.GetValue(user)));
            }

            mainJson["user"] = userJson;
        } catch {
            mainJson["user"] = null;
        }

        try
        {
            JsonArray historyArray = new JsonArray();

            foreach(var item in localStorage["history"]) {
                historyArray.Add(item);
            }

            mainJson["history"] = historyArray;
        }
        catch (KeyNotFoundException)
        {
            mainJson["history"] = new JsonArray();
        }

        var options = new JsonSerializerOptions {
            WriteIndented = true
        };

        var jsonString = JsonSerializer.Serialize(mainJson, options);

        using (StreamWriter sw = File.CreateText("data/localStorage.json")){ sw.WriteLine(jsonString); }
        LoadLocalStorage();
    }

    public static void LoadLocalStorage() {
        var json = File.ReadAllText("data/localStorage.json");
        var data = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        foreach (var item in data)
        {
            if(item.Key == "user") {
                if(item.Value == null) {
                    localStorage[item.Key] = null;
                } else {
                    localStorage[item.Key] = JsonSerializer.Deserialize<AccountModel>(item.Value.ToString());
                }
            } else if (item.Key == "history") {
                JsonElement historyElement = (JsonElement)data["history"];
                List<string> historyList = new List<string>();

                if(historyElement.ValueKind == JsonValueKind.Array) {
                    foreach (JsonElement element in historyElement.EnumerateArray())
                    {

                        if(element.ValueKind == JsonValueKind.String) {
                            historyList.Add(element.GetString());
                        }
                    }
                    localStorage["history"] = historyList;
                }
            }
            else {
                localStorage[item.Key] = item.Value;
            }
        }
    }

    public static bool LocalStorageKeyCheck(string key) {
        return (localStorage.ContainsKey(key) && localStorage[key] != null);
    }

    public static AccountModel? GetAuthenticatedUser() {
        AccountModel? user = null;
        if(LocalStorageKeyCheck("user")) {
            user = (AccountModel)localStorage["user"];
        }

        return user;
    }

    public static bool IsAuthenticated() {
        if(LocalStorageKeyCheck("authenticated")) {
            return (bool)localStorage["authenticated"];
        }

        return false;
    }

    public static void ClearStorage() {
        List<string> data = new(){};
        File.WriteAllLines("data/localStorage.json", data);
    }

    public static void AddToHistory(string viewName) {
        if(LocalStorageKeyCheck("history")) {
            var options = new JsonSerializerOptions { WriteIndented = true };

            localStorage["history"].Add(viewName);
            WriteToStorage();
        }
    }

    public static string? LastVisitedPage() {
        if(LocalStorageKeyCheck("history")) {
            try
            {
                string viewName = localStorage["history"][localStorage["history"].Count - 2];
                localStorage["history"].Remove(GetLastViewName());
                WriteToStorage();
                return viewName;
            }
            catch (ArgumentOutOfRangeException)
            {
                Helpers.ErrorMessage("Something went wrong when we wanted to redirect you back");
            }
        }

        return null;
    }

    public static string? GetLastViewName() {
        if (LocalStorageKeyCheck("history"))
        {
            if(localStorage["history"].Count != 0) return localStorage["history"][localStorage["history"].Count - 1];
        }

        return null;
    }
}
