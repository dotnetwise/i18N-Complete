using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc.ClientValidation.Adapters
{
    /// <summary>Provides a model UrlAttribute localizable validator for a specified validation type.</summary>
    public class UrlAttributeAdapter : DataAnnotationsModelValidator<UrlAttribute>
    {
        /// <summary>Initializes a new instance of the System.Web.Mvc.DataAnnotationsModelValidator with the UrlAttribute</summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="attribute">The validation attribute for the model.</param>
        public UrlAttributeAdapter(ModelMetadata metadata, ControllerContext context, UrlAttribute attribute)
            : base(metadata, context, attribute)
        {
        }
        /// <summary>Gets the validation attribute from the model validator.</summary>
        /// <returns>The validation attribute from the model validator.</returns>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationRegexRule(ErrorMessage, Attribute.Regex) };
        }
    }
}
