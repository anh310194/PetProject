using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Transactions;
using PetProject.Core.Interfaces;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace PetProject.Core.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly IDbContext _dbContext;

        public DbContext Context { get { return _dbContext.Instance; } }

        private bool _disposed;
        //private Dictionary<string, dynamic> _repositories;
        private IDictionary<string, object?>? _repositories;
        public UnitOfWork(IDbContext dbContext)
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
            if (!this._disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this._disposed = true;
        }
        #endregion

        public virtual int SaveChanges()
        {
            return Context.SaveChanges();
        }
        public virtual IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            string type = typeof(TEntity).Name;
            if (_repositories != null && _repositories.TryGetValue(type, out object? value))
            {
                return (IRepository<TEntity>)value;
            }

            value = Activator.CreateInstance(typeof(Repository<>).MakeGenericType(typeof(TEntity)), _dbContext);
            SetRepository(type, value);
            return (IRepository<TEntity>)value;
        }

        public virtual void SetRepository(string typeName, object? value)
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object?>();
            }

            _repositories?.Add(typeName, value);
        }

        public virtual Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
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
            var strategy = Context.Database.CreateExecutionStrategy();
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
            var strategy = Context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using var transaction = new TransactionScope();
                action.Invoke();
                transaction.Complete();
            });
        }
        public virtual Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func) where TResult : class
        {
            var strategy = Context.Database.CreateExecutionStrategy();
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
            var strategy = Context.Database.CreateExecutionStrategy();
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
            var command = Context.Database.GetDbConnection().CreateCommand();
            command.CommandText = query;
            command.CommandType = commandType;

            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }

            Context.Database.OpenConnection();
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
        public Task<IEnumerable<IDictionary<string, object>>> ExecStoreProcedureAsync(string query, params SqlParameter[] parameters)
        {
            return ExecCommandTextAsync(query, CommandType.StoredProcedure, parameters).ContinueWith<IEnumerable<IDictionary<string, object>>>(c =>
            {
                return c.Result.FirstOrDefault();
            });
        }
    }
}

