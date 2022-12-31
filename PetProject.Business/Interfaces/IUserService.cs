using PetProject.Business.Models;

namespace PetProject.Business.Interfaces
{
    public interface IUserService
    {
        Task<SignInModel> Authenticate(string userName, string password);
    }
}
