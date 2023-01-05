using PetProject.Domain.Common;
using PetProject.Domain.Interfaces;
using PetProject.Utilities.Exceptions;

namespace PetProject.Repositories.Common;

public abstract class RepositoryFactory : IRepositoryFactory
{
    private IServiceProvider _serviceProvider;
    public RepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICountryRepository CountryRepository
    {
        get
        {
            return GetRepository(typeof(ICountryRepository));
        }
    }

    public IUserRepository UserRepository
    {
        get
        {
            return GetRepository(typeof(IUserRepository));
        }
    }

    public IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : BaseEntity
    {
        return GetRepository(typeof(IGenericRepository<TEntity>));
    }
    private dynamic GetRepository(Type typeRepository)
    {
        object? repository = _serviceProvider.GetService(typeRepository);
        if (repository == null)
        {
            throw new PetProjectException($"The {typeRepository.Name} could not be found in Dependency Injection Container.");
        }

        return repository;
    }
}
