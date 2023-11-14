using BeOBank.Web.Api.Models;
using BeOBank.Web.Api.Services.CustomerAccountService;
using BeOBank.Web.Api.Services.CustomerAccountService.Enums;
using BeOBank.Web.Api.Services.CustomerAccountService.ServiceModels;
using BeOBank.Web.Api.Services.TransactionsService;
using BeOBank.Web.Api.Services.TransactionsService.ServiceModels;
using Moq;

namespace BeOBank.Web.Api.Tests.Services
{
    [TestClass]
    public class CustomerAccountServiceTests
    {
        #region Fields

        #pragma warning disable CS8618
                private IList<CustomerDetails> _customers;
        #pragma warning restore CS8618


        #endregion

        #region Initialization

        [TestInitialize]
        public void Setup()
        {
            _customers = new List<CustomerDetails>
            {
                new CustomerDetails { CustomerId = 1, Name = "John", Surname = "Doe" }
            };
        }

        #endregion

        #region CustomerAccount

        [TestMethod, TestCategory("UnitTest")]
        public void CreateCustomerAccount_SuccessfullyCreatesAccountWithInitialCredit()
        {
            // Arrange

            var transactionsServiceMock = new Mock<ITransactionsService>();
            transactionsServiceMock.Setup(x => x.AddTransaction(It.IsAny<TransactionRequest>()))
                .Returns(new CustomerTransaction()
                {
                    CreditAmount = 101
                });

            var service = new CustomerAccountService(_customers, transactionsServiceMock.Object);
            var createAccountRequest = new CreateAccountRequest
            {
                CustomerId = 1,
                InitialCredit = 101

            };

            // Act
            var result = service.CreateCustomerAccount(createAccountRequest);

            // Assert
            Assert.AreEqual(CreateAccountMessage.AccountCreatedAndAmountCreditedSuccessfully, result.MessageToCustomer);
            Assert.IsNotNull(_customers.First().AccountDetails);
            Assert.IsNotNull(_customers.First().AccountDetails?.Transactions);
            Assert.AreEqual(1, _customers.First().AccountDetails?.Transactions.Count);

        }

        [TestMethod, TestCategory("UnitTest")]
        public void CreateCustomerAccount_CreditAmountWithoutExistingAccount_SuccessfullyAddsTransaction()
        {
            // Arrange

            var transactionsServiceMock = new Mock<ITransactionsService>();
            transactionsServiceMock.Setup(x => x.AddTransaction(It.IsAny<TransactionRequest>()))
                .Returns(new CustomerTransaction { CreditAmount = 1001 });

            var service = new CustomerAccountService(_customers, transactionsServiceMock.Object);
            var createAccountRequest = new CreateAccountRequest
            {
                CustomerId = 1,
                InitialCredit = 1001

            };

            // Act
            var result = service.CreateCustomerAccount(createAccountRequest);

            // Assert
            Assert.AreEqual(CreateAccountMessage.AccountCreatedAndAmountCreditedSuccessfully, result.MessageToCustomer);
            Assert.IsNotNull(_customers.First().AccountDetails);
            Assert.IsNotNull(_customers.First().AccountDetails?.Transactions);
            Assert.AreEqual(1, _customers.First().AccountDetails?.Transactions.Count);

        }
        #endregion
    }
}