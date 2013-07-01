using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace System.Web.Mvc.ClientValidation.Adapters
{
    /// <summary>Provides a model EqualToAttribute localizable validator for a specified validation type.</summary>
    public class EqualToAttributeAdapter : DataAnnotationsModelValidator<EqualToAttribute>
    {
        /// <summary>Initializes a new instance of the System.Web.Mvc.DataAnnotationsModelValidator with the EqualToAttribute</summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        /// <param name="attribute">The validation attribute for the model.</param>
        public EqualToAttributeAdapter(ModelMetadata metadata, ControllerContext context, EqualToAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        /// <summary>Gets the validation attribute from the model validator.</summary>
        /// <returns>The validation attribute from the model validator.</returns>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            Attribute.OtherPropertyDisplayName = GetOtherPropertyDisplayName();

            var otherProp = FormatPropertyForClientValidation(Attribute.OtherProperty);
            //We'll just use the built-in System.Web.Mvc client validation rule
            return new[] { new ModelClientValidationEqualToRule(ErrorMessage, otherProp) };
        }

        private string GetOtherPropertyDisplayName()
        {
            if (Metadata.ContainerType != null && !String.IsNullOrEmpty(Attribute.OtherProperty))
            {
                var propertyMetaData = ModelMetadataProviders.Current.GetMetadataForProperty(() => Metadata.Model,
                                                                                             Metadata.ContainerType,
                                                                                             Attribute.OtherProperty);

                return propertyMetaData.GetDisplayName();
            }

            return Attribute.OtherProperty;
        }

        /// <summary>Returns *.property.</summary>
        public static string FormatPropertyForClientValidation(string property)
        {
            if (property == null)
            {
                throw new ArgumentException(CultureInfo.CurrentCulture._("Value cannot be null or empty."), "property");
            }
            return "*." + property;
        }
    }
}