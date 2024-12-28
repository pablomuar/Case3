using TechTalk.SpecFlow;
using static System.Net.WebRequestMethods;

namespace calculator.backend.test.Hooks
{
    [Binding]
    public class BeforeScenario
    {
        private readonly ScenarioContext _scenarioContext;
        public BeforeScenario(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [BeforeScenario]
        public void ScenarioPreparation(ScenarioContext scenarioContext)
        {
			// Getting url from environment variable
			// When not present, default to http://localhost:5226
			var urlBase =
                Environment.GetEnvironmentVariable("CALCULATOR_BACKEND_URL") ?? "http://localhost:5226";
            urlBase = urlBase + "/";
            _scenarioContext.Add("urlBase", urlBase);
        }
    }
}
