using System.Web.Mvc;

namespace System.Web.Mvc.ClientValidation.Rules
{
    /// <summary>A ModelClientValidationRule for the client-side for DateAttribute</summary>
    public class ModelClientValidationDateRule : ModelClientValidationRule
    {
        /// <summary>Creates a new ModelClientValidationDateRule instance</summary>
        /// <param name="errorMessage">The error message</param>
        public ModelClientValidationDateRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "date";
        }
    }
}