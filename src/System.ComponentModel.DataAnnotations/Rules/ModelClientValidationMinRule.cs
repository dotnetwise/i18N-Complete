using System.Web.Mvc;


namespace System.Web.Mvc.ClientValidation.Rules
{
	/// <summary>The ModelClientValidationMinRule class</summary>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:42 GMT"/>
    public class ModelClientValidationMinRule 
	: ModelClientValidationRule
    {
		/// <summary>Creates a new instance of ModelClientValidationMinRule</summary>
		/// <param name="errorMessage"></param>
		/// <param name="min"></param>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:50:42 GMT"/>
        public ModelClientValidationMinRule(string errorMessage, object min)
        {
            ErrorMessage = errorMessage;
            ValidationType = "range";
            ValidationParameters["min"] = min;
        }
    }
}