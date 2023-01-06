
using System.Security.Cryptography;

namespace PetProject.Models;

public class SignInModel
{
    private string? _userName;
    private long[]? _roles;
    private string? _firstName;
    private string? _lastName;
    private int _userType;

    public string? UserName
    {
        get { return _userName; }
        set { _userName = value; }
    }
    public long[]? Roles
    {
        get { return _roles; }
        set { _roles = value; }
    }
    public string? FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }
    public string? LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }
    public int UserType
    {
        get { return _userType; }
        set { _userType = value; }
    }

}
