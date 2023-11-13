using BeOBank.Web.Api.Models;
using BeOBank.Web.Api.Services.CustomerAccountService.Enums;
using BeOBank.Web.Api.Services.CustomerAccountService.ServiceModels;
using BeOBank.Web.Api.Services.TransactionsService;
using BeOBank.Web.Api.Services.TransactionsService.ServiceModels;

namespace BeOBank.Web.Api.Services.CustomerAccountService;

/// <summary>
/// CustomerAccountService
/// </summary>
public class CustomerAccountService : ICustomerAccountService
{
    private readonly IList<CustomerDetails> _customers;
    private readonly ITransactionsService _transactionsService;

    /// <summary>
    /// CustomerAccountService
    /// </summary>
    /// <param name="customers"></param>
    /// <param name="transactionsService"></param>
    public  CustomerAccountService(IList<CustomerDetails> customers,
        ITransactionsService transactionsService)
    {
        _customers = customers;
        _transactionsService = transactionsService;
    }

    /// <summary>
    /// CreateCustomerAccount
    /// </summary>
    /// <param name="createAccountRequest"></param>
    /// <returns>CreateAccountResponse</returns>
    public CreateAccountResponse CreateCustomerAccount(CreateAccountRequest createAccountRequest)
    {
        var existingCustomer = _customers.FirstOrDefault(x => x.CustomerId == createAccountRequest.CustomerId);
      
        if (existingCustomer != null && createAccountRequest.InitialCredit > 0)
        {
            if (existingCustomer.AccountDetails is null )
            {
                //CreatAccountAsIdDoNotExist
                existingCustomer.AccountDetails = new Account
                {
                    Transactions = new List<CustomerTransaction>
                    {
                        _transactionsService.AddTransaction(new TransactionRequest
                        {
                            CustomerId = createAccountRequest.CustomerId,
                            CreditAmount = createAccountRequest.InitialCredit
                        })
                    }
                };
                return new CreateAccountResponse
                {
                    MessageToCustomer = CreateAccountMessage.AccountCreatedAndAmountCreditedSuccessfully
                };
            }
            
            //asOnlyAccountIsThereSoJustUpdateIt
            else
            {
                existingCustomer.AccountDetails.Transactions.Add(_transactionsService.AddTransaction(
                    new TransactionRequest
                    {
                        CustomerId = createAccountRequest.CustomerId,
                        CreditAmount = createAccountRequest.InitialCredit
                    }));

                return new CreateAccountResponse
                {
                    MessageToCustomer = CreateAccountMessage.AmountCreditedSuccessfully
                };
            }
            
        }

        return new CreateAccountResponse
        {
            MessageToCustomer = CreateAccountMessage.Error
        };
    }

    /// <summary>
    /// GetCustomerById
    /// </summary>
    /// <param name="id"></param>
    /// <returns>CustomerDetails</returns>
    public CustomerDetails? GetCustomerById(int id)
    {
        return _customers.FirstOrDefault(x => x.CustomerId == id);
    }

    
    //JustToKnowWhatAllStaticDataWeHave
    /// <summary>
    /// GetAllCustomerDetails
    /// </summary>
    /// <returns>List of CustomerDetails</returns>
    public IList<CustomerDetails> GetAllCustomerDetails ()
    {
        return _customers;
    }
}