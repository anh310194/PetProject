namespace PetProject.WebAPI.Models.Responses;

public class TokenModel
{
    public string? Token { get; set; }
    public string? Type { get; set; }
    public double ExpiredTime { get; set; }
}

public class UserTokenModel
{
    public string? UserName { get; set; }
    public List<long>? Roles { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserType { get; set; }
    public string? IdentityId { get; set;}
}
