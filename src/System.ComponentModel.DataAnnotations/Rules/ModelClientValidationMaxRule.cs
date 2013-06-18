using System.Web.Mvc;

namespace System.Web.Mvc.ClientValidation.Rules
{
    public class ModelClientValidationMaxRule : ModelClientValidationRule
    {
        public ModelClientValidationMaxRule(string errorMessage, object max)
        {
            ErrorMessage = errorMessage;
            ValidationType = "range";
            ValidationParameters["max"] = max;
        }
    }
}