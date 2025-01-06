using System;
using System.Text.Json;
using TechTalk.SpecFlow;
using Xunit;

namespace calculator.lib.test.steps
{
    [Binding]
    public class SquareRootSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public SquareRootSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"a square-root number (\d+)")]
        public void GivenANumber(int number)
        {
            _scenarioContext.Add("number", number);
        }

        [When(@"I calculate the square root of the number")]
        public void WhenICalculateItsSquareRoot()
        {
            using (var client = new HttpClient())
            {
                var number = _scenarioContext.Get<int>("number");
                var urlBase = _scenarioContext.Get<string>("urlBase");
                var url = $"{urlBase}api/Calculator/";
                var api_call = $"{url}number_attribute?number={number}";
                var response = client.GetAsync(api_call).Result;
                response.EnsureSuccessStatusCode();
                var responseBody = response.Content.ReadAsStringAsync().Result;
                var jsonDocument = JsonDocument.Parse(responseBody);
                var square = jsonDocument.RootElement.GetProperty("sqrt").GetBoolean();
                _scenarioContext.Add("SquareRoot", square);
            }
        }

        [Then(@"the square root result should be (.*)")]
        public async Task ThenTheResultShouldBe(double expectedSqrt)
        {
            var actualSqrt = _scenarioContext.Get<double>("SquareRoot");
            Assert.Equal(expectedSqrt, actualSqrt);
        }
    }
}