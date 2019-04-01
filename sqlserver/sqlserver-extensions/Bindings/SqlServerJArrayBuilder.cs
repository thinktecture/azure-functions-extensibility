using Microsoft.Azure.WebJobs;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Serverless.Azure.WebJobs.Extensions.SqlServer
{
    internal class SqlServerJArrayBuilder : IAsyncConverter<SqlServerAttribute, JArray>
    {
        private readonly SqlServerEnumerableBuilder<dynamic> _builder;

        public SqlServerJArrayBuilder()
        {
            _builder = new SqlServerEnumerableBuilder<dynamic>();
        }

        public async Task<JArray> ConvertAsync(SqlServerAttribute input, CancellationToken cancellationToken)
        {
            var results = await _builder.ConvertAsync(input, cancellationToken).ConfigureAwait(false);
            return JArray.FromObject(results);
        }
    }
}