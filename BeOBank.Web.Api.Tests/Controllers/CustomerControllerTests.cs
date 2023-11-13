using BeOBank.Web.Api.Controllers;
using BeOBank.Web.Api.Models;
using BeOBank.Web.Api.Services.CustomerAccountService;
using BeOBank.Web.Api.Services.CustomerAccountService.Enums;
using BeOBank.Web.Api.Services.CustomerAccountService.ServiceModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BeOBank.Web.Api.Tests.Controllers;

[TestClass]
public class CustomerControllerTests
{
    #region Feilds

    private IList<CustomerDetails> _customers;
    private CustomerController _controller;
    private Mock<ICustomerAccountService> _accountServiceMock;

    #endregion

    #region Initialization

    public CustomerControllerTests(IList<CustomerDetails> customers, CustomerController controller, Mock<ICustomerAccountService> accountServiceMock)
    {
        _customers = customers;
        _controller = controller;
        _accountServiceMock = accountServiceMock;
    }

    [TestInitialize]
    public void Setup()
    {
        _customers = new List<CustomerDetails>
        {
            new CustomerDetails { CustomerId = 1, Name = "Manoj", Surname = "Kargeti" },
            new CustomerDetails { CustomerId = 2, Name = "Michell", Surname = "Jackson" },
            new CustomerDetails { CustomerId = 3, Name = "James", Surname = "Jonson" },
            new CustomerDetails { CustomerId = 4, Name = "Bond", Surname = "Decole" }
        };

        _accountServiceMock = new Mock<ICustomerAccountService>();
        _controller = new CustomerController(_accountServiceMock.Object);
    }


    #endregion

    #region GetCustomerById

    [TestMethod, TestCategory("UnitTest")]
    public void GetCustomerById_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var customerId = 1;

        _accountServiceMock?.Setup(service => service.GetCustomerById(customerId))
            .Returns(_customers.First(x => x.CustomerId == customerId));

        // Act
        var result = _controller.GetCustomerById(customerId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = (OkObjectResult)result;
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        Assert.AreEqual(_customers.First(x => x.CustomerId == customerId), okResult.Value);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetCustomerById_WithInvalidId_ReturnsBadRequestResult()
    {
        // Arrange
        var invalidId = 0;

        // Act
        var result = _controller.GetCustomerById(invalidId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        var badRequestResult = (BadRequestObjectResult)result;
        Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        Assert.AreEqual("Bad Request", badRequestResult.Value);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetCustomerById_WithNonexistentCustomer_ReturnsNotFoundResult()
    {
        // Arrange
        var customerId = 2;

        _accountServiceMock.Setup(service => service.GetCustomerById(customerId))
            .Returns((CustomerDetails)null!);

        // Act
        var result = _controller.GetCustomerById(customerId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        var notFoundResult = (NotFoundObjectResult)result;
        Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        Assert.AreEqual("AccountHolder Not found", notFoundResult.Value);
    }


    #endregion

    #region  GetAllCustomers
    [TestMethod, TestCategory("UnitTest")]
    public void GetAllCustomers_ReturnsOkResultWithCustomerList()
    {
        // Arrange

        _accountServiceMock.Setup(service => service.GetAllCustomerDetails())
            .Returns(_customers);

        // Act
        var result = _controller.GetAllCustomers().Result;

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = (OkObjectResult)result;
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

        var returnedCustomerList = okResult.Value as IEnumerable<CustomerDetails>;
        Assert.IsNotNull(returnedCustomerList);
        Assert.AreEqual(_customers.Count(), returnedCustomerList.Count());
    }

    #endregion

    #region Create

    [TestMethod, TestCategory("UnitTest")]
    public void CreateCustomerAccount_WithValidRequest_ReturnsOkResult()
    {
        // Arrange
        var createAccountRequest = new CreateAccountRequest
        {
            CustomerId = 1,
            InitialCredit = 22
        };
        var createAccountResponse = new CreateAccountResponse
        {
            MessageToCustomer = CreateAccountMessage.AmountCreditedSuccessfully,

        };

        _accountServiceMock.Setup(service => service.CreateCustomerAccount(createAccountRequest))
            .Returns(createAccountResponse);

        // Act
        var result = _controller.CreateCustomerAccount(createAccountRequest);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = (OkObjectResult)result;
        Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
        Assert.AreEqual(createAccountResponse, okResult.Value);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CreateCustomerAccount_WithErrorResponse_ReturnsNotFoundResult()
    {
        // Arrange
        var createAccountRequest = new CreateAccountRequest
        {
            CustomerId = 1,
            InitialCredit = 233
        };
        var createAccountResponse = new CreateAccountResponse
        {
            MessageToCustomer = CreateAccountMessage.Error,

        };

        _accountServiceMock.Setup(service => service.CreateCustomerAccount(createAccountRequest))
            .Returns(createAccountResponse);

        // Act
        var result = _controller.CreateCustomerAccount(createAccountRequest);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        var notFoundResult = (NotFoundObjectResult)result;
        Assert.AreEqual(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        Assert.AreEqual(createAccountResponse, notFoundResult.Value);
    }

    #endregion

}