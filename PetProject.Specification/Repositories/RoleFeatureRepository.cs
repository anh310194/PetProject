using Microsoft.EntityFrameworkCore;
using PetProject.Domain.Entities;
using PetProject.Shared.Interfaces;
using PetProject.Specification.Common;
using PetProject.Specification.Interfaces;

namespace PetProject.Specification.Repositories
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
