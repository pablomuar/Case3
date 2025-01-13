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

        private async Task<(bool isError, string isPrime, string isOdd, string squareRoot, string errorMessage)> ExecuteOperationAsync(string number)
        {
            try
            {
                using var client = new HttpClient();
                var url = $"{_baseUrl}/api/Calculator/number_attribute?number={number}";
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return (true, "Error", "Error", "Error", errorMessage);
                }

                var body = await response.Content.ReadAsStringAsync();
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
    }
}
