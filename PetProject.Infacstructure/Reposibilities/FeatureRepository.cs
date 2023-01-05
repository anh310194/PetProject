using PetProject.Domain.Entities;
using PetProject.Infacstructure.Context;
using PetProject.Repositories.Common;

namespace PetProject.Infacstructure.Reposibilities
{
    internal class FeatureRepository : GenericRepository<Feature>, IFeatureRepository
    {
        public FeatureRepository(PetProjectContext context) : base(context)
        {

        }
    }
}
