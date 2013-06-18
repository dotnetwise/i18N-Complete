using System.Web.Mvc;

namespace System.Web.Mvc.ClientValidation.Rules
{
    public class ModelClientValidationNumericRule : ModelClientValidationRule
    {
        public ModelClientValidationNumericRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "number";
        }
    }
}