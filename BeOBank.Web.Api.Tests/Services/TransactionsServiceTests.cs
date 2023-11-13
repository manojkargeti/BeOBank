using BeOBank.Web.Api.Models;
using BeOBank.Web.Api.Services.TransactionsService;
using BeOBank.Web.Api.Services.TransactionsService.ServiceModels;

namespace BeOBank.Web.Api.Tests.Services
{
    [TestClass]
    public class TransactionsServiceTests
    {
        #region Fields

        private IList<CustomerDetails> _customers;

        #endregion

        #region Constructor
        public TransactionsServiceTests(IList<CustomerDetails> customers)
        {
            this._customers = customers;
        }
        #endregion
        
        #region Initialization
        [TestInitialize]
        public void Setup()
        {
            _customers = new List<CustomerDetails>
            {
                new CustomerDetails { CustomerId = 1, Name = "John", Surname = "Doe" },
                new CustomerDetails { CustomerId = 2, Name = "Jane", Surname = "Doe" }
              
            };

            
        }
        #endregion
        
        #region Transaction
        [TestMethod, TestCategory("UnitTest")]
        public void AddTransaction_SuccessfullyAddsTransaction()
        {
            // Arrange
            _customers = new List<CustomerDetails>
            {
                new CustomerDetails { CustomerId = 1, Name = "John", Surname = "Doe" },
                new CustomerDetails { CustomerId = 2, Name = "Jane", Surname = "Doe" }
                
            };

            var service = new TransactionsService(_customers);
            var transactionRequest = new TransactionRequest
            {
                CustomerId = 1,
                CreditAmount = 100 
             
            };

            // Act
            var result = service.AddTransaction(transactionRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(100, result.CreditAmount);
        }

        [TestMethod, TestCategory("UnitTest")]
        [ExpectedException(typeof(Exception), "Customer not found. Transaction failed")]
        public void AddTransaction_CustomerNotFound_ThrowsException()
        {
            // Arrange
            var customers = new List<CustomerDetails>
            {
                new CustomerDetails { CustomerId = 1, Name = "John", Surname = "Doe" },
                new CustomerDetails { CustomerId = 2, Name = "Jane", Surname = "Doe" }
               
            };

            var service = new TransactionsService(customers);
            var transactionRequest = new TransactionRequest
            {
                CustomerId = 91,
                CreditAmount = 101
            };

            // Act
            service.AddTransaction(transactionRequest);
            
            //Assert
        }
        
        #endregion
    }
}
