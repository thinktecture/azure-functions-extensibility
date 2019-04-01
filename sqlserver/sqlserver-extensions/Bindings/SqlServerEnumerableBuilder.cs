using Dapper;
using Microsoft.Azure.WebJobs;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Serverless.Azure.WebJobs.Extensions.SqlServer
{
    public class SqlServerEnumerableBuilder<T> : IAsyncConverter<SqlServerAttribute, IEnumerable<T>> where T : class
    {
        public async Task<IEnumerable<T>> ConvertAsync(SqlServerAttribute attribute, CancellationToken cancellationToken)
        {
            var data = new List<T>();

            using (var connection = new SqlConnection(attribute.ConnectionString))
            {
                var parameters = new DynamicParameters(new { });
                attribute.SqlParameters.ForEach(param => parameters.Add(param.ParameterName, param.Value));

                data = (await connection.QueryAsync<T>(new CommandDefinition(attribute.Query, parameters))).ToList();
            }

            return data;
        }
    }
}