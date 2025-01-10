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

        [Given(@"a number to calculate square root (.*)")]
        public void WhenNumberIsChecked(int number)
        {
            _scenarioContext.Add("number", number);
        }

        [When("I calculate the square root")]
        public void WhenICalculateTheSquareRoot()
        {
            var number = _scenarioContext.Get<int>("number");
            var square = NumberAttributter.GetSquareRoot(number);
            _scenarioContext.Add("SquareRoot", square);
        }

        [Then("the calculated square root should be (.*)")]
        public void ThenTheSquareRootOfTheNumberIs(double expected)
        {
            var square = _scenarioContext.Get<double>("SquareRoot");
            Assert.Equal(expected, square);
        }
    }
}