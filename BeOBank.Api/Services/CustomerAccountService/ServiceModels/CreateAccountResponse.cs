using BeOBank.Web.Api.Services.CustomerAccountService.Enums;

namespace BeOBank.Web.Api.Services.CustomerAccountService.ServiceModels;

/// <summary>
/// Represents the response for creating a customer account.
/// </summary>
public class CreateAccountResponse
{
    /// <summary>
    /// Gets or sets the message indicating the result of the account creation.
    /// </summary>
    public CreateAccountMessage MessageToCustomer { get; set; }
}
