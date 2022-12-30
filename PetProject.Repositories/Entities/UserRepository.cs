using PetProject.Entities;
using Microsoft.EntityFrameworkCore;
using PetProject.Interfaces.Reponsitories;
using PetProject.Repositories.Common;

namespace PetProject.Repositories.Entities
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
