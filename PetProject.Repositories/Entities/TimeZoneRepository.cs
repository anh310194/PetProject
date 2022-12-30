using Microsoft.EntityFrameworkCore;
using PetProject.Interfaces.Reponsitories;
using PetProject.Repositories.Common;

namespace PetProject.Repositories.Entities
{
    public class TimeZoneRepository : GenericRepository<PetProject.Entities.TimeZone>, ITimeZoneRepository
    {
        public TimeZoneRepository(DbContext dbContext) : base(dbContext) { }
    }
}
