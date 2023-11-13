using BeOBank.Web.Api.Models;
using BeOBank.Web.Api.Services.TransactionsService.ServiceModels;

namespace BeOBank.Web.Api.Services.TransactionsService;

/// <summary>
/// TransactionsService
/// </summary>
public class TransactionsService : ITransactionsService
{
    private readonly IList<CustomerDetails> _customers;

    /// <summary>
    /// TransactionsService
    /// </summary>
    /// <param name="customers"></param>
    public TransactionsService(IList<CustomerDetails> customers)
    {
        _customers = customers;
    }

    /// <inheritdoc />
    public  CustomerTransaction AddTransaction(TransactionRequest transactionRequest)
    {
        {
            var customer = _customers.FirstOrDefault(x => x.CustomerId == transactionRequest.CustomerId);

            if (customer != null)
            {
                return new CustomerTransaction
                {
                    CreditAmount = transactionRequest.CreditAmount
                };
            }
            
            else
            {
                throw new Exception("Customer not found. Transaction failed");
            }
        }
    }
}