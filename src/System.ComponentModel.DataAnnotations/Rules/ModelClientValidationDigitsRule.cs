using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace System.Web.Mvc.ClientValidation.Rules
{
    public class ModelClientValidationDigitsRule : ModelClientValidationRule
    {
        public ModelClientValidationDigitsRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "digits";
        }
    }
}