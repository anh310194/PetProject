using Microsoft.EntityFrameworkCore;
using System.Transactions;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using PetProject.Interfaces.Common;
using PetProject.Interfaces.Reponsitories;

namespace PetProject.Repositories.Common
{
    public class UnitOfWork :RepositoryFactory, IUnitOfWork
    {
        protected DbContext _dbContext { get; private set; }
        private bool _disposed;

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
        ) : base(           
                userRepository,
                dateTimeFormatRepository,
                featureRepository,
                roleFeatureRepository,
                roleRepository,
                timeZoneRepository,
                userRoleRepository,
                countryRepository,
                serviceProvider
             )
        {
            _dbContext = dbContext;           
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

