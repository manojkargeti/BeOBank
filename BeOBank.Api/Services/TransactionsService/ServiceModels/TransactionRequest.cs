using System.ComponentModel.DataAnnotations;

namespace BeOBank.Web.Api.Services.TransactionsService.ServiceModels;

/// <summary>
/// TransactionRequest
/// </summary>
public class TransactionRequest
{
    /// <summary>
    /// CustomerId
    /// </summary>
    [Required]
    public int CustomerId { get; set; }
    /// <summary>
    /// CreditAmount
    /// </summary>
    public int CreditAmount { get; set; }
}