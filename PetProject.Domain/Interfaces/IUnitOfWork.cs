using System.Data;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;

namespace PetProject.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        Task<int> SaveChangesAsync();
        int SaveChanges();
        Task<List<IEnumerable<IDictionary<string, object>>>> ExecCommandTextAsync(string query, CommandType commandType, params SqlParameter[] parameters);
        TResult ExecuteTransaction<TResult>(Func<TResult> func) where TResult : class;
        Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func) where TResult : class;
        void ExecuteTransaction(Action action);
        Task ExecuteTransactionAsync(Func<Task> action);
        Task<List<IEnumerable<IDictionary<string, object>>>> ExecStoreProcedureReturnMutipleAsync(string query, params SqlParameter[] parameters);
        Task<IEnumerable<IDictionary<string, object>>?> ExecStoreProcedureAsync(string query, params SqlParameter[] parameters);
    }

}
