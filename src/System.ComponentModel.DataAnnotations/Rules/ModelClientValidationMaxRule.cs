using System.Web.Mvc;

namespace System.Web.Mvc.ClientValidation.Rules
{
    /// <summary>A ModelClientValidationRule for the client-side for MaxAttribute</summary>
    public class ModelClientValidationMaxRule : ModelClientValidationRule
    {
        /// <summary>Creates a new ModelClientValidationMaxRule instance</summary>
        /// <param name="errorMessage">The error message</param>
        /// <param name="max">The max value</param>
        public ModelClientValidationMaxRule(string errorMessage, object max)
        {
            ErrorMessage = errorMessage;
            ValidationType = "range";
            ValidationParameters["max"] = max;
        }
    }
}