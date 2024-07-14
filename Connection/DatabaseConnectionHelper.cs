// Helpers/DatabaseConnectionHelper.cs
using System;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using Microsoft.Extensions.Options;
using YourNamespace.Models;

namespace YourNamespace.Helpers
{
    public class DatabaseConnectionHelper
    {
        private readonly CompanyConnection _companyConnection;

        public DatabaseConnectionHelper(IOptions<CompanyConnection> config)
        {
            _companyConnection = config.Value;
        }

        public string ServerType => _companyConnection.ServerType;

        public IDbConnection GetConnection()
        {
            IDbConnection connection;
            if (_companyConnection.ServerType == "HANA")
            {
                connection = new OdbcConnection(GetHANAConnection(_companyConnection));
            }
            else
            {
                connection = new SqlConnection(GetSQLConnection(_companyConnection));
            }
            connection.Open();
            return connection;
        }

        private string GetHANAConnection(CompanyConnection config)
        {
            string strConnectionString = string.Empty;

            if (IntPtr.Size == 8)
            {
                strConnectionString = string.Concat(strConnectionString, "Driver={HDBODBC};");
            }
            else
            {
                strConnectionString = string.Concat(strConnectionString, "Driver={HDBODBC32};");
            }

            strConnectionString = string.Concat(strConnectionString, "ServerNode=", config.DatabaseServer, ";");
            strConnectionString = string.Concat(strConnectionString, "UID=", config.DbUsername, ";");
            strConnectionString = string.Concat(strConnectionString, "PWD=", config.DbPassword, ";");
            strConnectionString = string.Concat(strConnectionString, "CS=", config.CompanyDBName, ";");
            string hanaConn = new(strConnectionString);
            return hanaConn;
        }

        private string GetSQLConnection(CompanyConnection config)
        {
            return $"Server={config.DatabaseServer};Database={config.CompanyDBName};User Id={config.DbUsername};Password={config.DbPassword};";
        }
    }
}
