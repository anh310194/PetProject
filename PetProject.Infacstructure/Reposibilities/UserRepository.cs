using PetProject.Domain.Entities;
using PetProject.Infacstructure.Context;
using PetProject.Interfaces.Repositories;
using PetProject.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace PetProject.Infacstructure.Reposibilities
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(PetProjectContext context) : base(context)
        {

        }

        public User? GetUserByUserName(string? userName)
        {
            return QueryUserByUserName(userName).FirstOrDefault();
        }
        private IQueryable<User> QueryUserByUserName(string? userName)
        {
            return Queryable(p => !string.IsNullOrEmpty(userName) && p.UserName == userName);
        }

        public Task<User?> GetUserByUserNameAsync(string? userName)
        {
            return QueryUserByUserName(userName).FirstOrDefaultAsync();
        }
    }
}
