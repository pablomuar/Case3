using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace calculator.frontend.tests.steps
{
    [Binding]
    public class PrimeNumberSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public PrimeNumberSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [When(@"number (.*) is checked")]
        public async Task WhenNumberIsChecked(int number)
        {
            IPage page = _scenarioContext.Get<IPage>("page");
            var base_url = _scenarioContext.Get<string>("urlBase");
            await page.GotoAsync($"{base_url}/Attribute");
            await page.FillAsync("#number", number.ToString());
            await page.ClickAsync("#attribute");
        }

        [Then(@"the answer to know whether is prime or not is (.*)")]
        public async Task ThenTheAnswerToKnowWhetherIsPrimeOrNotIs(string isPrime)
        {
            var page = (IPage)_scenarioContext["page"];
            var resultText = await page.InnerTextAsync("#isPrime");
            Assert.Equal(isPrime, resultText);
        }

    }
}
