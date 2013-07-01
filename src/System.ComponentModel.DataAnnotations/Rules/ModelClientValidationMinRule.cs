using System.Web.Mvc;


namespace System.Web.Mvc.ClientValidation.Rules
{
    /// <summary>A ModelClientValidationRule for the client-side for MinAttribute</summary>
    /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:42 GMT"/>
    public class ModelClientValidationMinRule
    : ModelClientValidationRule
    {
        /// <summary>Creates a new ModelClientValidationMinRule instance</summary>
        /// <param name="errorMessage">The error message</param>
        /// <param name="min">The min value</param>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:42 GMT"/>
        public ModelClientValidationMinRule(string errorMessage, object min)
        {
            ErrorMessage = errorMessage;
            ValidationType = "range";
            ValidationParameters["min"] = min;
        }
    }
}