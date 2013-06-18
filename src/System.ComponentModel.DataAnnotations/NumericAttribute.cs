using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NumericAttribute : DataTypeAttribute
    {
		public NumericAttribute()
			: this("The {0} field is not a valid number.")
		{

		}
        public NumericAttribute(string errorMessage) 
			: base("numeric")
		{
			this.ErrorMessage = errorMessage;
		}

        public override string FormatErrorMessage(string name)
        {
			return CultureInfo.CurrentCulture.Format(this.ErrorMessage, name);
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            double retNum;

            return double.TryParse(Convert.ToString(value), out retNum);
        }
    }
}
