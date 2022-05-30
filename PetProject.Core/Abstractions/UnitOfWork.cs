using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Transactions;
using PetProject.Core.Interfaces;
using PetProject.Core.Entities;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace PetProject.Core.Data
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private DbContext _dataContext { get; }
        private bool _disposed;
        //private Dictionary<string, dynamic> _repositories;
        private Dictionary<string, dynamic> _repositories;
        public UnitOfWork(DbContext dataContext)
        {
            _dataContext = dataContext;
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, dynamic>();
            }
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
                    _dataContext.Dispose();
                }
            }
            this._disposed = true;
        }
        #endregion

        public virtual int SaveChanges()
        {
            return _dataContext.SaveChanges();
        }

        private IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            string type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IRepository<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(Repository<>);
            var valueOject = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dataContext);        
            
            _repositories.Add(type, valueOject);
            return _repositories[type];
        }
        public virtual Task<int> SaveChangesAsync()
        {
            return _dataContext.SaveChangesAsync();
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
            var strategy = _dataContext.Database.CreateExecutionStrategy();
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
            var strategy = _dataContext.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using var transaction = new TransactionScope();
                action.Invoke();
                transaction.Complete();
            });
        }
        public virtual Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func) where TResult : class
        {
            var strategy = _dataContext.Database.CreateExecutionStrategy();
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
            var strategy = _dataContext.Database.CreateExecutionStrategy();
            return strategy.ExecuteAsync(async () =>
            {
                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await action.Invoke().ConfigureAwait(false);
                    transaction.Complete();
                }
            });
        }
        public virtual TEntity Insert<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            return GetRepository<TEntity>().Insert(entity);
        }
        public virtual void InsertRange<TEntity>(ICollection<TEntity> entities) where TEntity : BaseEntity
        {
            GetRepository<TEntity>().InsertRange(entities);
        }
        public virtual TEntity Update<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            return GetRepository<TEntity>().Update(entity);
        }
        public virtual void UpdateRange<TEntity>(ICollection<TEntity> entities) where TEntity : BaseEntity
        {
            GetRepository<TEntity>().UpdateRange(entities);
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
            var command = _dataContext.Database.GetDbConnection().CreateCommand();

            command.CommandText = query;
            command.CommandType = commandType;
            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }
            _dataContext.Database.OpenConnection();
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
    }
}

