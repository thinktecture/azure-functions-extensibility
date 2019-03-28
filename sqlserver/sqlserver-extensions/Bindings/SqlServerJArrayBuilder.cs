using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json.Linq;

namespace Serverless.Azure.WebJobs.Extensions.SqlServer
{
    internal class SqlServerJArrayBuilder : IAsyncConverter<SqlServerAttribute, JArray>
    {
        private SqlServerEnumerableBuilder<dynamic> _builder;

        public SqlServerJArrayBuilder(SqlServerExtensionConfigProvider configProvider)
        {
            _builder = new SqlServerEnumerableBuilder<dynamic>(configProvider);
        }

        public async Task<JArray> ConvertAsync(SqlServerAttribute attribute, CancellationToken cancellationToken)
        {
            IEnumerable<dynamic> results = (await _builder.ConvertAsync(attribute, cancellationToken));
            
            return JArray.FromObject(results);
        }
    }
}