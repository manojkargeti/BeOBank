using BeOBank.Web.Models;
using BeOBank.Web.ViewModels;

namespace BeOBank.Web.Mappers
{
    public class CustomerDetailsMapper : ICustomerDetailsMapper
    {
    
        public List<CustomerDetailsViewModel>? MapToViewModelList(List<CustomerDetails> customerDetailsList)
        {
            if (!customerDetailsList.Any())
                return null;

            var viewModelList = new List<CustomerDetailsViewModel>();

            foreach (var customerDetails in customerDetailsList)
            {
                var viewModel = MapToViewModel(customerDetails);
                viewModelList.Add(viewModel);
            }

            return viewModelList;
        }

        public CustomerDetailsViewModel MapToViewModel(CustomerDetails customerDetails)
        {
            return new CustomerDetailsViewModel
            {
                Name = customerDetails.Name,
                Surname = customerDetails.Surname,
                AccountDetails = customerDetails.AccountDetails ?? null
            
            };
        }
    }

}