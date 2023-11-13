namespace BeOBank.Web.Resources
{
    public static class ApiEndpoints
    {
        public const string BaseUrl = "http://localhost:5115/api/Customer/";

        public const string GetAllCustomersDetails = "GetAllCustomersDetails";
        public static string GetCustomerById(int customerId) => $"GetCustomerById/{customerId}";

        public static string CreateCustomerAccount = "CreateAccount";
    }
}
