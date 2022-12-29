using PetProject.Entities;
using PetProject.Interfaces.Common;
using PetProject.Interfaces.Reponsitories;
using PetProject.Repositories.Common;

namespace PetProject.Repositories.Entities
{
    public class DateTimeFormatRepository : GenericRepository<DateTimeFormat>, IDateTimeFormatRepository
    {
		public DateTimeFormatRepository(IDataContext dbContext) : base(dbContext) { }
    }
}
