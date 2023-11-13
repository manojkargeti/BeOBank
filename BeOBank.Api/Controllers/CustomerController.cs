using System.ComponentModel;
using BeOBank.Web.Api.Models;
using BeOBank.Web.Api.Services.CustomerAccountService;
using BeOBank.Web.Api.Services.CustomerAccountService.Enums;
using BeOBank.Web.Api.Services.CustomerAccountService.ServiceModels;
using Microsoft.AspNetCore.Mvc;

namespace BeOBank.Web.Api.Controllers;

/// <summary>
/// API Controller for Customer.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CustomerController : Controller
{
    private readonly ICustomerAccountService _accountService;

    /// <inheritdoc />
    public CustomerController(ICustomerAccountService userAccountService)
    {
        _accountService = userAccountService;
    }

    /// <summary>
    /// Get customer account details by ID.
    /// </summary>
    /// <param name="id">The ID of the customer.</param>
    /// <returns>The response containing the details of the customer account.</returns>
    [HttpGet("GetCustomerById/{id:int}", Name = "GetCustomerById")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerDetails))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomerDetails))]
    public ActionResult GetCustomerById(int id)
    {
        if (id <= 0)
            return BadRequest("Bad Request");

        var result = _accountService.GetCustomerById(id);

        if (result is not null)
            return Ok(result);

        return NotFound("AccountHolder Not found");
    }

    /// <summary>
    /// Get all customer account details.
    /// </summary>
    /// <returns>The response containing the details of all customer accounts.</returns>
    [HttpGet("GetAllCustomersDetails", Name = "GetAllCustomers")]
    [ProducesResponseType(StatusCodes.Status200OK,
        Type = typeof(IList<CustomerDetails>))] // the response for a successful GetAllUsers
    public ActionResult<IList<CustomerDetails>> GetAllCustomers()
    {
        return Ok(_accountService.GetAllCustomerDetails());
    }
    


    /// <summary>
    /// Create a customer account.
    /// </summary>
    /// <param name="request">The request to create a customer account or update value in existing.</param>
    /// <returns>The response indicating the result of the account creation or updation.</returns>
    [HttpPost("CreateAccount", Name = "CreateCustomerAccount")]
    [ProducesResponseType(StatusCodes.Status200OK,
        Type = typeof(CreateAccountResponse))] // the response for a successful CreatAccount
    [ProducesResponseType(StatusCodes.Status404NotFound,
        Type = typeof(CreateAccountResponse))] // the response for NotFound
    [ProducesResponseType(StatusCodes.Status400BadRequest,
        Type = typeof(CreateAccountResponse))] // the response for BadRequest
    [Description("CreateAccount response enum.")]
    public ActionResult CreateCustomerAccount(CreateAccountRequest request)
    {
        var response = _accountService.CreateCustomerAccount(request);

        switch (response.MessageToCustomer)
        {
            case CreateAccountMessage.AmountCreditedSuccessfully:
                return Ok(response);
            case CreateAccountMessage.AccountCreatedAndAmountCreditedSuccessfully:
                return Ok(response);
            case CreateAccountMessage.Error:
                return NotFound(response); //return a 404 status for the "Customer does not exist" case
            default:
                return BadRequest(response); //any other cases
        }
    }
}