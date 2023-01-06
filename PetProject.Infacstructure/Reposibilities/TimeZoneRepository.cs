using PetProject.Infacstructure.Context;
using PetProject.Repositories.Common;
using PetProject.Interfaces.Repositories;

namespace PetProject.Infacstructure.Reposibilities
{
    public class TimeZoneRepository : GenericRepository<Domain.Entities.TimeZone>, ITimeZoneRepository
    {
        public TimeZoneRepository(PetProjectContext context) : base(context)
        {

        }
    }
}
