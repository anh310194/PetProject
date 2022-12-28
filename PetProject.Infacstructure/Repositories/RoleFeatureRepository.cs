using PetProject.Entities;
using PetProject.Infacstructure.Database;

namespace PetProject.Infacstructure.Repositories
{
    public class RoleFeatureRepository : GenericRepository<RoleFeature>
    {
        public RoleFeatureRepository(PetProjectContext dataContext) : base(dataContext) { }

        public override IQueryable<RoleFeature> Queryable()
        {
            return _dbSet.AsQueryable<RoleFeature>().Where(p => p.Status == 1);
        }
    }
}
