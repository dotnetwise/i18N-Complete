using System.Web.Mvc;

namespace System.Web.Mvc.ClientValidation.Rules
{
    public class ModelClientValidationDateRule : ModelClientValidationRule
    {
        public ModelClientValidationDateRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "date";
        }
    }
}