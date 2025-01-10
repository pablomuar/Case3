using System;
using TechTalk.SpecFlow;
using Xunit; // Usando Xunit para las aserciones

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
            _scenarioContext["number"] = number;
        }

        [When(@"I calculate the square root")]
        public void WhenICalculateTheSquareRoot()
        {
            int number = _scenarioContext.Get<int>("number");
            try
            {
                double result = NumberAttributter.GetSquareRoot(number);
                _scenarioContext["result"] = result;
            }
            catch (ArgumentException ex)
            {
                _scenarioContext["result"] = "Exception";
                _scenarioContext["exceptionMessage"] = ex.Message;
            }
        }

        [Then(@"the calculated square root should be (.*)")]
        public void ThenTheCalculatedSquareRootShouldBe(string expected)
        {
            var result = _scenarioContext["result"].ToString();

            if (expected == "Exception")
            {
                Assert.Equal("Exception", result);
                Assert.Equal("La raiz cuadrada de un numero negativo no se puede calcular.", _scenarioContext["exceptionMessage"].ToString());
            }
            else
            {
                double expectedResult = double.Parse(expected);
                Assert.Equal(expectedResult, double.Parse(result));
            }
        }
    }
}
