namespace BeOBank.Web.Api.Models;

/// <summary>
/// Represents a customer Current account.
/// </summary>
public class Account
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Account"/> class.
    /// </summary>
    public Account()
    {
        AccountNumber = Guid.NewGuid();
        Transactions = new List<CustomerTransaction>();
        _balance = 0;
    }

    /// <summary>
    /// Gets the unique identifier of the account.
    /// </summary>
    public Guid AccountNumber { get; }

    private int _balance;

    /// <summary>
    /// Gets the current balance of the account, including all transactions.
    /// </summary>
    public int Balance
    {
        get { return _balance + Transactions.Sum(t => t.CreditAmount); }
    }

    /// <summary>
    /// Gets or sets the list of transactions associated with the account.
    /// </summary>
    public List<CustomerTransaction> Transactions { get; set; }
}
