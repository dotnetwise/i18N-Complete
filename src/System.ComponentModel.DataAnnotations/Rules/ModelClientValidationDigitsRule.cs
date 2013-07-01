using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace System.Web.Mvc.ClientValidation.Rules
{
    /// <summary>A ModelClientValidationRule for the client-side for DigitsAttribute</summary>
    public class ModelClientValidationDigitsRule : ModelClientValidationRule
    {
        /// <summary>Creates a new ModelClientValidationDigitsRule instance</summary>
        /// <param name="errorMessage">The error message</param>
        public ModelClientValidationDigitsRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "digits";
        }
    }
}