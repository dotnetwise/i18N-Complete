using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Collections.Generic;




namespace System.ComponentModel.DataAnnotations
{
	/// <summary>The MaxAttribute class</summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NumericRangeAttribute
		: RangeAttribute, IClientValidatable
    {
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

		/// <summary>
		/// Formats the error message that is displayed when range validation fails.
		/// </summary>
		/// <param name="name"></param>
		public override string FormatErrorMessage(string name)
		{
			return CultureInfo.CurrentCulture.Format(this.ErrorMessage, name, Minimum, Maximum);
		}

    }
}