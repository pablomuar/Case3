using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace calculator.frontend.Controllers
{
    public class AttributeController : Controller
    {
        private readonly string _baseUrl =
            Environment.GetEnvironmentVariable("CALCULATOR_BACKEND_URL") ??
            "https://master-ugr-ci-backend-uat.azurewebsites.net";

        public IActionResult Index()
        {
            return View();
        }

        private (bool isError, string isPrime, string isOdd, string squareRoot, string errorMessage) ExecuteOperation(string number)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"{_baseUrl}/api/Calculator/number_attribute?number={number}";
                var response = client.GetAsync(url).Result;

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = response.Content.ReadAsStringAsync().Result;
                    return (true, "Error", "Error", "Error", errorMessage);
                }

                var body = response.Content.ReadAsStringAsync().Result;
                var json = JObject.Parse(body);

                var isPrime = json["prime"]?.Value<bool>() == true ? "Yes" : "No";
                var isOdd = json["odd"]?.Value<bool>() == true ? "Yes" : "No";
                var squareRoot = json["square"]?.Value<double?>()?.ToString() ?? "null";

                return (false, isPrime, isOdd, squareRoot, string.Empty);
            }
            catch (Exception ex)
            {
                return (true, "unknown", "unknown", "unknown", $"Unexpected error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Index(string number)
        {
            // Validación de entrada
            if (string.IsNullOrWhiteSpace(number) || !int.TryParse(number, out int parsedNumber))
            {
                ViewBag.ErrorMessage = "Invalid input. Please enter a valid integer.";
                return View();
            }

            if (parsedNumber < 0)
            {
                ViewBag.ErrorMessage = "The square root of a negative number cannot be calculated.";
                return View();
            }

            // Llamar al backend
            var result = ExecuteOperation(number);

            if (result.isError)
            {
                ViewBag.ErrorMessage = result.errorMessage;
                return View();
            }

            // Mostrar resultados
            ViewBag.IsPrime = result.isPrime;
            ViewBag.IsOdd = result.isOdd;
            ViewBag.SquareRoot = result.squareRoot;

            return View();
        }
    }
}
