using PetProject.Domain.Entities;
using PetProject.Infacstructure.Context;
using PetProject.Repositories.Common;
using PetProject.Interfaces.Repositories;

namespace PetProject.Infacstructure.Reposibilities
{
    public class RoleFeatureRepository : GenericRepository<RoleFeature>, IRoleFeatureRepository
    {
        public RoleFeatureRepository(PetProjectContext context) : base(context)
        {

        }
    }
}
