using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using TechTalk.SpecFlow;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace calculator.lib.test.steps
{
    [Binding]
    public class SquareNumberSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public SquareNumberSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"a number to calculate square root (.*)")]
        public void GivenANumberToCalculateSquareRoot(int number)
        {
            _scenarioContext.Add("number", number);
        }

        [When(@"I calculate the square root")]
        public void WhenICalculateTheSquareRoot()
        {
            using (var client = new HttpClient())
            {
                var number = _scenarioContext.Get<int>("number");
                var urlBase = _scenarioContext.Get<string>("urlBase");
                var url = $"{urlBase}api/Calculator/";
                var api_call = $"{url}number_attribute?number={number}";

                var response = client.GetAsync(api_call).Result;

                if (!response.IsSuccessStatusCode)
                {
                    _scenarioContext.Add("StatusCode", (int)response.StatusCode);
                    var errorMessage = response.Content.ReadAsStringAsync().Result;
                    _scenarioContext.Add("ErrorMessage", errorMessage);
                    return;
                }

                var responseBody = response.Content.ReadAsStringAsync().Result;
                var jsonDocument = JsonDocument.Parse(responseBody);

                var odd = jsonDocument.RootElement.GetProperty("odd").GetBoolean();
                var prime = jsonDocument.RootElement.GetProperty("prime").GetBoolean();
                var square = jsonDocument.RootElement.GetProperty("square").GetDouble();

                _scenarioContext.Add("SquareRoot", square);
            }
        }


        [Then(@"the calculated square root should be (.*)")]
        public void ThenTheSquareRootOfTheNumberIs(string expected)
        {
            if (expected == "Exception")
            {
                var statusCode = _scenarioContext.Get<int>("StatusCode");
                Assert.Equal(400, statusCode);

                var errorMessage = _scenarioContext.Get<string>("ErrorMessage");
                Assert.Contains("La raiz cuadrada de un numero negativo no se puede calcular.", errorMessage);
            }
            else
            {
                var expectedSquareRoot = double.Parse(expected);
                var squareRoot = _scenarioContext.Get<double>("SquareRoot");
                Assert.Equal(expectedSquareRoot, squareRoot);
            }
        }


    }
}