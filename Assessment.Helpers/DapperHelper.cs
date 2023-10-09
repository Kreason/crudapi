using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assessment.Helpers.Interface;
using Dapper;

namespace Assessment.Helpers
{
    // this class helps build dapper requests used to connect to the database
    public class DapperHelper : IDapperHelper
    {
        public async Task<IEnumerable<T>> QueryAsync<T>(SqlConnection sqlConnection, string storedProcedureName, CommandType queryCommandType, object parameters = null)
        {
            IEnumerable<T> result;
            sqlConnection.Open();
            SqlCommand command = sqlConnection.CreateCommand();
            SqlTransaction transaction;
            transaction = sqlConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            command.Connection = sqlConnection;
            command.Transaction = transaction;

            try
            {
                result = await sqlConnection.QueryAsync<T>(storedProcedureName,
                                                             param: parameters,
                                                             transaction: transaction,
                                                             commandTimeout: 120,
                                                             commandType: queryCommandType);
                transaction.Commit();

            }
            finally
            {
                transaction.Dispose();
            }

            return result;
        }

        public async Task<IEnumerable<T>> QueryFirstAsync<T>(SqlConnection sqlConnection, string storedProcedureName, CommandType queryCommandType, object parameters = null)
        {
            IEnumerable<T> result;
            sqlConnection.Open();
            SqlCommand command = sqlConnection.CreateCommand();
            SqlTransaction transaction;
            transaction = sqlConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            command.Connection = sqlConnection;
            command.Transaction = transaction;

            try
            {
                result = await sqlConnection.QueryAsync<T>(storedProcedureName,
                                                             param: parameters,
                                                             transaction: transaction,
                                                             commandTimeout: 120,
                                                             commandType: queryCommandType);
                transaction.Commit();

            }
            finally
            {
                transaction.Dispose();
            }

            return result;
        }

        public async Task<IEnumerable<T>> QueryFirstOrDefaultAsync<T>(SqlConnection sqlConnection, string storedProcedureName, CommandType queryCommandType, object parameters = null)
        {
            IEnumerable<T> result;
            sqlConnection.Open();
            SqlCommand command = sqlConnection.CreateCommand();
            SqlTransaction transaction;
            transaction = sqlConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            command.Connection = sqlConnection;
            command.Transaction = transaction;

            try
            {
                result = await sqlConnection.QueryAsync<T>(storedProcedureName,
                                                             param: parameters,
                                                             transaction: transaction,
                                                             commandTimeout: 120,
                                                             commandType: queryCommandType);
                transaction.Commit();

            }
            finally
            {
                transaction.Dispose();
            }

            return result;
        }

        public async Task<T> QuerySingleAsync<T>(SqlConnection sqlConnection, string storedProcedureName, CommandType queryCommandType, object parameters = null)
        {
            T result;
            sqlConnection.Open();
            SqlCommand command = sqlConnection.CreateCommand();
            SqlTransaction transaction;
            transaction = sqlConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            command.Connection = sqlConnection;
            command.Transaction = transaction;

            try
            {
                result = await sqlConnection.QueryFirstOrDefaultAsync<T>(storedProcedureName, param: parameters, transaction: transaction, commandTimeout: 120, commandType: queryCommandType);
                transaction.Commit();

            }
            finally
            {
                transaction.Dispose();
            }

            return result;
        }

        public async Task<IEnumerable<T>> QuerySingleOrDefaultAsync<T>(SqlConnection sqlConnection, string storedProcedureName, CommandType queryCommandType, object parameters = null)
        {
            IEnumerable<T> result;
            sqlConnection.Open();
            SqlCommand command = sqlConnection.CreateCommand();
            SqlTransaction transaction;
            transaction = sqlConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
            command.Connection = sqlConnection;
            command.Transaction = transaction;

            try
            {
                result = await sqlConnection.QueryAsync<T>(storedProcedureName,
                                                             param: parameters,
                                                             transaction: transaction,
                                                             commandTimeout: 120,
                                                             commandType: queryCommandType);
                transaction.Commit();

            }
            finally
            {
                transaction.Dispose();
            }

            return result;
        }
    }
}
