using System.Runtime.Serialization;

namespace BeOBank.Web.Models.Enums;
 
public enum CreateAccountMessage
{
    
    [EnumMember(Value = "AmountCreditedSuccessfully")]
    AmountCreditedSuccessfully = 0,
     
    [EnumMember(Value = "AccountCreatedAndAmountCreditedSuccessfully")]
    AccountCreatedAndAmountCreditedSuccessfully = 1,

    [EnumMember(Value = "Error")]
    Error = 2
}
