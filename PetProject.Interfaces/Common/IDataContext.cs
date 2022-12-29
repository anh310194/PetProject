using Microsoft.EntityFrameworkCore;

namespace PetProject.Interfaces.Common
{
    public interface IDataContext{
        DbContext GetDbContext();
    }
}