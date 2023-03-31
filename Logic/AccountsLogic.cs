using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
class AccountsLogic
{
    private List<AccountModel> _accountsList;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public AccountModel? CurrentAccount { get; private set; }

    public AccountsLogic()
    {
        _accountsList = AccountsAccess.LoadAll();
    }


    public void UpdateList(AccountModel acc)
    {
        //Find if there is already an model with the same id
        int index = _accountsList.FindIndex(s => s.EmailAddress == acc.EmailAddress);

        if (index != -1)
        {
            //update existing model
            //_accounts[index] = acc;
            Console.WriteLine("Already found an account with the same email adress.");
        }
        else
        {
            //add new model
            _accountsList.Add(acc);
        }
        AccountsAccess.WriteAll(_accountsList);

    }

    public AccountModel GetById(string id)
    {
        return _accountsList.Find(i => i.Id == id);
    }

    public AccountModel CheckLogin(string email, string password)
    {
        if (email == null || password == null)
        {
            return null;
        }

        CurrentAccount = _accountsList.Find(i => i.EmailAddress == email && i.Password == password);
        return CurrentAccount;
    }

    public string GenerateUUID()
    {
        // Generates a new unique user ID.
        string uuid = Guid.NewGuid().ToString();
        return uuid;
    }
}




