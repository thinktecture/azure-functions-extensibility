using System;
using Microsoft.Azure.WebJobs;

namespace Serverless.Azure.WebJobs.Extensions.SqlServer
{
    public static class SqlServerWebJobsBuilderExtensions
    {
        public static IWebJobsBuilder AddSqlServer(this IWebJobsBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.AddExtension<SqlServerExtensionConfigProvider>();

            return builder;
        }
    }
}
