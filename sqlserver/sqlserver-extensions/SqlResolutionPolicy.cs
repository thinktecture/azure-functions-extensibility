using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Bindings.Path;

namespace Serverless.Azure.WebJobs.Extensions.SqlServer
{
    // Let's protect ourselves against SQL injection...
    internal class SqlResolutionPolicy : IResolutionPolicy
    {
        public string TemplateBind(PropertyInfo propInfo, Attribute resolvedAttribute, BindingTemplate bindingTemplate, IReadOnlyDictionary<string, object> bindingData)
        {
            if (bindingTemplate == null)
            {
                throw new ArgumentNullException(nameof(bindingTemplate));
            }

            if (bindingData == null)
            {
                throw new ArgumentNullException(nameof(bindingData));
            }

            var sqlServerAttribute = resolvedAttribute as SqlServerAttribute;

            if (sqlServerAttribute == null)
            {
                throw new NotSupportedException($"This policy is only supported for {nameof(SqlServerAttribute)}.");
            }
          
            var parameters = new List<SqlParameter>();
            var replacements = new Dictionary<string, object>();

            foreach (var token in bindingTemplate.ParameterNames.Distinct())
            {
                string sqlToken = $"@{token}";
                parameters.Add(new SqlParameter(sqlToken, bindingData[token]));
                replacements.Add(token, sqlToken);
            }

            sqlServerAttribute.SqlParameters = parameters;
            var replacement = bindingTemplate.Bind(new ReadOnlyDictionary<string, object>(replacements));
            
            return replacement;
        }
    }
}