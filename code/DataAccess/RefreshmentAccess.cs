public class RefreshmentAccess : Access<RefreshmentModel>
{
    public RefreshmentAccess(string overwritePath = null) : base("data/refreshments.json", overwritePath) { }

}
