using PetProject.Domain.Entities;
using PetProject.Infacstructure.Context;
using PetProject.Repositories.Common;

namespace PetProject.Infacstructure.Reposibilities
{
    internal class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(PetProjectContext context) : base(context)
        {

        }
    }
}
