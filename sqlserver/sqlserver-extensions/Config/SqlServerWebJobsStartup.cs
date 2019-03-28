using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Serverless.Azure.WebJobs.Extensions.SqlServer;

[assembly: WebJobsStartup(typeof(SqlServerWebJobsStartup))]

namespace Serverless.Azure.WebJobs.Extensions.SqlServer
{
    public class SqlServerWebJobsStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddSqlServer();
        }
    }
}