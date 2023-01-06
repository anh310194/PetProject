using PetProject.Domain.Entities;

namespace PetProject.Interfaces.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    User? GetUserByUserName(string? userName);
    Task<User?> GetUserByUserNameAsync(string? userName);
}
