namespace BeOBank.Web.Api.Models;

/// <summary>
/// Represents details about a customer.
/// </summary>
public class CustomerDetails
{
    /// <summary>
    /// Gets or initializes the unique identifier of the customer.
    /// </summary>
    public int CustomerId { get; init; }

    /// <summary>
    /// Gets or sets the name of the customer.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the surname of the customer.
    /// </summary>
    public string? Surname { get; set; }

    /// <summary>
    /// Gets or sets the account details associated with the customer.
    /// </summary>
    public Account? AccountDetails { get; set; }
}