using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.CommonModels;
using static System.Net.WebRequestMethods;

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

        [Then(@"the result should be (.*)")]
        [Then(@"the result shall be (.*)")]
        [Then(@"the result is (.*)")]
        public void ThenTheResultShouldBe(double expectedResult)
        {
            var result = _scenarioContext.Get<double>("result");
            Assert.Equal(expectedResult, result);
        }

        [Then(@"the result shall be NaN")]
        public void ThenTheResultShallBeNaN()
        {
            var result = _scenarioContext.Get<string>("result");
            Assert.Equal("NaN", result);
        }
    }
}
