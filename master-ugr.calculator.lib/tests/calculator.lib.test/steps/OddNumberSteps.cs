using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit; // Necesario para usar Assert

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

        [Given(@"a number (.*)")]
        public void GivenANumber(double number)
        {
            _scenarioContext.Add("number", number);
        }

        [When("I calculate the square root")]
        public void ICalculateTheSquareRoot()
        {
            var number = _scenarioContext.Get<double>("number");
            var squareRoot = Math.Sqrt(number);
            _scenarioContext.Add("squareRoot", squareRoot);
        }

        [Then("the result should be (.*)")]
        public void TheResultShouldBe(double expected)
        {
            var squareRoot = _scenarioContext.Get<double>("squareRoot");
            Assert.Equal(expected, squareRoot, precision: 2);
        }
    }
}
