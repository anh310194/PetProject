using PetProject.Domain.Entities;

namespace PetProject.Infacstructure.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User? GetUserByUserName(string userName);
    }
}
