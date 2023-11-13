using System.Security.Principal;
using BeOBank.Web.Models;

namespace BeOBank.Web.ViewModels
{
    public class CustomerDetailsViewModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public Account? AccountDetails { get; set; }
 
    }
}
