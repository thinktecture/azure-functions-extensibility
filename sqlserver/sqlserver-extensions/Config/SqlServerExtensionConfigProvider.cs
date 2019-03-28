using System;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Newtonsoft.Json.Linq;

namespace Serverless.Azure.WebJobs.Extensions.SqlServer
{
    [Extension("SqlServer")]
    public class SqlServerExtensionConfigProvider : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var bindingRule = context.AddBindingRule<SqlServerAttribute>();
            bindingRule.BindToInput<JArray>(typeof(SqlServerJArrayBuilder), this);
            bindingRule.BindToInput<IEnumerable<OpenType>>(typeof(SqlServerEnumerableBuilder<>), this);
            bindingRule.BindToInput<OpenType>(typeof(SqlServerBuilder<>), this);
        }
    }
}