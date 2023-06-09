namespace CinemaApp;

[TestClass]
public class AccountLogicTest
{
    private AccountsLogic _accountsLogic;
    private AccountAccess _accountAccess;

    [TestInitialize]
    public void Initialize()
    {
        _accountsLogic = new AccountsLogic();
        _accountAccess = new AccountAccess();
    }

    [TestMethod]
    public void TestGetAccounts()
    {
        List<AccountModel> accounts = _accountsLogic.GetAccounts();
        Assert.IsNotNull(accounts);
        Assert.AreEqual(0, accounts.Count);
    }

    [TestMethod]
    public void TestEmailExists_WhenEmailDoesNotExist_ReturnsFalse()
    {
        // Arrange
        AccountModel existingAccount = new AccountModel("username", "password", "existing@example.com", "123456789", "John", "Doe", true, "role");
        _accountsLogic.AddAccount(existingAccount);

        // Act
        bool emailExists = _accountsLogic.EmailExists("nonexisting@example.com");

        // Assert
        Assert.IsFalse(emailExists);
    }

    [TestMethod]
    public void TestAddAccount()
    {
        // Arrange
        AccountModel newAccount = new AccountModel("username", "password", "new@example.com", "123456789", "Jane", "Smith", false, "role");

        // Act
        _accountsLogic.AddAccount(newAccount);
        List<AccountModel> accounts = _accountsLogic.GetAccounts();

        // Assert
        Assert.AreEqual(1, accounts.Count);
        Assert.AreEqual("new@example.com", accounts[0].EmailAddress);
    }

    [TestMethod]
    public void TestGetById_WhenIdExists_ReturnsAccount()
    {
        // Arrange
        AccountModel existingAccount = new AccountModel("username", "password", "test@example.com", "123456789", "John", "Doe", true, "role", "1");
        _accountsLogic.AddAccount(existingAccount);

        // Act
        AccountModel account = _accountsLogic.GetById("1");

        // Assert
        Assert.IsNotNull(account);
        Assert.AreEqual("1", account.Id);
    }

    [TestMethod]
    public void TestGetById_WhenIdDoesNotExist_ReturnsNull()
    {
        // Arrange
        AccountModel existingAccount = new AccountModel("username", "password", "test@example.com", "123456789", "John", "Doe", true, "role", "1");
        _accountsLogic.AddAccount(existingAccount);

        // Act
        AccountModel account = _accountsLogic.GetById("2");

        // Assert
        Assert.IsNull(account);
    }

    [TestMethod]
    public void TestCheckLogin_WhenValidCredentials_ReturnsAccount()
    {
        // Arrange
        AccountModel existingAccount = new AccountModel("username", "password", "test@example.com", "123456789", "John", "Doe", true, "role");
        _accountsLogic.AddAccount(existingAccount);

        // Act
        AccountModel account = _accountsLogic.CheckLogin("test@example.com", "password");

        // Assert
        Assert.IsNotNull(account);
        Assert.AreEqual("test@example.com", account.EmailAddress);
        Assert.AreEqual("password", account.Password);
    }

    [TestMethod]
    public void TestCheckLogin_WhenInvalidCredentials_ReturnsNull()
    {
        // Arrange
        AccountModel existingAccount = new AccountModel("username", "password", "test@example.com", "123456789", "John", "Doe", true, "role");
        _accountsLogic.AddAccount(existingAccount);

        // Act
        AccountModel account = _accountsLogic.CheckLogin("test@example.com", "wrongpassword");

        // Assert
        Assert.IsNull(account);
    }

    [TestCleanup]
    public void Cleanup()
    {
        // Clean up test data
        List<AccountModel> accounts = new();
        _accountAccess.WriteAll(accounts);
    }


}
