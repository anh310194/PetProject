using System.Data;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using PetProject.Domain;

namespace PetProject.Specification.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();
        IQueryable<TEntity> AsQuery<TEntity>() where TEntity : BaseEntity;
        IQueryable<TEntity> AsQuery<TEntity>(Expression<Func<TEntity, bool>> match) where TEntity : BaseEntity;
        TEntity Insert<TEntity>(TEntity entity, long userId) where TEntity : BaseEntity;
        void InsertRange<TEntity>(ICollection<TEntity> entities, long userId) where TEntity : BaseEntity;
        TEntity Update<TEntity>(TEntity entity, long userId) where TEntity : BaseEntity;
        void UpdateRange<TEntity>(ICollection<TEntity> entities, long userId) where TEntity : BaseEntity;
        void Delete<TEntity>(object id) where TEntity : BaseEntity;
        void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity;
        void DeleteRange<TEntity>(ICollection<TEntity> entities) where TEntity : BaseEntity;
        TEntity? Find<TEntity>(params object[] keyValues) where TEntity : BaseEntity;
        ValueTask<TEntity?> FindAsync<TEntity>(params object[] keyValues) where TEntity : BaseEntity;
        ValueTask<TEntity?> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : BaseEntity;
        Task<List<IEnumerable<IDictionary<string, object>>>> ExecCommandTextAsync(string query, CommandType commandType, params SqlParameter[] parameters);
        TResult ExecuteTransaction<TResult>(Func<TResult> func) where TResult : class;
        Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func) where TResult : class;
        void ExecuteTransaction(Action action);
        Task ExecuteTransactionAsync(Func<Task> action);
        Task<List<IEnumerable<IDictionary<string, object>>>> ExecStoreProcedureReturnMutipleAsync(string query, params SqlParameter[] parameters);
        Task<IEnumerable<IDictionary<string, object>>> ExecStoreProcedureAsync(string query, params SqlParameter[] parameters);
    }

}
