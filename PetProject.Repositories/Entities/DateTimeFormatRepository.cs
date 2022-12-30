using PetProject.Entities;
using Microsoft.EntityFrameworkCore;
using PetProject.Interfaces.Reponsitories;
using PetProject.Repositories.Common;

namespace PetProject.Repositories.Entities
{
    public class DateTimeFormatRepository : GenericRepository<DateTimeFormat>, IDateTimeFormatRepository
    {
		public DateTimeFormatRepository(DbContext dbContext) : base(dbContext) { }
    }
}
