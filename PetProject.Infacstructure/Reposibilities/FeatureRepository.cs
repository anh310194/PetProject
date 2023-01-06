using PetProject.Domain.Entities;
using PetProject.Infacstructure.Context;
using PetProject.Repositories.Common;
using PetProject.Interfaces.Repositories;

namespace PetProject.Infacstructure.Reposibilities
{
    public class FeatureRepository : GenericRepository<Feature>, IFeatureRepository
    {
        public FeatureRepository(PetProjectContext context) : base(context)
        {

        }
    }
}
