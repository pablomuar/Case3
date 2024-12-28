using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace calculator.frontend.tests.steps
{
    [Binding]
    public class OddNumberSteps
    {
        private readonly ScenarioContext _scenarioContext;

        public OddNumberSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [Given(@"a number (.*)")]
        public void GivenANumber(int number)
        {
            _scenarioContext.Add("number", number);
        }

        [When(@"I check if it is odd")]
        public async Task WhenICheckIfItIsOdd()
        {
            IPage page = _scenarioContext.Get<IPage>("page");
            var base_url = _scenarioContext.Get<string>("urlBase");
            var number = _scenarioContext.Get<int>("number");
            await page.GotoAsync($"{base_url}/Attribute");
            await page.FillAsync("#number", number.ToString());
            await page.ClickAsync("#attribute");
        }

        [Then(@"it should be odd (.*)")]
        public async Task ThenItShouldBeOddTrue(bool isIt_boolean)
        {
            var page = (IPage)_scenarioContext["page"];
            var resultText = await page.InnerTextAsync("#isOdd");
            if (isIt_boolean)
            {
                Assert.Equal("Yes", resultText);
            }
            else
            {
                Assert.Equal("No", resultText);
            }
        }
    }
}
