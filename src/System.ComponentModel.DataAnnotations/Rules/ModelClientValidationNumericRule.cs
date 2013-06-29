using System.Web.Mvc;

namespace System.Web.Mvc.ClientValidation.Rules
{
    /// <summary>
    /// ModelClient validation localized version
    /// </summary>
    public class ModelClientValidationNumericRule : ModelClientValidationRule
    {
        /// <summary>
        /// Creates a new ModelClientValidationNumericRule instance
        /// </summary>
        /// <param name="errorMessage"></param>
        public ModelClientValidationNumericRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "number";
        }
    }
}