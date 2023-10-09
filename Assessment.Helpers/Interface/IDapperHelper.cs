using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.Helpers.Interface
{
    public interface IDapperHelper
    {
        Task<T> QuerySingleAsync<T>(SqlConnection sqlConnection, string storedProcedureName, CommandType queryCommandType, object parameters = null);
        Task<IEnumerable<T>> QueryAsync<T>(SqlConnection sqlConnection, string storedProcedureName, CommandType queryCommandType, object parameters = null);
        Task<IEnumerable<T>> QueryFirstOrDefaultAsync<T>(SqlConnection sqlConnection, string storedProcedureName, CommandType queryCommandType, object parameters = null);
        Task<IEnumerable<T>> QuerySingleOrDefaultAsync<T>(SqlConnection sqlConnection, string storedProcedureName, CommandType queryCommandType, object parameters = null);
        Task<IEnumerable<T>> QueryFirstAsync<T>(SqlConnection sqlConnection, string storedProcedureName, CommandType queryCommandType, object parameters = null);
    }
}
