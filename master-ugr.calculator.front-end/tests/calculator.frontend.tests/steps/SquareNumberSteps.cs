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
            IPage page = _scenarioContext.Get<IPage>("page");
            var base_url = _scenarioContext.Get<string>("urlBase");
            var number = _scenarioContext.Get<int>("number");

            await page.GotoAsync($"{base_url}/Attribute");

            await page.FillAsync("#number", number.ToString());
            await page.ClickAsync("#attribute");

            var resultTask = page.WaitForSelectorAsync("#squareRoot", new PageWaitForSelectorOptions { State = WaitForSelectorState.Attached });
            var errorTask = page.WaitForSelectorAsync("#error", new PageWaitForSelectorOptions { State = WaitForSelectorState.Attached });

            var completedTask = await Task.WhenAny(resultTask, errorTask);

            if (completedTask == errorTask)
            {
                var errorMessage = await page.InnerTextAsync("#error");
                _scenarioContext["error"] = errorMessage;
            }
            else
            {
                var squareRoot = await page.InnerTextAsync("#squareRoot");
                _scenarioContext["squareRoot"] = squareRoot;
            }
        }

        [Then(@"the calculated square root should be (.*)")]
        public void ThenTheResultShouldBe(string expected)
        {
            if (expected == "Exception")
            {
                Assert.True(_scenarioContext.ContainsKey("error"), "No se encontró un mensaje de error en el frontend.");
                var errorMessage = _scenarioContext["error"].ToString();
                Assert.Contains("La raiz cuadrada de un numero negativo no se puede calcular.", errorMessage);
            }
            else
            {
                Assert.True(_scenarioContext.ContainsKey("squareRoot"), "No se encontró un resultado de raíz cuadrada en el frontend.");
                var squareRoot = _scenarioContext["squareRoot"].ToString();
                Assert.Equal(expected, squareRoot);
            }
        }

    }
}
