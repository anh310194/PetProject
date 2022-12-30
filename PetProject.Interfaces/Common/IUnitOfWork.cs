using System.Data;
using Microsoft.Data.SqlClient;
using PetProject.Interfaces.Reponsitories;

namespace PetProject.Interfaces.Common
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository UserRepository { get; }
        public IDateTimeFormatRepository DateTimeFormatRepository { get; }
        public IFeatureRepository FeatureRepository { get; }
        public IRoleFeatureRepository RoleFeatureRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public ITimeZoneRepository TimeZoneRepository { get; }
        public IUserRoleRepository UserRoleRepository { get; }
        public ICountryRepository CountryRepository { get; }

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
