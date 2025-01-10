using Microsoft.Playwright;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace calculator.frontend.tests.steps
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

        [When(@"I calculate the square root")]
        public async Task WhenICalculateTheSquareRoot()
        {
            var page = _scenarioContext.Get<IPage>("page");
            var baseUrl = _scenarioContext.Get<string>("urlBase");
            var number = _scenarioContext.Get<int>("number");

            await page.GotoAsync($"{baseUrl}/Attribute");
            await page.FillAsync("#number", number.ToString());
            await page.ClickAsync("#attribute");

            await page.WaitForSelectorAsync("#result", new PageWaitForSelectorOptions { State = WaitForSelectorState.Attached });
        }

        [Then(@"the calculated square root should be (.*)")]
        public async Task ThenTheResultShouldBe(string expected)
        {
            var page = _scenarioContext.Get<IPage>("page");
            var resultText = await page.InnerTextAsync("#squareRoot");

            if (expected == "null")
            {
                Assert.Equal("null", resultText);
            }
            else if (expected == "Exception")
            {
                var errorText = await page.InnerTextAsync("#error");
                Assert.Contains("No se puede calcular", errorText);
            }
            else
            {
                Assert.Equal(expected, resultText);
            }
        }
    }
}
