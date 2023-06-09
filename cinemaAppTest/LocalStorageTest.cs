namespace CinemaApp;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

[TestClass]
public class LocalStorageTests
{
    [TestInitialize]
    public void Initialize()
    {
        // Clear storage before each test
        LocalStorage.ClearStorage();
    }

    [TestMethod]
    public void TestSetItemAndGetItem()
    {
        // Arrange
        string key = "testKey1";
        string value = "testValue";

        // Act
        LocalStorage.setItem(key, value);
        var retrievedValue = LocalStorage.GetItem(key);

        // Assert
        Assert.AreEqual(value, retrievedValue);
    }

    [TestMethod]
    public void TestWriteToStorage()
    {
        // Arrange
        AccountModel user = new AccountModel("username", "password", "test@example.com", "123456789", "John", "Doe", true, "role");

        // Act
        LocalStorage.WriteToStorage(user);

        // Assert
        var jsonString = File.ReadAllText("data/localStorage.json");
        var data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(jsonString);
        Assert.AreEqual("True", data["authenticated"].ToString());
        Assert.IsNotNull(data["user"]);
    }

    [TestMethod]
    public void TestLoadLocalStorage()
    {
        // Arrange
        AccountModel user = new AccountModel("username", "password", "test@example.com", "123456789", "John", "Doe", true, "role");
        LocalStorage.WriteToStorage(user);

        // Act
        LocalStorage.LoadLocalStorage();

        // Assert
        var retrievedUser = LocalStorage.GetAuthenticatedUser();
        Assert.IsNotNull(retrievedUser);
        Assert.AreEqual(user.Username, retrievedUser.Username);
    }

    [TestMethod]
    public void TestLocalStorageKeyCheck_WhenKeyExists_ReturnsTrue()
    {
        LocalStorage.ClearStorage();
        // Arrange
        string key = "testKey2";
        string value = "testValue";
        LocalStorage.setItem(key, value);

        // Act
        var result = LocalStorage.LocalStorageKeyCheck(key);
        if(result.ToString() == "True") {

        }
        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void TestLocalStorageKeyCheck_WhenKeyDoesNotExist_ReturnsFalse()
    {
        // Arrange
        string key = "nonExistentKey";

        // Act
        var result = LocalStorage.LocalStorageKeyCheck(key);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void TestClearStorage()
    {
        // Arrange
        string key = "testKey3";
        string value = "testValue";
        LocalStorage.setItem(key, value);

        // Act
        LocalStorage.ClearStorage();
        LocalStorage.localStorage = new Dictionary<string, dynamic>();

        // Assert
        Assert.IsFalse(LocalStorage.LocalStorageKeyCheck(key));
    }

    [TestMethod]
    public void TestAddToHistory()
    {
        // initialize empty storage
        LocalStorage.WriteToStorage();
        // Arrange
        string viewName = "HomePage";

        // Act
        LocalStorage.AddToHistory(viewName);

        // Assert
        var history = LocalStorage.GetItem("history") as List<string>;
        Assert.IsNotNull(history);
        Assert.IsTrue(history.Contains(viewName));
    }

    [TestMethod]
    public void TestLastVisitedPage_WhenHistoryExists_ReturnsLastVisitedPage()
    {
        // Arrange
        string viewName1 = "Page1";
        string viewName2 = "Page2";
        LocalStorage.AddToHistory(viewName1);
        LocalStorage.AddToHistory(viewName2);

        // Act
        var lastVisitedPage = LocalStorage.LastVisitedPage();

        // Assert
        Assert.AreEqual(viewName1, lastVisitedPage);
        LocalStorage.ClearStorage();
        LocalStorage.localStorage = new Dictionary<string, dynamic>();
    }

    [TestMethod]
    public void TestLastVisitedPage_WhenHistoryDoesNotExist_ReturnsNull()
    {
        // Act
        var lastVisitedPage = LocalStorage.LastVisitedPage();

        // Assert
        Assert.IsNull(lastVisitedPage);
    }


    [TestCleanup]
    public void Cleanup()
    {
        // Clear storage after each test
        LocalStorage.ClearStorage();
    }
}
