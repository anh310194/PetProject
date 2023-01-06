using Microsoft.Data.SqlClient;
using System.Data;

namespace PetProject.Interfaces.Repositories;

public interface IStoreProcedureRepository
{
    Task<ICollection<long>> GetRolesBysUserId(long userId);
    Task<ICollection<IEnumerable<IDictionary<string, object>>>> ExecCommandTextAsync(string query, CommandType commandType, params SqlParameter[] parameters);
    Task<ICollection<IEnumerable<IDictionary<string, object>>>> ExecStoreProcedureReturnMutipleAsync(string query, params SqlParameter[] parameters);
    Task<IEnumerable<IDictionary<string, object>>?> ExecStoreProcedureAsync(string query, params SqlParameter[] parameters);
}
