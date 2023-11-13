using System.Net;
using BeOBank.Web.Mappers;
using BeOBank.Web.Models;
using BeOBank.Web.Models.Enums;
using BeOBank.Web.Resources;
using BeOBank.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace BeOBank.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICustomerDetailsMapper _customerDetailsMapper;

        public HomeController(IHttpClientFactory httpClientFactory,
            ICustomerDetailsMapper customerDetailsMapper)
        {
            _httpClientFactory = httpClientFactory;
            _customerDetailsMapper = customerDetailsMapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {

            var client = _httpClientFactory.CreateClient("BeOBankAPI");

            var response = await client.GetAsync(ApiEndpoints.GetAllCustomersDetails);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                // Deserialize list of CustomerDetails
                var customerDetailsList =
                    JsonConvert.DeserializeObject<List<CustomerDetails>>(responseData);

                //CallMapper
                var model = _customerDetailsMapper
                    .MapToViewModelList(customerDetailsList!);

                return View(model);

            }

            //ItShouldNotHappenStaticData
            return View();



        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            if (id > 0 && id < 5)
            {
                var client = _httpClientFactory.CreateClient("BeOBankAPI");

                var response = await client.GetAsync(ApiEndpoints.GetCustomerById(id));

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();

                    // Deserialize list of CustomerDetails
                    var customerDetailsList =
                        JsonConvert.DeserializeObject<CustomerDetails>(responseData);

                    //CallMapper
                    var model = _customerDetailsMapper
                        .MapToViewModel(customerDetailsList!);
                    return View(model);
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {

                    return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = (int)response.StatusCode });
                }
                else
                {

                    return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = (int)response.StatusCode });
                }
            }
            else
            {

                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 500 });

            }
        }

        public async Task<IActionResult> Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerAccount(CreateAccountViewModel request)
        {
            var client = _httpClientFactory.CreateClient("BeOBankAPI");

            // Assuming ApiEndpoints.CreateCustomerAccount is the endpoint for CreateCustomerAccount
            var response = await client.PostAsJsonAsync(ApiEndpoints.CreateCustomerAccount, request);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<CreateAccountResponse>(responseData);

                if (apiResponse != null)
                    switch (apiResponse.MessageToCustomer)
                    {
                        case CreateAccountMessage.AmountCreditedSuccessfully:
                            return RedirectToAction("GetCustomerById", "Home", new { id = request.CustomerId });
                        case CreateAccountMessage.AccountCreatedAndAmountCreditedSuccessfully:
                            return RedirectToAction("GetCustomerById", "Home", new { id = request.CustomerId });
                        case CreateAccountMessage.Error:
                            return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = (int)response.StatusCode }); // return a 404 status for the "Customer does not exist" case
                        default:
                            return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 500 }); // any other cases
                    }
            }

            // Handle unsuccessful response
            return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = 500 });
        }
    }
}
