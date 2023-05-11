public class RefreshmentsLogic : LogicTemplate
{
    private RefreshmentAccess _refreshmentAccess;
    private List<RefreshmentModel> _refreshmentList;

    public RefreshmentsLogic(string overwritePath = null) : base()
    {
        _refreshmentAccess = new(overwritePath);
        ReloadRefreshments();
    }

    private void ReloadRefreshments() => _refreshmentList = _refreshmentAccess.LoadAll();

    public void AddRefreshment(RefreshmentModel refreshment)
    {
        _refreshmentList.Add(refreshment);
        _refreshmentAccess.WriteAll(_refreshmentList);
    }

    public void EditRefreshment(int index, string propertyName, object updatedValue)
    {
        var property = _refreshmentList[index].GetType().GetProperty(propertyName);

        property.SetValue(_refreshmentList[index], updatedValue);
        _refreshmentAccess.WriteAll(_refreshmentList);        
    }

    public void DeleteRefreshment(int id){
        _refreshmentList.RemoveAt(id);
        _refreshmentAccess.WriteAll(_refreshmentList);
    }

    public void SaveRefreshments(List<RefreshmentModel> RefreshmentList = null)
    {
        if (RefreshmentList != null)
        {
            _refreshmentAccess.WriteAll(RefreshmentList);
        }
        else
        {
            _refreshmentAccess.WriteAll(_refreshmentList);
        }
    }

    public List<RefreshmentModel> GetRefreshments()
    {
        ReloadRefreshments();
        return _refreshmentList;
    }
}