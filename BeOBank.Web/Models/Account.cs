 

namespace BeOBank.Web.Models
{
    public class Account
    {
        public Guid AccountNumber { get; set; }
        public int Balance { get; set; }
        public List<CustomerTransaction>? Transactions { get; set; }
    }
}
