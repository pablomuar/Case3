using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace calculator.frontend.Controllers
{
    public class AttributeController : Controller
    {
        private readonly string _baseUrl;

        public AttributeController()
        {
            _baseUrl = Environment.GetEnvironmentVariable("CALCULATOR_BACKEND_URL") ?? "http://localhost:5226";
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string number)
        {
            // Validar entrada
            var validationError = ValidateInput(number, out int parsedNumber);
            if (validationError != null)
            {
                ViewBag.ErrorMessage = validationError;
                return View();
            }

            // Llamada al backend
            var result = await ExecuteOperationAsync(number);

            if (result["isError"] == "true")
            {
                ViewBag.ErrorMessage = result["errorMessage"];
                return View();
            }

            // Mostrar resultados
            ViewBag.IsPrime = result["isPrime"];
            ViewBag.IsOdd = result["isOdd"];
            ViewBag.SquareRoot = result["squareRoot"];

            return View();
        }

        private string ValidateInput(string number, out int parsedNumber)
        {
            parsedNumber = 0;

            if (string.IsNullOrWhiteSpace(number) || !int.TryParse(number, out parsedNumber))
            {
                return "Invalid input. Please enter a valid integer.";
            }

            if (parsedNumber < 0)
            {
                return "La raiz cuadrada de un numero negativo no se puede calcular.";
            }

            return null;
        }

        private async Task<Dictionary<string, string>> ExecuteOperationAsync(string number)
        {
            var result = new Dictionary<string, string>
            {
                { "isError", "false" },
                { "isPrime", "unknown" },
                { "isOdd", "unknown" },
                { "squareRoot", "unknown" },
                { "errorMessage", string.Empty }
            };

            try
            {
                using var client = new HttpClient();
                var url = $"{_baseUrl}/api/Calculator/number_attribute?number={number}";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    result["isError"] = "true";
                    result["errorMessage"] = await response.Content.ReadAsStringAsync();
                    return result;
                }

                var body = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(body);

                result["isPrime"] = json["prime"]?.Value<bool>() == true ? "Yes" : "No";
                result["isOdd"] = json["odd"]?.Value<bool>() == true ? "Yes" : "No";
                result["squareRoot"] = json["square"]?.Value<double?>()?.ToString() ?? "null";
            }
            catch (Exception ex)
            {
                result["isError"] = "true";
                result["errorMessage"] = $"Unexpected error: {ex.Message}";
            }

            return result;
        }
    }
}
