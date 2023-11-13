using BeOBank.Web.Api.Models;
using BeOBank.Web.Api.Services.CustomerAccountService.ServiceModels;

namespace BeOBank.Web.Api.Services.CustomerAccountService;

/// <summary>
/// ICustomerAccountService
/// </summary>
public interface ICustomerAccountService
{
   /// <summary>
   /// CreateCustomerAccount
   /// </summary>
   /// <param name="createAccountRequest"></param>
   /// <returns>CreateAccountResponse</returns>
   public CreateAccountResponse CreateCustomerAccount(CreateAccountRequest createAccountRequest);
   /// <summary>
   /// GetCustomer By Id
   /// </summary>
   /// <param name="id"></param>
   /// <returns>CustomerDetails</returns>
   public CustomerDetails? GetCustomerById(int id);
   /// <summary>
   /// Get AllCustomer Details
   /// </summary>
   /// <returns>List of CustomerDetails</returns>
   public IList<CustomerDetails> GetAllCustomerDetails();
}