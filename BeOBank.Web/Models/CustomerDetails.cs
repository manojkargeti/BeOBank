namespace BeOBank.Web.Models;

/// <summary>
/// Represents details about a customer.
/// </summary>
public class CustomerDetails {
    
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public Account? AccountDetails { get; set; }
}