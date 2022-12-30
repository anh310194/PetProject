using PetProject.Entities;
using PetProject.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using PetProject.Interfaces.Reponsitories;

namespace PetProject.Repositories.Entities
{
    public class RoleFeatureRepository : GenericRepository<RoleFeature>, IRoleFeatureRepository
    {
        public RoleFeatureRepository(DbContext dbContext) : base(dbContext) { }

        public override IQueryable<RoleFeature> Queryable()
        {
            return _dbSet.AsQueryable<RoleFeature>().Where(p => p.Status == 1);
        }
    }
}
