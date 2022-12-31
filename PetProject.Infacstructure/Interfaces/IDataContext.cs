using Microsoft.EntityFrameworkCore;

namespace PetProject.Infacstructure.Interfaces
{
    public interface IDataContext
    {
        DbContext DataContext { get; }
    }
}
