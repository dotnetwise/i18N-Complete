using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>A localized version of the IntegerAttribute</summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IntegerAttribute : DataTypeAttribute
    {
        /// <summary>
        /// The default message is "The field {0} should be a positive or negative non-decimal number."
        /// </summary>
        public IntegerAttribute()
            : this("The field {0} should be a positive or negative non-decimal number.")
        {

        }
        /// <summary>
        /// Speicify a custom localizable error message
        /// </summary>
        /// <param name="errorMessage"></param>
        public IntegerAttribute(string errorMessage)
            : base("integer")
        {
            this.ErrorMessage = errorMessage;
        }

        /// <summary>Applies formatting to an error message, based on the data field where the error occurred.</summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>The localized formatted error message</returns>
        public override string FormatErrorMessage(string name)
        {
            return CultureInfo.CurrentCulture.Format(this.ErrorMessage, name);
        }

        /// <summary>Determines whether the specified value of the object is valid.</summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>true if the specified value is valid; otherwise, false.</returns>
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            int retNum;

            return int.TryParse(Convert.ToString(value), out retNum);
        }
    }
}