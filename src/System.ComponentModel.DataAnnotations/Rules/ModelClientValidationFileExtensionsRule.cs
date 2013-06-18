using System.Web.Mvc;


namespace System.Web.Mvc.ClientValidation.Rules
{
	/// <summary>The ModelClientValidationFileExtensionsRule class</summary>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:53:56 GMT"/>
    public class ModelClientValidationFileExtensionsRule 
	: ModelClientValidationRule
    {
		/// <summary>Creates a new instance of ModelClientValidationFileExtensionsRule</summary>
		/// <param name="errorMessage"></param>
		/// <param name="extensions"></param>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:53:56 GMT"/>
        public ModelClientValidationFileExtensionsRule(string errorMessage, string extensions)
        {
            ErrorMessage = errorMessage;
            ValidationType = "accept";
            ValidationParameters["exts"] = extensions;
        }
    }
}