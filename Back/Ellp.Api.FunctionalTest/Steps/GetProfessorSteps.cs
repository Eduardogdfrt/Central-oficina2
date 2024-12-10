using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;
using Xunit;

namespace Ellp.Api.FunctionalTest.Steps
{
    [Binding]
    public class GetProfessorSteps
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private HttpResponseMessage _response;

        public GetProfessorSteps()
        {
            _configuration = ConfigurationHelper.Configuration;
            var baseAddress = _configuration["ApiBaseUrl"];
            _client = new HttpClient { BaseAddress = new System.Uri(baseAddress) };
        }

        [Given(@"I have the professor ID ""(.*)"" and password ""(.*)""")]
        public void GivenIHaveTheProfessorIDAndPassword(string professorId, string password)
        {
            ScenarioContext.Current["ProfessorId"] = professorId;
            ScenarioContext.Current["Password"] = password;
        }

        [When(@"I request the professor details")]
        public async Task WhenIRequestTheProfessorDetails()
        {
            var professorId = ScenarioContext.Current["ProfessorId"].ToString();
            var password = ScenarioContext.Current["Password"].ToString();
            var requestUri = $"api/Professor/login?professorId={professorId}&password={password}";

            _response = await _client.GetAsync(requestUri);
        }

        [Then(@"the response should be successful")]
        public void ThenTheResponseShouldBeSuccessful()
        {
            _response.EnsureSuccessStatusCode();
        }

        [Then(@"the professor details should be returned")]
        public async Task ThenTheProfessorDetailsShouldBeReturned()
        {
            var responseData = await _response.Content.ReadAsStringAsync();
            Assert.False(string.IsNullOrEmpty(responseData));
        }
    }
}
