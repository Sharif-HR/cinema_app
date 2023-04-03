public class AccountAccess : Access<AccountModel>
{
    public AccountAccess(string overwritePath=null) : base("data/accounts.json", overwritePath) { }
}
