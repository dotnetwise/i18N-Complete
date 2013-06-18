using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IntegerAttribute : DataTypeAttribute
    {
		public IntegerAttribute()
			: this("The field {0} should be a positive or negative non-decimal number.")
		{

		}
        public IntegerAttribute(string errorMessage)
            : base("integer")
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

            int retNum;

            return int.TryParse(Convert.ToString(value), out retNum);
        }
    }
}