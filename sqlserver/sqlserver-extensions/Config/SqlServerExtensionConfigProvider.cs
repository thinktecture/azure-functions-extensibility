using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Config;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Serverless.Azure.WebJobs.Extensions.SqlServer
{
    [Extension("SqlServer")]
    internal class SqlServerExtensionConfigProvider : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var bindingRule = context.AddBindingRule<SqlServerAttribute>();
            bindingRule.BindToInput<JArray>(typeof(SqlServerJArrayBuilder));
            bindingRule.BindToInput<IEnumerable<OpenType>>(typeof(SqlServerEnumerableBuilder<>));
            bindingRule.BindToInput<OpenType>(typeof(SqlServerBuilder<>));
        }
    }
}