using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [Given(@"a number to calculate square root (.*)")]
        public void GivenANumber(int number)
        {
            _scenarioContext.Add("number", number);
        }

        [When("I calculate the square root")]
        public void WhenICalculateTheSquareRoot()
        {
            var number = _scenarioContext.Get<int>("number");
            var squareRoot = NumberAttributter.GetSquareRoot(number);
            _scenarioContext.Add("squareRoot", squareRoot);
        }

        [Then("the calculated square root should be (.*)")]
        public void ThenTheResultShouldBe(string expected)
        {
            var squareRoot = _scenarioContext.Get<double?>("squareRoot");
            double? expectedValue = expected == "null" ? (double?)null : double.Parse(expected);
            Assert.Equal(expectedValue, squareRoot);
        }
    }
}