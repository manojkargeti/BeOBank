using BeOBank.Web.Models;
using BeOBank.Web.ViewModels;

namespace BeOBank.Web.Mappers
{
    public interface ICustomerDetailsMapper
    {
        List<CustomerDetailsViewModel>? MapToViewModelList(List<CustomerDetails> customerDetailsList);
        CustomerDetailsViewModel MapToViewModel(CustomerDetails customerDetails);
    }

}
