using PetProject.Domain.Entities;
using PetProject.Infacstructure.Context;
using PetProject.Repositories.Common;
using PetProject.Interfaces.Repositories;

namespace PetProject.Infacstructure.Reposibilities
{
    public class DateTimeFormatRepository : GenericRepository<DateTimeFormat>, IDateTimeFormatRepository
    {
        public DateTimeFormatRepository(PetProjectContext context) : base(context)
        {

        }
    }
}
