namespace BeOBank.Web.Models
{
    public class CustomerTransaction
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public int CreditAmount { get; set; }
    }
}
