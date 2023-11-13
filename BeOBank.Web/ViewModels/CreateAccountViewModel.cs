using System.ComponentModel.DataAnnotations;

namespace BeOBank.Web.ViewModels
{
    public class CreateAccountViewModel
    {
        [Required(ErrorMessage = "Customer ID is required.")]
        [Range(1, 4, ErrorMessage = "Customer ID must be between 1 and 4.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Initial Credit is required.")]
        public int InitialCredit { get; set; }
    }
}
