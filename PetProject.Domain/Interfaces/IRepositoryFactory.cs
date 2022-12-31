using PetProject.Domain.Common;

namespace PetProject.Domain.Interfaces
{
    public interface IRepositoryFactory
    {
        IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : BaseEntity;
    }
}
