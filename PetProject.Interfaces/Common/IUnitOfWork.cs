using System.Data;
using Microsoft.Data.SqlClient;
using PetProject.Interfaces.Reponsitories;

namespace PetProject.Interfaces.Common
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository User { get; }
        public IDateTimeFormatRepository DateTimeFormat { get; }
        public IFeatureRepository Feature { get; }
        public IRoleFeatureRepository RoleFeature { get; }
        public IRoleRepository Role { get; }
        public ITimeZoneRepository TimeZone { get; }
        public IUserRoleRepository UserRole { get; }
        public ICountryRepository Country { get; }

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
