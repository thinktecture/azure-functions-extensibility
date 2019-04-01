using Dapper;
using Microsoft.Azure.WebJobs;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Serverless.Azure.WebJobs.Extensions.SqlServer
{
    public class SqlServerBuilder<T> : IAsyncConverter<SqlServerAttribute, T> where T : class
    {
        public async Task<T> ConvertAsync(SqlServerAttribute attribute, CancellationToken cancellationToken)
        {
            var data = default(T);

            using (var connection = new SqlConnection(attribute.ConnectionString))
            {
                var parameters = new DynamicParameters(new { });
                attribute.SqlParameters.ForEach(param => parameters.Add(param.ParameterName, param.Value));

                data = await connection.QuerySingleAsync<T>(new CommandDefinition(attribute.Query, parameters));
            }

            return data;
        }
    }
}