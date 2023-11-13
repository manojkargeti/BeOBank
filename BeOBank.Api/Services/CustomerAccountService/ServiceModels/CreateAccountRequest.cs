 
namespace BeOBank.Web.Api.Services.CustomerAccountService.ServiceModels;

/// <summary>
/// Represents a request to create a customer account.
/// </summary>
public class CreateAccountRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the customer.
    /// </summary>
    public int CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the initial credit amount for the customer account.
    /// </summary>
    public int InitialCredit { get; set; }
}