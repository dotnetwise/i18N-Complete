using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MinAttribute : DataTypeAttribute
    {
        public object Min { get { return _min; } }

        private readonly double _min;

        public MinAttribute(int min) 
			: this((double)min)
        {
        }

		public MinAttribute(string errorMessage, int min)
			: this(errorMessage, (double)min)
		{

		}

		public MinAttribute(double min)
			: this("The field {0} must be greater than or equal to {1}.", min)
		{

		}
        public MinAttribute(string errorMessage, double min) 
			: base("min")
        {
            _min = min;
			this.ErrorMessage = errorMessage;
		}

		public override string FormatErrorMessage(string name)
		{
			return CultureInfo.CurrentCulture.Format(this.ErrorMessage, name, _min);
		}

        public override bool IsValid(object value)
        {
            if (value == null) return true;
            
            double valueAsDouble;

            var isDouble = double.TryParse(Convert.ToString(value), out valueAsDouble);

            return isDouble && valueAsDouble >= _min;
        }
    }
}
