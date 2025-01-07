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
        const string base_url = "https://master-ugr-ci-backend-uat.azurewebsites.net";
        private string ExecuteOperation(string number)
        {
            bool? result = null;
            var clientHandler = new HttpClientHandler();
            var client = new HttpClient(clientHandler);
            var url = $"{base_url}/api/Calculator/is_prime?number={number}";
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
                var result_json = json["result"];
                if (result_json != null)
                {
                    result = result_json.Value<bool>();
                }
            }
            var yesOrNo = "unknown";
            if (result != null && result.Value)
            {
                yesOrNo = "Yes";
            }
            else if (result != null && !result.Value)
            {
                yesOrNo = "No";
            }
            return yesOrNo;
        }
        [HttpPost]
        public ActionResult Index(string number)
        {
            ViewBag.IsPrime = ExecuteOperation(number);
            return View();
        }
    }
}