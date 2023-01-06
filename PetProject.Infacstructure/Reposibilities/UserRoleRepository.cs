using PetProject.Domain.Entities;
using PetProject.Interfaces.Repositories;
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
