using PetProject.Domain.Entities;
using PetProject.Infacstructure.Context;
using PetProject.Repositories.Common;

namespace PetProject.Infacstructure.Reposibilities
{
    internal class TimeZoneRepository : GenericRepository<Domain.Entities.TimeZone>, ITimeZoneRepository
    {
        public TimeZoneRepository(PetProjectContext context) : base(context)
        {

        }
    }
}
