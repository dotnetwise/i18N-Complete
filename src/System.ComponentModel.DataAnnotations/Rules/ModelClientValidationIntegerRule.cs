using System.Web.Mvc;

namespace System.Web.Mvc.ClientValidation.Rules
{
    /// <summary>A ModelClientValidationRule for the client-side for IntegerAttribute</summary>
    public class ModelClientValidationIntegerRule : ModelClientValidationRule
    {
        private const string Regex = @"^-?\d+$";
        /// <summary>Creates a new ModelClientValidationIntegerRule instance</summary>
        /// <param name="errorMessage">The error message</param>
        public ModelClientValidationIntegerRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "regex";
            ValidationParameters["pattern"] = Regex;
        }
    }
}