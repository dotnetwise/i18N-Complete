using System.Web.Mvc;


namespace System.Web.Mvc.ClientValidation.Rules
{
    /// <summary>A ModelClientValidationRule for the client-side for FileAttribute</summary>
    /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:53:56 GMT"/>
    public class ModelClientValidationFileExtensionsRule
        : ModelClientValidationRule
    {
        /// <summary>Creates a new ModelClientValidationFileRule instance</summary>
        /// <param name="errorMessage">The error message</param>
        /// <param name="extensions">Accepted extensions</param>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:53:56 GMT"/>
        public ModelClientValidationFileExtensionsRule(string errorMessage, string extensions)
        {
            ErrorMessage = errorMessage;
            ValidationType = "accept";
            ValidationParameters["exts"] = extensions;
        }
    }
}