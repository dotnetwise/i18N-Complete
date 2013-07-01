using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc.ClientValidation.Adapters
{
    /// <summary>Provides a model YearAttribute localizable validator for a specified validation type.</summary>
    public class YearAttributeAdapter : DataAnnotationsModelValidator<YearAttribute>
    {
        /// <summary>Initializes a new instance of the System.Web.Mvc.DataAnnotationsModelValidator with the YearAttribute</summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="attribute">The validation attribute for the model.</param>
        public YearAttributeAdapter(ModelMetadata metadata, ControllerContext context, YearAttribute attribute)
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