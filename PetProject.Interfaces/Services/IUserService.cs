using PetProject.Models;

namespace PetProject.Interfaces.Services
{
    public interface IUserService
    {
        Task<SignInModel> Authenticate(string userName, string password);
    }
}
