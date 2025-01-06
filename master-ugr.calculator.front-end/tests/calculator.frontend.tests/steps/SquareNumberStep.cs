using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task WhenICalculateItsSquareRootAsync()
        {
            var page = _scenarioContext.Get<IPage>("page");
            var base_url = _scenarioContext.Get<string>("urlBase");
            var number = _scenarioContext.Get<int>("number");
            await page.GotoAsync($"{base_url}/Attribute");
            await page.FillAsync("#number", number.ToString());
            await page.ClickAsync("#attribute");
        }


        [Then(@"the square root result should be (.*)")]
        public async Task ThenTheResultShouldBe(double expectedSqrt)
        {
            var page = (IPage)_scenarioContext["page"];

            // Obtén el texto del resultado y conviértelo a double
            var actualSqrtText = await page.InnerTextAsync("#SquareRoot");
            if (!double.TryParse(actualSqrtText, out var actualSqrt))
            {
                throw new InvalidOperationException("No se pudo convertir el resultado a un número válido.");
            }
        }

    }
}