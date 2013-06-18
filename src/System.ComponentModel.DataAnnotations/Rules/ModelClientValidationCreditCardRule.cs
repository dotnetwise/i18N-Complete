using System.Web.Mvc;
namespace System.Web.Mvc.ClientValidation.Adapters
{
    public class ModelClientValidationCreditCardRule : ModelClientValidationRule
    {
        public ModelClientValidationCreditCardRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "creditcard";
        }
    }
}