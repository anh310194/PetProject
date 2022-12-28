using PetProject.Entities;
using PetProject.Infacstructure.Interfaces;

namespace PetProject.Infacstructure.Repositories
{
    public class RoleFeatureRepository : GenericRepository<RoleFeature>
    {
        public RoleFeatureRepository(IDataContext dataContext) : base(dataContext) { }

        public override IQueryable<RoleFeature> Queryable()
        {
            return _dbSet.AsQueryable<RoleFeature>().Where(p => p.Status == 1);
        }
    }
}
