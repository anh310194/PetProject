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

        private GenericRepository<TEntity> GetOrCreateRepository<TEntity>() where TEntity : BaseEntity
        {
            var typeEntity = typeof(TEntity);
            if (_repositories != null && _repositories.TryGetValue(typeEntity.Name, out object? value))
            {
                return (GenericRepository<TEntity>)value;
            }

            value = CreateOrignalOrAssignedRepository<TEntity>();
            if (value == null)
            {
                throw new PetProjectException("The system Can't not found Repository with " + typeof(TEntity).Name);
            }
            _repositories?.Add(typeEntity.Name, value);
            return (GenericRepository<TEntity>)value;
        }
        private object? CreateOrignalOrAssignedRepository<TEntity>() where TEntity : BaseEntity
        {
            var typeGenericRepository = typeof(GenericRepository<TEntity>);
            var typeEntity = typeof(TEntity);
            var dbSet = _dbContext.Set<TEntity>();

            var typeAssignabledRepository = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .FirstOrDefault(type => 
                    typeGenericRepository.IsAssignableFrom(type)
                );
            if (typeAssignabledRepository == null)
            {
                return Activator.CreateInstance(typeGenericRepository.MakeGenericType(typeEntity), dbSet);
            }
            else
            {
                return Activator.CreateInstance(typeAssignabledRepository, dbSet);
            }
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
                var result = c.Result.FirstOrDefault();

                return result;
            });
        }

        public virtual IQueryable<TEntity> Queryable<TEntity>() where TEntity : BaseEntity
        {
            return GetOrCreateRepository<TEntity>().Queryable();
        }

        public virtual IQueryable<TEntity> Queryable<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        {
            return GetOrCreateRepository<TEntity>().Queryable(predicate);
        }
        
        public virtual TEntity Insert<TEntity>(TEntity entity, long userId) where TEntity : BaseEntity
        {
            return GetOrCreateRepository<TEntity>().Insert(entity, userId);
        }

        public virtual void InsertRange<TEntity>(ICollection<TEntity> entities, long userId) where TEntity : BaseEntity
        {
            GetOrCreateRepository<TEntity>().InsertRange(entities, userId);
        }

        public virtual TEntity Update<TEntity>(TEntity entity, long userId) where TEntity : BaseEntity
        {
            return GetOrCreateRepository<TEntity>().Update(entity, userId);
        }

        public virtual void UpdateRange<TEntity>(ICollection<TEntity> entities, long userId) where TEntity : BaseEntity
        {
            foreach (var entity in entities)
            {
                entity.UpdatedTime = DateTime.UtcNow;
                entity.UpdatedBy = userId;
            }
            GetOrCreateRepository<TEntity>().UpdateRange(entities, userId);
        }

        public virtual void Delete<TEntity>(object id) where TEntity : BaseEntity
        {
            GetOrCreateRepository<TEntity>().Delete(id);
        }

        public virtual void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            GetOrCreateRepository<TEntity>().Delete(entity);
        }

        public virtual void DeleteRange<TEntity>(ICollection<TEntity> entities) where TEntity : BaseEntity
        {
            GetOrCreateRepository<TEntity>().DeleteRange(entities);
        }

        public virtual TEntity? Find<TEntity>(params object[] keyValues) where TEntity : BaseEntity
        {
            return GetOrCreateRepository<TEntity>().Find(keyValues);
        }

        public virtual ValueTask<TEntity?> FindAsync<TEntity>(params object[] keyValues) where TEntity : BaseEntity
        {
            return GetOrCreateRepository<TEntity>().FindAsync(keyValues);
        }

        public virtual ValueTask<TEntity?> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : BaseEntity
        {
            return GetOrCreateRepository<TEntity>().FindAsync(keyValues, cancellationToken);
        }

    }
}

