using System.Web.Mvc;

namespace System.Web.Mvc.ClientValidation.Rules
{
    /// <summary>A ModelClientValidationRule for the client-side for EmailAttribute</summary>
    public class ModelClientValidationEmailRule : ModelClientValidationRule
    {
        /// <summary>Creates a new ModelClientValidationEmailRule instance</summary>
        /// <param name="errorMessage">The error message</param>
        public ModelClientValidationEmailRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "email";
        }
    }
}