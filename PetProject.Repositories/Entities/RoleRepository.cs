using PetProject.Entities;
using PetProject.Interfaces.Common;
using PetProject.Interfaces.Reponsitories;
using PetProject.Repositories.Common;

namespace PetProject.Repositories.Entities
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(IDataContext dbContext) : base(dbContext) { }
    }
}
