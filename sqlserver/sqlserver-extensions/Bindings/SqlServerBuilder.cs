using Dapper;
using Microsoft.Azure.WebJobs;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Serverless.Azure.WebJobs.Extensions.SqlServer
{
    internal class SqlServerBuilder<T> : IAsyncConverter<SqlServerAttribute, T> where T : class
    {
        public async Task<T> ConvertAsync(SqlServerAttribute input, CancellationToken cancellationToken)
        {
            var data = default(T);

            using (var connection = new SqlConnection(input.ConnectionString))
            {
                var parameters = new DynamicParameters(new { });
                input.SqlParameters.ForEach(param => parameters.Add(param.ParameterName, param.Value));

                data = await connection.QuerySingleAsync<T>(new CommandDefinition(input.Query, parameters)).ConfigureAwait(false);
            }

            return data;
        }
    }
}