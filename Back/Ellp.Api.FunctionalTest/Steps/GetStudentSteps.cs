using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;
using Xunit;

namespace Ellp.Api.FunctionalTest.Steps
{
    [Binding]
    public class GetStudentSteps
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private HttpResponseMessage _response;

        public GetStudentSteps()
        {
            _configuration = ConfigurationHelper.Configuration;
            var baseAddress = _configuration["ApiBaseUrl"];
            _client = new HttpClient { BaseAddress = new System.Uri(baseAddress) };
        }

        [Given(@"I have the student email ""(.*)"" and password ""(.*)""")]
        public void GivenIHaveTheStudentEmailAndPassword(string email, string password)
        {
            ScenarioContext.Current["Email"] = email;
            ScenarioContext.Current["Password"] = password;
        }

        [When(@"I request the student details")]
        public async Task WhenIRequestTheStudentDetails()
        {
            var email = ScenarioContext.Current["Email"].ToString();
            var password = ScenarioContext.Current["Password"].ToString();
            var requestUri = $"api/Student/login?email={email}&password={password}";

            _response = await _client.GetAsync(requestUri);
        }

        [Then(@"the response should be successful")]
        public void ThenTheResponseShouldBeSuccessful()
        {
            _response.EnsureSuccessStatusCode();
        }

        [Then(@"the student details should be returned")]
        public async Task ThenTheStudentDetailsShouldBeReturned()
        {
            var responseData = await _response.Content.ReadAsStringAsync();
            Assert.False(string.IsNullOrEmpty(responseData));
        }
    }
}
