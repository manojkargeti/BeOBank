// -----------------------------------------------------------------------
// <copyright file="CreateAccountMessage.cs" company="Eurofins">
//   Copyright 2023 Eurofins Scientific Ltd, Ireland
//   Usage reserved to Eurofins Global Franchise Model subscribers.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.Serialization;

namespace BeOBank.Web.Api.Services.CustomerAccountService.Enums;

/// <summary>
/// Represents messages for the result.
/// </summary>
public enum CreateAccountMessage
{
    /// <summary>
    /// The amount was credited to the account successfully.
    /// </summary>
    [EnumMember(Value = "AmountCreditedSuccessfully")]
    AmountCreditedSuccessfully = 0,

    /// <summary>
    /// The account was created, and the amount was credited successfully.
    /// </summary>
    [EnumMember(Value = "AccountCreatedAndAmountCreditedSuccessfully")]
    AccountCreatedAndAmountCreditedSuccessfully = 1,

    /// <summary>
    /// An error occurred during the account creation process.
    /// </summary>
    [EnumMember(Value = "Error")]
    Error = 2
}
