using PetProject.Core.Data;
using PetProject.Core.Interfaces;
using PetProject.Entities;

namespace PetProject.Infacstructure.Repositories
{
    public class RoleFeatureRepository : Repository<RoleFeature>
    {
        public RoleFeatureRepository(IDbContext context) : base(context) { }

        public override IQueryable<RoleFeature> Queryable()
        {
            return _dbSet.AsQueryable<RoleFeature>().Where(p => p.Status == 1);
        }
    }
}
