namespace BeOBank.Web.Api.Models;

/// <summary>
/// Represents a customer transaction.
/// </summary>
public class CustomerTransaction
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomerTransaction"/> class.
    /// </summary>
    public CustomerTransaction()
    {
        Id = Guid.NewGuid();
        DateTime = DateTime.Now;
    }

    /// <summary>
    /// Gets the unique identifier of the transaction.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets the date and time of the transaction.
    /// </summary>
    public DateTime DateTime { get; private set; }

    /// <summary>
    /// Gets or sets the credit amount in the transaction.
    /// </summary>
    public int CreditAmount { get; set; }
}