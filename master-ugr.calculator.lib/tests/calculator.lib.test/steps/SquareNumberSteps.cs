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
            var sqrt = NumberAttributter.SquareRoot(number);
            _scenarioContext.Add("SquareRoot", sqrt);
        }

        [Then(@"the square root result should be (.*)")]
        public void ThenTheResultShouldBe(double expectedSqrt)
        {
            var actualSqrt = _scenarioContext.Get<double>("SquareRoot");
            Assert.Equal(expectedSqrt, actualSqrt);
        }
    }
}
