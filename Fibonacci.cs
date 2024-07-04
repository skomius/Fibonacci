using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Fibonacci
{
    public class Fibonacci
    {
        private readonly ILogger<Fibonacci> _logger;

        public Fibonacci(ILogger<Fibonacci> log)
        {
            _logger = log;
        }

        [FunctionName("Fibonacci")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "number" })]
        [OpenApiParameter(name: "number", In = ParameterLocation.Query, Required = true, Type = typeof(int), Description = "The **Number** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(int), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            int n = int.Parse(req.Query["number"]);

            int number = n;
            int[] Fib = new int[number + 1];
            Fib[0] = 0;
            Fib[1] = 1;
            for (int i = 2; i <= number; i++)
            {
                Fib[i] = Fib[i - 2] + Fib[i - 1];
            }

            _logger.LogInformation($"Calculated value is {Fib[number]}");

            return new OkObjectResult(Fib[number]);
        }
    }
}