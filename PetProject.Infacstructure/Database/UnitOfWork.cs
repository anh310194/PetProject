using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Transactions;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using PetProject.Domain;
using PetProject.Domain.Interfaces;
using PetProject.Utilities.Exceptions;
using PetProject.Infacstructure.Repositories;
using PetProject.Infacstructure.Interfaces;

namespace PetProject.Infacstructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        protected DbContext _dbContext { get; private set; }
        private bool _disposed;
        private IDictionary<string, object> _repositories;

        public UnitOfWork(IDataContext dataContext)
        {
            _repositories = new Dictionary<string, object>();
            _dbContext = dataContext.GetDbContext();
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

        private GenericRepository<TEntity> GetOrCreateGenericRepository<TEntity>() where TEntity : BaseEntity
        {
            var genericRepository = GetGenericRepository<TEntity>();            
            if(genericRepository == null)
            {
                genericRepository = CreateGenericRepository<TEntity>();  
                if (genericRepository == null)
                {
                    throw new PetProjectException("The system Can't not found Repository with " + typeof(TEntity).Name);
                }              
                _repositories?.Add(typeof(TEntity).Name, genericRepository);
            }

            return (GenericRepository<TEntity>)genericRepository;
        }
        private object? GetGenericRepository<TEntity>() where TEntity : BaseEntity
        {
            _repositories.TryGetValue(typeof(TEntity).Name, out object? value);
            return value;
        }
        private object? CreateGenericRepository<TEntity>() where TEntity : BaseEntity
        {
            var assignedRepository = CreateAssignedGenericRepository<TEntity>();
            if (assignedRepository != null)
            {
                return assignedRepository;
            }
            
            return Activator.CreateInstance(TypeOfGenericRepository<TEntity>(), _dbContext.Set<TEntity>());            
        }
        private object? CreateAssignedGenericRepository<TEntity>() where TEntity : BaseEntity
        {
            var typeAssignabledRepository = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .FirstOrDefault(type => typeof(GenericRepository<TEntity>).IsAssignableFrom(type));
            if (typeAssignabledRepository == null)
            {
                return null;
            }

            return Activator.CreateInstance(typeAssignabledRepository, _dbContext.Set<TEntity>());
        }
        private Type TypeOfGenericRepository<TEntity>() where TEntity : BaseEntity
        {
            return typeof(GenericRepository<TEntity>).MakeGenericType(typeof(TEntity));
        }

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
                return c.Result.FirstOrDefault();;
            });
        }

        public virtual IQueryable<TEntity> Queryable<TEntity>() where TEntity : BaseEntity
        {
            return GetOrCreateGenericRepository<TEntity>().Queryable();
        }

        public virtual IQueryable<TEntity> Queryable<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            return GetOrCreateGenericRepository<TEntity>().Queryable(predicate);
        }
        
        public virtual TEntity Insert<TEntity>(TEntity entity, long userId) where TEntity : BaseEntity
        {
            return GetOrCreateGenericRepository<TEntity>().Insert(entity, userId);
        }

        public virtual void InsertRange<TEntity>(ICollection<TEntity> entities, long userId) where TEntity : BaseEntity
        {
            GetOrCreateGenericRepository<TEntity>().InsertRange(entities, userId);
        }

        public virtual TEntity Update<TEntity>(TEntity entity, long userId) where TEntity : BaseEntity
        {
            return GetOrCreateGenericRepository<TEntity>().Update(entity, userId);
        }

        public virtual void UpdateRange<TEntity>(ICollection<TEntity> entities, long userId) where TEntity : BaseEntity
        {
            GetOrCreateGenericRepository<TEntity>().UpdateRange(entities, userId);
        }

        public virtual void Delete<TEntity>(object id) where TEntity : BaseEntity
        {
            GetOrCreateGenericRepository<TEntity>().Delete(id);
        }

        public virtual void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            GetOrCreateGenericRepository<TEntity>().Delete(entity);
        }

        public virtual void DeleteRange<TEntity>(ICollection<TEntity> entities) where TEntity : BaseEntity
        {
            GetOrCreateGenericRepository<TEntity>().DeleteRange(entities);
        }

        public virtual TEntity? Find<TEntity>(params object[] keyValues) where TEntity : BaseEntity
        {
            return GetOrCreateGenericRepository<TEntity>().Find(keyValues);
        }

        public virtual ValueTask<TEntity?> FindAsync<TEntity>(params object[] keyValues) where TEntity : BaseEntity
        {
            return GetOrCreateGenericRepository<TEntity>().FindAsync(keyValues);
        }

        public virtual ValueTask<TEntity?> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : BaseEntity
        {
            return GetOrCreateGenericRepository<TEntity>().FindAsync(keyValues, cancellationToken);
        }

    }
}

