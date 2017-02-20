using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Dapper;

namespace BookKeeping.Models
{
    public class BKRepository
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private T _Query<T>(Func<IDbConnection, T> callback)
        {
            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                return callback(conn);
            }
        }

        public IEnumerable<T> GetAllModels<T>(string model = null)
        {
            model = model ?? typeof(T).Name;
            var qs = $"SELECT * FROM {model}";
            return _Query(conn => conn.Query<T>(qs));
        }

        private string _MakeInsertQuery<T>(IEnumerable<string> exclude, string model)
        {
            var props = from p in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        where !exclude.Contains(p.Name)
                        select $"@{p.Name}";

            var propsQuery = string.Join(", ", props);
            return $"INSERT INTO {model} VALUES ({propsQuery}); SELECT CAST(SCOPE_IDENTITY() as int)";
        }

        public int AddMany<T>(IEnumerable<T> values, IEnumerable<string> exclude = null, string model = null)
        {
            model = model ?? typeof(T).Name;
            var query = _MakeInsertQuery<T>(exclude, model);
            return _Query(conn => conn.Execute(query, values));
        }
    }
}