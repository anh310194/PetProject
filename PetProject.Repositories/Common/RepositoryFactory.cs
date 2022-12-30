using PetProject.Interfaces.Common;
using PetProject.Interfaces.Reponsitories;
using PetProject.Utilities.Exceptions;

namespace PetProject.Repositories.Common
{
    public abstract class RepositoryFactory: IRepositoryFactory
    {
        private IDictionary<string, object> _repositoryCollection;
        private IServiceProvider _serviceProvider { get; }
        public RepositoryFactory(
            IUserRepository userRepository,
            IDateTimeFormatRepository dateTimeFormatRepository,
            IFeatureRepository featureRepository,
            IRoleFeatureRepository roleFeatureRepository,
            IRoleRepository roleRepository,
            ITimeZoneRepository timeZoneRepository,
            IUserRoleRepository userRoleRepository,
            ICountryRepository countryRepository,
            IServiceProvider serviceProvider)
        {
             _countryRepository = countryRepository;
            _dateTimeFormatRepository = dateTimeFormatRepository;
            _featureRepository = featureRepository;
            _roleFeatureRepository = roleFeatureRepository;
            _roleRepository = roleRepository;
            _timeZoneRepository = timeZoneRepository;
            _userRoleRepository= userRoleRepository;
            _userRepository = userRepository;
            _serviceProvider = serviceProvider;
            _repositoryCollection = new Dictionary<string, object>();
        }
        
        private ICountryRepository _countryRepository { get; }
        public ICountryRepository CountryRepository { get{ return /*GetRepository(this.GetType())*/_countryRepository; } 
        }

        private IUserRepository _userRepository { get; }
        public IUserRepository UserRepository { get{ return _userRepository;} }

        private IDateTimeFormatRepository _dateTimeFormatRepository { get; }
        public IDateTimeFormatRepository DateTimeFormatRepository { get{ return _dateTimeFormatRepository;} }

        private IFeatureRepository _featureRepository { get; }
        public IFeatureRepository FeatureRepository { get{ return _featureRepository;} }

        private IRoleFeatureRepository _roleFeatureRepository { get; }
        public IRoleFeatureRepository RoleFeatureRepository { get{ return _roleFeatureRepository;} }

        private IRoleRepository _roleRepository { get; }
        public IRoleRepository RoleRepository { get{ return _roleRepository;} }

        private ITimeZoneRepository _timeZoneRepository { get; }
        public ITimeZoneRepository TimeZoneRepository { get{ return _timeZoneRepository;} }

        private IUserRoleRepository _userRoleRepository { get; }        
        public IUserRoleRepository UserRoleRepository { get{ return _userRoleRepository;} }

        private dynamic GetRepository(Type typeRepository)
        {
            object? repository;
            if(_repositoryCollection.TryGetValue(typeRepository.Name, out repository))
            {
                return repository;
            }

            repository = CreateRepository(typeRepository);
            return repository;
        }

        private dynamic CreateRepository(Type typeRepository)
        {
            object? repository = _serviceProvider.GetService(typeRepository);
            if(repository == null){
                throw new PetProjectException($"The {typeRepository.Name} could not be found in Dependency Injection Container.");
            }

            _repositoryCollection.Add(typeRepository.Name, repository);
            return repository;
        }
        }
}
