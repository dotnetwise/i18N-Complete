using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>Specifies the min value of an associated input for this field</summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MinAttribute : DataTypeAttribute
    {
        /// <summary>The minimum value</summary>
        public object Min { get { return _min; } }

        private readonly double _min;
        /// <summary>Creates a new MinAttribute with the default error message "The field {0} must be greater than or equal to {1}."</summary>
        /// <param name="min">The minimum value as integer</param>
        public MinAttribute(int min)
            : this((double)min)
        {
        }

        /// <summary>Creates a new MinAttribute with a custom localizable error message</summary>
        /// <param name="min">The minimum value as integer</param>
        /// <param name="errorMessage">The custom erro message</param>
        public MinAttribute(string errorMessage, int min)
            : this(errorMessage, (double)min)
        {

        }
        /// <summary>Creates a new MinAttribute with the default error message "The field {0} must be greater than or equal to {1}."</summary>
        /// <param name="min">The minimum value as double</param>
        public MinAttribute(double min)
            : this("The field {0} must be greater than or equal to {1}.", min)
        {

        }
        /// <summary>Creates a new MinAttribute with a custom localizable error message</summary>
        /// <param name="min">The minimum value as double</param>
        /// <param name="errorMessage">The custom erro message</param>
        public MinAttribute(string errorMessage, double min)
            : base("min")
        {
            _min = min;
            this.ErrorMessage = errorMessage;
        }
        /// <summary>Applies formatting to an error message, based on the data field where the error occurred.</summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>The localized formatted error message</returns>
        public override string FormatErrorMessage(string name)
        {
            return CultureInfo.CurrentCulture.Format(this.ErrorMessage, name, _min);
        }
        /// <summary>Determines whether the specified value of the object is valid.</summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>true if the specified value is valid; otherwise, false.</returns>
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            double valueAsDouble;

            var isDouble = double.TryParse(Convert.ToString(value), out valueAsDouble);

            return isDouble && valueAsDouble >= _min;
        }
    }
}
