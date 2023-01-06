
namespace PetProject.Business.Models;

public class SignInModel
{
    public string? UserName { get; set; }
    public long[]? Roles { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int UserType { get; set; }

}
