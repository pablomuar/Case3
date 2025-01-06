using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

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

        [When(@"I calculate the square root of number (.*)")]
        public void WhenICalculateTheSquareRootOfNumber(int number)
        {
            var sqrt = Math.Round(Math.Sqrt(number), 2);
            _scenarioContext.Add("sqrt", sqrt);
        }

        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(double expectedSqrt)
        {
            var actualSqrt = _scenarioContext.Get<double>("sqrt");
            Assert.Equal(expectedSqrt, actualSqrt);
        }
    }
}
