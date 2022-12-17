using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Transactions;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using PetProject.Domain;
using PetProject.Specification.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PetProject.Specification.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        protected DbContext _dbContext { get; private set; }

        private bool _disposed;
        private IDictionary<string, object> _repositories;
        public UnitOfWork(DbContext dbContext)
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }
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

        public virtual IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            string type = typeof(TEntity).Name;
            if (_repositories != null && _repositories.TryGetValue(type, out object? value))
            {
                return (IGenericRepository<TEntity>)value;
            }
            var typeEntity = typeof(TEntity);
            value = Activator.CreateInstance(typeof(GenericRepository<>).MakeGenericType(typeEntity), _dbContext);
           
            SetRepository<TEntity>(type, value);
            return (IGenericRepository<TEntity>)value;
        }

        public object? GetRepositoryByTypeName<TEntity>(string typeName) where TEntity : BaseEntity
        {
            if (_repositories != null && _repositories.TryGetValue(typeName, out object? value))
            {
                return value;
            }

            return null;
        }

        protected virtual void SetRepository<TEntity>(string typeName, object? value) where TEntity : BaseEntity
        {        
            if (value == null)
            {
                throw new SpecificationException("The system Can't not found Repository with " + typeof(TEntity).Name);
            }
            _repositories?.Add(typeName, value);
        }

        public virtual Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
        public virtual IQueryable<TEntity> AsQuery<TEntity>() where TEntity : BaseEntity
        {
            return GetRepository<TEntity>().Queryable();
        }
        public virtual IQueryable<TEntity> AsQuery<TEntity>(Expression<Func<TEntity, bool>> match) where TEntity : BaseEntity
        {
            return GetRepository<TEntity>().Queryable(match);
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
        public virtual TEntity Insert<TEntity>(TEntity entity, long userId) where TEntity : BaseEntity
        {
            return GetRepository<TEntity>().Insert(entity, userId);
        }
        public virtual void InsertRange<TEntity>(ICollection<TEntity> entities, long userId) where TEntity : BaseEntity
        {
            GetRepository<TEntity>().InsertRange(entities, userId);
        }
        public virtual TEntity Update<TEntity>(TEntity entity, long userId) where TEntity : BaseEntity
        {
            return GetRepository<TEntity>().Update(entity, userId);
        }
        public virtual void UpdateRange<TEntity>(ICollection<TEntity> entities, long userId) where TEntity : BaseEntity
        {
            foreach (var entity in entities)
            {
                entity.UpdatedTime = DateTime.UtcNow;
                entity.UpdatedBy = userId;
            }
            GetRepository<TEntity>().UpdateRange(entities, userId);
        }
        public virtual void Delete<TEntity>(object id) where TEntity : BaseEntity
        {
            GetRepository<TEntity>().Delete(id);
        }
        public virtual void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            GetRepository<TEntity>().Delete(entity);
        }
        public virtual void DeleteRange<TEntity>(ICollection<TEntity> entities) where TEntity : BaseEntity
        {
            GetRepository<TEntity>().DeleteRange(entities);
        }
        public virtual TEntity? Find<TEntity>(params object[] keyValues) where TEntity : BaseEntity
        {
            return GetRepository<TEntity>().Find(keyValues);
        }
        public virtual ValueTask<TEntity?> FindAsync<TEntity>(params object[] keyValues) where TEntity : BaseEntity
        {
            return GetRepository<TEntity>().FindAsync(keyValues);
        }
        public virtual ValueTask<TEntity?> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : BaseEntity
        {
            return GetRepository<TEntity>().FindAsync(keyValues, cancellationToken);
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
    }
}

