using BeOBank.Web.Api.Models;
using BeOBank.Web.Api.Services.CustomerAccountService.ServiceModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;

namespace BeOBank.Web.Api.Tests.Controllers
{
    [TestClass]
    public class CustomerControllerIntegrationTests
    {
        private HttpClient _httpClient;

        public CustomerControllerIntegrationTests()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }
        
        [TestMethod, TestCategory("IntegrationTest")]
        public async Task GetCustomerById_ReturnsCustomersDetails_WhenIdIsValid()
        {
            var response = await _httpClient.GetAsync(ApiEndpoints.GetAllCustomersDetails);
            var stringResult = await response.Content.ReadAsStringAsync();
            var content = await response.Content.ReadAsStringAsync();
            var customer =
                JsonConvert.DeserializeObject<IList<CustomerDetails>>(
                    content); 

            // Assert
            Assert.IsNotNull(customer);
            Assert.AreEqual(4 , customer.Count);
        }


        [TestMethod, TestCategory("IntegrationTest")]
        public async Task CreateCustomerAccount_Returns200OK()
        {
            // Arrange
            var request = new CreateAccountRequest
            {
                CustomerId = 1,
                InitialCredit = 200
            };

            var jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            // Act: Make an HTTP POST request to the endpoint
            var response = await _httpClient.PostAsync(ApiEndpoints.CreateCustomerAccount, content);

            // Assert
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);

        }

        [TestMethod, TestCategory("IntegrationTest")]
        public async Task GetCustomerById_Returns200OK()
        {
            // Arrange 
            int customerId = 1;

            // Act 
            var response = await _httpClient.GetAsync(ApiEndpoints.GetCustomerById(customerId));

            // Assert
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}