using System.Web.Mvc;

namespace System.Web.Mvc.ClientValidation.Rules
{
    public class ModelClientValidationIntegerRule : ModelClientValidationRule
    {
        private const string Regex = @"^-?\d+$";

        public ModelClientValidationIntegerRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "regex";
            ValidationParameters["pattern"] = Regex;
        }
    }
}