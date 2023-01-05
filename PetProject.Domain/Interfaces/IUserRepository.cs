using PetProject.Domain.Entities;

namespace PetProject.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User? GetUserByUserName(string? userName);
    }
}
