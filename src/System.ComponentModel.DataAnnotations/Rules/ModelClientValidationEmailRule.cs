using System.Web.Mvc;

namespace System.Web.Mvc.ClientValidation.Rules
{
    public class ModelClientValidationEmailRule : ModelClientValidationRule
    {
        public ModelClientValidationEmailRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "email";
        }
    }
}