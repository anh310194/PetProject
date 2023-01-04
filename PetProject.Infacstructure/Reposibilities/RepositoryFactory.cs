using PetProject.Domain.Common;
using PetProject.Domain.Entities;
using PetProject.Infacstructure.Interfaces;
using PetProject.Utilities.Exceptions;

namespace PetProject.Repositories.Common;

public abstract class RepositoryFactory : IRepositoryFactory
{
    private IUserRepository _userRepository;
    private IGenericRepository<Country> _countryRepository;
    private IServiceProvider _serviceProvider;
    public RepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IGenericRepository<Country> CountryRepository
    {
        get
        {
            if (_countryRepository == null)
            {
                _countryRepository = CountryRepository;
            }

            return _countryRepository;
        }
    }

    public IUserRepository UserRepository
    {
        get
        {
            if (_userRepository == null)
            {
                _userRepository = GetRepository(typeof(IGenericRepository<User>));
            }

            return _userRepository;
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
