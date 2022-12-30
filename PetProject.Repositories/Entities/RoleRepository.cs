using PetProject.Entities;
using Microsoft.EntityFrameworkCore;
using PetProject.Interfaces.Reponsitories;
using PetProject.Repositories.Common;

namespace PetProject.Repositories.Entities
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext dbContext) : base(dbContext) { }
    }
}
