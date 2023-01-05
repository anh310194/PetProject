using PetProject.Domain.Common;
using PetProject.Domain.Entities;

namespace PetProject.Domain.Interfaces;

public interface IRepositoryFactory
{
    IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : BaseEntity;

    ICountryRepository CountryRepository { get; }
    IUserRepository UserRepository { get; }
}
