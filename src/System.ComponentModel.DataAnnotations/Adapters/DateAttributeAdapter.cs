using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.ClientValidation.Rules;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc.ClientValidation.Adapters
{
    /// <summary>Provides a model DateAttributeAdapter localizable validator for a specified validation type.</summary>
    public class DateAttributeAdapter : DataAnnotationsModelValidator<DateAttribute>
    {
        /// <summary>Initializes a new instance of the System.Web.Mvc.DataAnnotationsModelValidator with the DateAttributeAdapter</summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="attribute">The validation attribute for the model.</param>
        public DateAttributeAdapter(ModelMetadata metadata, ControllerContext context, DateAttribute attribute)
            : base(metadata, context, attribute)
        {
        }
        /// <summary>Gets the validation attribute from the model validator.</summary>
        /// <returns>The validation attribute from the model validator.</returns>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationDateRule(ErrorMessage) };
        }
    }
}