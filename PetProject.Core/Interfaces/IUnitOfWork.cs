using System.Linq.Expressions;
using PetProject.Core.Entities;

namespace PetProject.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();
        IQueryable<TEntity> AsQuery<TEntity>() where TEntity : BaseEntity;
        IQueryable<TEntity> AsQuery<TEntity>(Expression<Func<TEntity, bool>> match) where TEntity : BaseEntity;
        TEntity Insert<TEntity>(TEntity entity) where TEntity : BaseEntity;
        void InsertRange<TEntity>(ICollection<TEntity> entities) where TEntity : BaseEntity;
        TEntity Update<TEntity>(TEntity entity) where TEntity : BaseEntity;
        void UpdateRange<TEntity>(ICollection<TEntity> entities) where TEntity : BaseEntity;
        void Delete<TEntity>(object id) where TEntity : BaseEntity;
        void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity;
        void DeleteRange<TEntity>(ICollection<TEntity> entities) where TEntity : BaseEntity;
        TEntity? Find<TEntity>(params object[] keyValues) where TEntity : BaseEntity;
        ValueTask<TEntity?> FindAsync<TEntity>(params object[] keyValues) where TEntity : BaseEntity;
        ValueTask<TEntity?> FindAsync<TEntity>(object[] keyValues, CancellationToken cancellationToken) where TEntity : BaseEntity;

        TResult ExecuteTransaction<TResult>(Func<TResult> func) where TResult : class;
        Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func) where TResult : class;
        void ExecuteTransaction(Action action);
        Task ExecuteTransactionAsync(Func<Task> action);
    }

}
