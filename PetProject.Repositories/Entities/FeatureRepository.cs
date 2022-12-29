using PetProject.Entities;
using PetProject.Interfaces.Common;
using PetProject.Interfaces.Reponsitories;
using PetProject.Repositories.Common;

namespace PetProject.Repositories.Entities
{
    public class FeatureRepository : GenericRepository<Feature>, IFeatureRepository
    {
        public FeatureRepository(IDataContext dbContext) : base(dbContext) { }
    }
}
