using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Mvc.ClientValidation.Rules;

namespace System.Web.Mvc.ClientValidation.Adapters
{
    /// <summary>Provides a model DigitsAttribute localizable validator for a specified validation type.</summary>
    /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:04 GMT"/>
    public class DigitsAttributeAdapter
        : DataAnnotationsModelValidator<DigitsAttribute>
    {
        /// <summary>Initializes a new instance of the System.Web.Mvc.DataAnnotationsModelValidator with the DigitsAttribute</summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="attribute">The validation attribute for the model.</param>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:04 GMT"/>
        public DigitsAttributeAdapter(ModelMetadata metadata, ControllerContext context, DigitsAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        /// <summary>Gets the validation attribute from the model validator.</summary>
        /// <returns>The validation attribute from the model validator.</returns>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:05 GMT"/>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationDigitsRule(ErrorMessage) };
        }
    }

    /// <summary>Provides a model IntegerAttribute localizable validator for a specified validation type.</summary>
    /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:05 GMT"/>
    public class IntegerAttributeAdapter
        : DataAnnotationsModelValidator<IntegerAttribute>
    {
        /// <summary>Initializes a new instance of the System.Web.Mvc.DataAnnotationsModelValidator with the DigitsAttribute</summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="attribute">The validation attribute for the model.</param>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:05 GMT"/>
        public IntegerAttributeAdapter(ModelMetadata metadata, ControllerContext context, IntegerAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        /// <summary>Gets the validation attribute from the model validator.</summary>
        /// <returns>The validation attribute from the model validator.</returns>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:05 GMT"/>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationIntegerRule(ErrorMessage) };
        }
    }
}