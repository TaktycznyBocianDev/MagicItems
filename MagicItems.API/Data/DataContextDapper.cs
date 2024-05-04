using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace MagicItems.API.Data
{
    class DataContextDapper
    {
        private readonly IConfiguration _configuration;
        public DataContextDapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T LoadDataSingle<T>(string sql, object parameters = null)
        {
            IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return dbConnection.QuerySingle<T>(sql, parameters);
        }

        public IEnumerable<T> LoadData<T>(string sql, object parameters = null)
        {
            IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return dbConnection.Query<T>(sql, parameters);
        }

        public bool ExecuteSql(string sql, object parameters = null)
        {
            IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql, parameters) > 0;
        }

        public int ExecuteSqlWithRowCount(string sql, object parameters = null)
        {
            IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql, parameters);
        }
    }
}