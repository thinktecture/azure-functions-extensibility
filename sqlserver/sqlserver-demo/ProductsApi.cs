using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Serverless.Azure.WebJobs.Extensions.SqlServer;
using Serverless.Models.Entities;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Serverless
{
    public class ProductsApi
    {
        [FunctionName("GetProducts")]
        public IActionResult GetProducts(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "products")]
            HttpRequest req,
            
            [SqlServer(Query = "SELECT ProductID, Name FROM SalesLT.Product p ORDER BY p.Name")]
            IEnumerable<Product> products,
            
            ILogger log)
        {
            log.LogInformation("C# GetProducts trigger function processed a request.");

            return new OkObjectResult(products.Select(p => new { p.ProductId, p.Name }));
        }

        [FunctionName("GetProducts2")]
        public IActionResult GetProductsJArray(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "products2")]
            HttpRequest req,
            
            [SqlServer(Query = "SELECT * FROM SalesLT.Product")]
            JArray products,
            
            ILogger log)
        {
            log.LogInformation("C# GetProducts2 trigger function processed a request.");

            return new OkObjectResult(products);
        }

        [FunctionName("GetProductDetails")]
        public IActionResult GetProductDetails(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = "products/{id}")]
            HttpRequest req,
            
            [SqlServer(Query = "SELECT * FROM SalesLT.Product as p WHERE p.ProductID={id}")]
            Product product,
            
            ILogger log)
        {
            log.LogInformation("C# GetProduct trigger function processed a request.");

            return new OkObjectResult(product);
        }
    }
}
