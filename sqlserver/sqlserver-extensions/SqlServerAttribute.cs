using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Azure.WebJobs.Description;

namespace Serverless.Azure.WebJobs.Extensions.SqlServer
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    [Binding]
    public class SqlServerAttribute : Attribute
    {
        [ConnectionString(Default = "SqlServerConnectionString")]
        public string ConnectionString { get; set; }

        [AutoResolve(ResolutionPolicyType = typeof(SqlResolutionPolicy))]
        public string Query { get; set; }

        internal List<SqlParameter> SqlParameters { get; set; }
    }
}
