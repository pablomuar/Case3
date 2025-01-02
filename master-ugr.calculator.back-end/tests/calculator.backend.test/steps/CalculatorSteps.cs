using System;
using System.Net.Http;
using System.Text.Json;
using TechTalk.SpecFlow;
using Xunit; // Asegúrate de tener esta referencia para usar Assert

namespace calculator.lib.test.steps
{
    [Binding]
    public class CalculatorSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public CalculatorSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"the first number is (.*)")]
        public void GivenTheFirstNumberIs(int firstNumber)
        {
            _scenarioContext.Add("firstNumber", firstNumber);
        }

        [Given(@"the second number is (.*)")]
        public void GivenTheSecondNumberIs(int secondNumber)
        {
            _scenarioContext.Add("secondNumber", secondNumber);
        }

        private void ApiCall(string operation)
        {
            using (var client = new HttpClient())
            {
                // Asegúrate de que "urlBase" esté definido en el contexto
                var urlBase = _scenarioContext.Get<string>("urlBase");
                var firstNumber = _scenarioContext.Get<int>("firstNumber");
                var secondNumber = _scenarioContext.Get<int>("secondNumber");
                var url = $"{urlBase}api/Calculator/";
                var api_call = $"{url}{operation}?a={firstNumber}&b={secondNumber}";
                var response = client.GetAsync(api_call).Result;
                response.EnsureSuccessStatusCode();
                var responseBody = response.Content.ReadAsStringAsync().Result;

                // Manejar el caso de NaN
                if (responseBody.Contains("NaN"))
                {
                    _scenarioContext.Add("result", "NaN");
                }
                else
                {
                    var jsonDocument = JsonDocument.Parse(responseBody);
                    var result = jsonDocument.RootElement.GetProperty("result").GetDouble();
                    _scenarioContext.Add("result", result);
                }
            }
        }

        [When(@"the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            ApiCall("add");
        }

        [When(@"I divide first number by second number")]
        public void WhenIDivideFirstNumberBySecondNumber()
        {
            ApiCall("divide");
        }

        [When(@"I divide both numbers")] // Agregado para resolver el paso faltante
        public void WhenIDivideBothNumbers()
        {
            ApiCall("divide");
        }

        [When(@"I multiply both numbers")]
        public void WhenIMultiplyBothNumbers()
        {
            ApiCall("multiply");
        }

        [When(@"I substract first number to second number")]
        public void WhenISubstractFirstNumberToSecondNumber()
        {
            ApiCall("subtract");
        }

        // Definiciones de pasos para valores numéricos específicos
        [Then(@"the result should be (-?\d+(\.\d+)?)")]
        [Then(@"the result shall be (-?\d+(\.\d+)?)")]
        [Then(@"the result is (-?\d+(\.\d+)?)")]
        public void ThenTheResultShouldBe(double expectedResult)
        {
            var result = _scenarioContext.Get<double>("result");
            Assert.Equal(expectedResult, result);
        }

        // Definición específica para NaN
        [Then(@"the result shall be NaN")]
        public void ThenTheResultShallBeNaN()
        {
            var result = _scenarioContext.Get<string>("result");
            Assert.Equal("NaN", result);
        }
    }
}
