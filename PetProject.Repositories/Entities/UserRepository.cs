using PetProject.Entities;
using PetProject.Interfaces.Common;
using PetProject.Interfaces.Reponsitories;
using PetProject.Repositories.Common;

namespace PetProject.Repositories.Entities
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IDataContext dbContext) : base(dbContext)
        {

        }
    }
}
