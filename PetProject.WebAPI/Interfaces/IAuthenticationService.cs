using PetProject.Business.Models;
using PetProject.WebAPI.Models.Responses;

namespace PetProject.WebAPI.Interfaces
{
    public interface IAuthenticationService
    {
        UserTokenModel CurrentUser { get; }
        TokenModel GetTokenModel(SignInModel signInUser);
    }
}
