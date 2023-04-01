using System.Text.Json.Serialization;


class AccountModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; }

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string LastName { get; set; }

    [JsonPropertyName("isStudent")]
    public bool IsStudent { get; set; }

    [JsonPropertyName("role")]
    public string Role { get; set; }

    public AccountModel(string username, string password, string emailAddress, string phoneNumber, string firstName, string lastName, bool isStudent, string role, string id = null)
    {
        Id = string.IsNullOrWhiteSpace(id) ? Helpers.GenUid() : id;
        Username = username;
        EmailAddress = emailAddress;
        Password = password;
        PhoneNumber = phoneNumber;
        FirstName = firstName;
        LastName = lastName;
        IsStudent = isStudent;
        Role = role;
    }
}




