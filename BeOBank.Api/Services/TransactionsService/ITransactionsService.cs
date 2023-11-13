using BeOBank.Web.Api.Models;
using BeOBank.Web.Api.Services.TransactionsService.ServiceModels;

namespace BeOBank.Web.Api.Services.TransactionsService;

/// <summary>
/// ITransactionsService
/// </summary>
public interface ITransactionsService
{
 
    /// <summary>
    /// AddTransaction
    /// </summary>
    /// <param name="transactionRequest"></param>
    /// <returns>CustomerTransaction</returns>
    CustomerTransaction AddTransaction(TransactionRequest transactionRequest);
}