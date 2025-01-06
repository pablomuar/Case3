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

                // Leer los valores de la respuesta JSON
                var prime = json["prime"];
                var odd = json["odd"];
                var square = json["square"]; // Leer la raíz cuadrada
                if (prime != null)
                {
                    raw_prime = prime.Value<bool>();
                }
                if (odd != null)
                {
                    raw_odd = odd.Value<bool>();
                }
                if (square != null)
                {
                    raw_squareRoot = square.Value<double>();
                }
            }

            // Formatear los valores para el resultado final
            var isPrime = "unknown";
            if (raw_prime != null && raw_prime.Value)
            {
                isPrime = "Yes";
            }
            else if (raw_prime != null && !raw_prime.Value)
            {
                isPrime = "No";
            }

            var isOdd = "unknown";
            if (raw_odd != null && raw_odd.Value)
            {
                isOdd = "Yes";
            }
            else if (raw_odd != null && !raw_odd.Value)
            {
                isOdd = "No";
            }

            var squareRoot = raw_squareRoot != null ? raw_squareRoot.Value.ToString("F2") : "unknown"; // Formatear a 2 decimales

            return (isPrime, isOdd, squareRoot);
        }

        [HttpPost]
        public ActionResult Index(string number)
        {
            var result = ExecuteOperation(number);
            ViewBag.IsPrime = result.isPrime;
            ViewBag.IsOdd = result.isOdd;
            ViewBag.SquareRoot = result.squareRoot; // Agregar la raíz cuadrada al ViewBag
            return View();
        }
    }
}
