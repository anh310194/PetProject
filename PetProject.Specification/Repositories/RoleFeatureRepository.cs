using Microsoft.EntityFrameworkCore;
using PetProject.Domain.Entities;
using PetProject.Specification.Common;

namespace PetProject.Specification.Repositories
{
    public class RoleFeatureRepository : GenericRepository<RoleFeature>
    {
        public RoleFeatureRepository(DbContext context) : base(context) { }

        public override IQueryable<RoleFeature> Queryable()
        {
            return _dbSet.AsQueryable<RoleFeature>().Where(p => p.Status == 1);
        }
    }
}
