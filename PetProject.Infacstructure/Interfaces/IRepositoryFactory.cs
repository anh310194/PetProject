using PetProject.Domain.Common;
using PetProject.Domain.Entities;

namespace PetProject.Infacstructure.Interfaces;

public interface IRepositoryFactory
{
    IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : BaseEntity;

    IGenericRepository<Country> CountryRepository { get; }
    IUserRepository UserRepository { get; }
}
