using PetProject.Domain.Common;

namespace PetProject.Infacstructure.Interfaces;

public interface IRepositoryFactory
{
    IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : BaseEntity;
}
