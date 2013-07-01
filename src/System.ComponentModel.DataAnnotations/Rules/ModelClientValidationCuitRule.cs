using System.Web.Mvc;

namespace System.Web.Mvc.ClientValidation.Rules
{
    /// <summary>A ModelClientValidationRule for the client-side for CuitAttribute</summary>
    public class ModelClientValidationCuitRule : ModelClientValidationRule
    {
        /// <summary>Creates a new ModelClientValidationCuitRule instance</summary>
        /// <param name="errorMessage">The error message</param>
        /// <param name="regex">A custom regex pattern</param>
        public ModelClientValidationCuitRule(string errorMessage, string regex)
        {
            ErrorMessage = errorMessage;
            ValidationType = "regex";
            ValidationParameters["pattern"] = regex;
        }
    }
}