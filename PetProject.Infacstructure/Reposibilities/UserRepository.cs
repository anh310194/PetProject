using PetProject.Domain.Entities;
using PetProject.Infacstructure.Context;
using PetProject.Infacstructure.Interfaces;
using PetProject.Repositories.Common;

namespace PetProject.Infacstructure.Reposibilities
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(PetProjectContext context) : base(context)
        {

        }

        public User? GetUserByUserName(string userName)
        {
            return Queryable(p => p.UserName == userName).FirstOrDefault();
        }
    }
}
