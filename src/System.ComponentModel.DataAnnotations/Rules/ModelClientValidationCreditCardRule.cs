using System.Web.Mvc;
namespace System.Web.Mvc.ClientValidation.Adapters
{
    /// <summary>A ModelClientValidationRule for the client-side for CreditCardAttribute</summary>
    public class ModelClientValidationCreditCardRule : ModelClientValidationRule
    {
        /// <summary>Creates a new ModelClientValidationCardRule instance</summary>
        /// <param name="errorMessage">The error message</param>
        public ModelClientValidationCreditCardRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "creditcard";
        }
    }
}