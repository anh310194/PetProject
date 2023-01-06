using System.Data.Common;
using System.Data;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using PetProject.Infacstructure.Context;
using PetProject.Interfaces.Repositories;

namespace PetProject.Infacstructure.Reposibilities
{
    public class StoreProcedureRepository : IStoreProcedureRepository
    {
        private readonly PetProjectContext _dbContext;
        public StoreProcedureRepository(PetProjectContext context)
        {
            _dbContext = context;
        }
        public async Task<ICollection<long>> GetRolesBysUserId(long userId)
        {
            List<long> result;
            var paremeter = new SqlParameter("@UserId", System.Data.SqlDbType.BigInt) { Value = userId };
            var roleIds = await ExecStoreProcedureAsync("GetFeaturesByUser", paremeter);
            if (roleIds != null)
            {
                result = roleIds.Select(s => (long)s.FirstOrDefault().Value).ToList();
            }
            else
            {
                result = new List<long>();
            }
            return result;
        }

        public virtual async Task<ICollection<IEnumerable<IDictionary<string, object>>>> ExecCommandTextAsync(string query, CommandType commandType, params SqlParameter[] parameters)
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
        private ICollection<IDictionary<string, object>> ReadToCollection(DbDataReader reader)
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

        public Task<ICollection<IEnumerable<IDictionary<string, object>>>> ExecStoreProcedureReturnMutipleAsync(string query, params SqlParameter[] parameters)
        {
            return ExecCommandTextAsync(query, CommandType.StoredProcedure, parameters);
        }

        public Task<IEnumerable<IDictionary<string, object>>?> ExecStoreProcedureAsync(string query, params SqlParameter[] parameters)
        {
            return ExecCommandTextAsync(query, CommandType.StoredProcedure, parameters).ContinueWith(c =>
            {
                return c.Result.FirstOrDefault(); ;
            });
        }

    }
}
