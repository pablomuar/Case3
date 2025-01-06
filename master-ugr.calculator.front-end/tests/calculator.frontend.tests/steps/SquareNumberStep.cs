using System;
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
            var number = _scenarioContext.Get<int>("number");
            var sqrt = Math.Round(Math.Sqrt(number), 2); // Calcula y redondea a dos decimales
            _scenarioContext.Add("sqrt", sqrt);
        }

        [Then(@"the square root result should be (.*)")]
        public void ThenTheResultShouldBe(double expectedSqrt)
        {
            var actualSqrt = _scenarioContext.Get<double>("sqrt");
            Assert.Equal(expectedSqrt, actualSqrt);
        }
    }
}