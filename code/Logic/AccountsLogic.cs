using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


public class AccountsLogic: LogicTemplate
{
    private AccountAccess _accountAccess;
    private List<AccountModel> _accountsList;

    private void ReloadAccounts(){
        _accountsList = _accountAccess.LoadAll();
    }

    public AccountsLogic(string overWritePath=null): base()
    {
        _accountAccess = new(overWritePath);
        ReloadAccounts();
    }


    public List<AccountModel> GetAccounts(){
        ReloadAccounts();
        return _accountsList;
    }


    public bool EmailExists(string email){

        int foundEmails = _accountsList.FindIndex(s => s.EmailAddress == email);

        if(foundEmails > 0){
            Helpers.WarningMessage($"The email: {email} is already registerd please enter another email.");
            return true;
        }

        return false;

    }


    public void AddAccount(AccountModel acc)
    {
        _accountsList.Add(acc);
        _accountAccess.WriteAll(_accountsList);
    }

    public AccountModel GetById(string id)
    {
        return _accountsList.Find(i => i.Id == id);
    }

    public AccountModel CheckLogin(string email, string password)
    {
        _accountsList = _accountAccess.LoadAll();

        if (email == null || password == null)
        {
            return null;
        }

        AccountModel foundAccount = _accountsList.Find(i => i.EmailAddress == email && i.Password == password);
        return foundAccount;
    }

    public void Logout() {
        Dictionary<string, dynamic> emptyStorage = new();
        LocalStorage.localStorage = emptyStorage;

        LocalStorage.WriteToStorage();
        Views.RouteHandeler.View("MenuPage");
    }

}




