
using Microsoft.Data.SqlClient;
using System.Data;

namespace PetProject.Domain.Interfaces
{
    public interface IStoreProcedureRepository
    {
        Task<long[]?> GetRolesBysUserId(long userId);
        Task<List<IEnumerable<IDictionary<string, object>>>> ExecCommandTextAsync(string query, CommandType commandType, params SqlParameter[] parameters);
        Task<List<IEnumerable<IDictionary<string, object>>>> ExecStoreProcedureReturnMutipleAsync(string query, params SqlParameter[] parameters);
        Task<IEnumerable<IDictionary<string, object>>?> ExecStoreProcedureAsync(string query, params SqlParameter[] parameters);
    }
}
