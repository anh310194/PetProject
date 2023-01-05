using PetProject.Domain.Entities;
using PetProject.Domain.Interfaces;
using PetProject.Infacstructure.Context;
using PetProject.Repositories.Common;

namespace PetProject.Infacstructure.Reposibilities
{
    public class UserRoleRepository: GenericRepository<UserRole> , IUserRoleRepository
    {
        public UserRoleRepository(PetProjectContext context) : base(context)
        {

        }
    }
}
