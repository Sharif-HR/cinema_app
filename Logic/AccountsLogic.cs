using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class AccountsLogic
{
    private AccountAccess _accountAccess = new();
    private List<AccountModel> _accountsList;

    private void ReloadAccounts(){
        _accountsList = _accountAccess.LoadAll();
    }

    public AccountsLogic()
    {
        ReloadAccounts();
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



}




