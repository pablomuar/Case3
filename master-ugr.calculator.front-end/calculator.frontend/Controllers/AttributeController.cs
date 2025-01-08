using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace calculator.frontend.Controllers
{
    public class AttributeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private string base_url =
            Environment.GetEnvironmentVariable("CALCULATOR_BACKEND_URL") ??
            "https://master-ugr-ci-backend-uat.azurewebsites.net";
        const string api = "api/Calculator";

        private (string isPrime, string isOdd, string squareRoot) ExecuteOperation(string number)
        {
            bool? raw_prime = null;
            bool? raw_odd = null;
            double? raw_squareRoot = null;

            var clientHandler = new HttpClientHandler();
            var client = new HttpClient(clientHandler);
            var url = $"{base_url}/api/Calculator/number_attribute?number={number}";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
            };

            using (var response = client.Send(request))
            {
                response.EnsureSuccessStatusCode();
                var body = response.Content.ReadAsStringAsync().Result;
                var json = JObject.Parse(body);
                var prime = json["prime"];
                var odd = json["odd"];
                var squareRoot = json["square"];

                if (prime != null)
                {
                    raw_prime = prime.Value<bool>();
                }
                if (odd != null)
                {
                    raw_odd = odd.Value<bool>();
                }
                if (squareRoot != null)
                {
                    raw_squareRoot = squareRoot.Value<double?>();
                }
            }

            var isPrime = raw_prime.HasValue ? (raw_prime.Value ? "Yes" : "No") : "unknown";
            var isOdd = raw_odd.HasValue ? (raw_odd.Value ? "Yes" : "No") : "unknown";
            var squareRootResult = raw_squareRoot.HasValue ? raw_squareRoot.Value.ToString() : "null";

            return (isPrime, isOdd, squareRootResult);
        }

        [HttpPost]
        public ActionResult Index(string number)
        {
            var result = ExecuteOperation(number);
            ViewBag.IsPrime = result.isPrime;
            ViewBag.IsOdd = result.isOdd;
            ViewBag.SquareRoot = result.squareRoot;
            return View();
        }
    }
}