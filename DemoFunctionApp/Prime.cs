using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DemoFunctionApp
{
    public static class Prime
    {
        [FunctionName("Prime")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var number = req.Query["number"];
            if (string.IsNullOrWhiteSpace(number))
            {
                return new BadRequestObjectResult("Please pass a number on the query string");
            }

            try
            {
                var prime = FindPrimeNumber(int.Parse(number));
                return new OkObjectResult($"{System.Net.Dns.GetHostName()}, {prime}");
            }
            catch
            {
                return new BadRequestObjectResult("Number parameter must be integer number");
            }
        }

        private static long FindPrimeNumber(int n)
        {
            var count = 0;
            long a = 2;
            while (count < n)
            {
                long b = 2;
                var prime = 1;
                while (b * b <= a)
                {
                    if (a % b == 0)
                    {
                        prime = 0;
                        break;
                    }
                    b++;
                }
                if (prime > 0)
                {
                    count++;
                }
                a++;
            }
            return (--a);
        }
    }
}
