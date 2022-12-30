using PetProject.Entities;
using Microsoft.EntityFrameworkCore;
using PetProject.Interfaces.Reponsitories;
using PetProject.Repositories.Common;

namespace PetProject.Repositories.Entities
{
    public class FeatureRepository : GenericRepository<Feature>, IFeatureRepository
    {
        public FeatureRepository(DbContext dbContext) : base(dbContext) { }
    }
}
