using Microsoft.EntityFrameworkCore;
using System.Transactions;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using PetProject.Interfaces.Common;
using PetProject.Interfaces.Reponsitories;
using PetProject.Utilities.Exceptions;

namespace PetProject.Repositories.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        protected DbContext _dbContext { get; private set; }
        private bool _disposed;        
        private IServiceProvider _serviceProvider { get; }
        private IDictionary<string, object> _repositoryCollection;

        public UnitOfWork(
            DbContext dbContext,
            IUserRepository userRepository,
            IDateTimeFormatRepository dateTimeFormatRepository,
            IFeatureRepository featureRepository,
            IRoleFeatureRepository roleFeatureRepository,
            IRoleRepository roleRepository,
            ITimeZoneRepository timeZoneRepository,
            IUserRoleRepository userRoleRepository,
            ICountryRepository countryRepository,
            IServiceProvider serviceProvider
        )
        {
            _dbContext = dbContext;
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

        #region Dispose        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }
        #endregion

        public virtual int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public virtual Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public virtual TResult ExecuteTransaction<TResult>(Func<TResult> func) where TResult : class
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            return strategy.Execute(() =>
            {
                using var transaction = new TransactionScope();
                var result = func.Invoke();
                transaction.Complete();
                return result;
            });
        }

        public virtual void ExecuteTransaction(Action action)
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using var transaction = new TransactionScope();
                action.Invoke();
                transaction.Complete();
            });
        }

        public virtual Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func) where TResult : class
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            return strategy.ExecuteAsync(async () =>
            {
                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    var result = await func.Invoke().ConfigureAwait(false);
                    transaction.Complete();
                    return result;
                }
            });
        }

        public virtual Task ExecuteTransactionAsync(Func<Task> action)
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            return strategy.ExecuteAsync(async () =>
            {
                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await action.Invoke().ConfigureAwait(false);
                    transaction.Complete();
                }
            });
        }

        public virtual async Task<List<IEnumerable<IDictionary<string, object>>>> ExecCommandTextAsync(string query, CommandType commandType, params SqlParameter[] parameters)
        {
            var list = new List<IEnumerable<IDictionary<string, object>>>();
            using (var reader = await ExecCommandText(query, commandType, parameters).ExecuteReaderAsync())
            {
                do
                {
                    list.Add(ReadToCollection(reader));
                }
                while (reader.NextResult());
            }
            return list;
        }
        private DbCommand ExecCommandText(string query, CommandType commandType, params SqlParameter[] parameters)
        {
            var command = _dbContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = commandType;

            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }

            _dbContext.Database.OpenConnection();
            return command;
        }
        private List<IDictionary<string, object>> ReadToCollection(DbDataReader reader)
        {
            List<IDictionary<string, object>> result = new List<IDictionary<string, object>>();
            while (reader.Read())
            {
                IDictionary<string, object> row = new Dictionary<string, object>();
                foreach (var column in reader.GetColumnSchema())
                {
                    row.Add(column.ColumnName, reader[column.ColumnName]);
                }
                result.Add(row);
            }
            return result;
        }

        public Task<List<IEnumerable<IDictionary<string, object>>>> ExecStoreProcedureReturnMutipleAsync(string query, params SqlParameter[] parameters)
        {
            return ExecCommandTextAsync(query, CommandType.StoredProcedure, parameters);
        }

        public Task<IEnumerable<IDictionary<string, object>>?> ExecStoreProcedureAsync(string query, params SqlParameter[] parameters)
        {
            return ExecCommandTextAsync(query, CommandType.StoredProcedure, parameters).ContinueWith<IEnumerable<IDictionary<string, object>>?>(c =>
            {
                return c.Result.FirstOrDefault(); ;
            });
        }

    }
}

