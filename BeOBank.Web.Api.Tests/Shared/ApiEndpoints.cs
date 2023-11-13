namespace BeOBank.Web.Api.Tests
{
    public static class ApiEndpoints
    {
        public const string BaseUrl = "http://localhost:5115/api/Customer/";

        public const string GetAllCustomersDetails = BaseUrl+"GetAllCustomersDetails";
        public static string GetCustomerById(int customerId) => BaseUrl+$"GetCustomerById/{customerId}";

        public static string CreateCustomerAccount = BaseUrl+"CreateAccount";
    }
}
