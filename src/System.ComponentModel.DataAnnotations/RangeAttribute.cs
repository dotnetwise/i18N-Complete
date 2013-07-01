using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Collections.Generic;




namespace System.ComponentModel.DataAnnotations
{
    /// <summary>A localized version of the NumericRangeAttribute</summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NumericRangeAttribute
        : RangeAttribute, IClientValidatable
    {
        /// <summary>Returns the ModelClientValidationRangeRule as the client validation rules</summary>
        /// <param name="metadata">The metadata for the model.</param>
        /// <param name="context">The controller context for the model.</param>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRangeRule(ErrorMessage, Minimum, Maximum);
            yield return rule;
        }

        /// <summary>Initializes a new I18N localizable instance of the <see cref="System.ComponentModel.DataAnnotations.RangeAttribute"/> class by using the specified minimum and maximum values.</summary>
        /// <param name="min">Specifies the minimum value allowed for the data field value.</param>
        /// <param name="max">Specifies the maximum value allowed for the data field value.</param>
        public NumericRangeAttribute(int min, int max)
            : base((double)min, (double)max)
        {

        }
        /// <summary>Initializes a new I18N localizable instance of the <see cref="System.ComponentModel.DataAnnotations.RangeAttribute"/> class by using the specified minimum and maximum values.</summary>
        /// <param name="errorMessage"></param>
        /// <param name="min">Specifies the minimum value allowed for the data field value.</param>
        /// <param name="max">Specifies the maximum value allowed for the data field value.</param>
        public NumericRangeAttribute(string errorMessage, int min, int max)
            : this(errorMessage, (double)min, (double)max)
        {
        }

        /// <summary>Creates a new instance of MaxAttribute</summary>
        /// <param name="min">Specifies the minimum value allowed for the data field value.</param>
        /// <param name="max">Specifies the maximum value allowed for the data field value.</param>
        public NumericRangeAttribute(double min, double max)
            : this("The field {0} must be between {1} and {2}.", min, max)
        {

        }
        /// <summary>Creates a new instance of MaxAttribute</summary>
        /// <param name="errorMessage"></param>
        /// <param name="min">Specifies the minimum value allowed for the data field value.</param>
        /// <param name="max">Specifies the maximum value allowed for the data field value.</param>
        public NumericRangeAttribute(string errorMessage, double min, double max)
            : base(min, max)
        {
            this.ErrorMessage = errorMessage;
        }

        /// <summary>Applies formatting to an error message, based on the data field where the error occurred.</summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>The localized formatted error message</returns>
        public override string FormatErrorMessage(string name)
        {
            return CultureInfo.CurrentCulture.Format(this.ErrorMessage, name, Minimum, Maximum);
        }

    }
}