using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;



namespace System.ComponentModel.DataAnnotations
{
    /// <summary>A localized version of the DigitsAttribute</summary>
    /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:26:54 GMT"/>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DigitsAttribute
    : DataTypeAttribute
    {
        /// <summary>Creates a new instance of DigitsAttribute</summary>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:26:54 GMT"/>
        public DigitsAttribute()
            : this("The field {0} should contain only digits.")
        {

        }
        /// <summary>Creates a new instance of DigitsAttribute</summary>
        /// <param name="errorMessage"></param>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:26:54 GMT"/>
        public DigitsAttribute(string errorMessage)
            : base("digits")
        {
            this.ErrorMessage = errorMessage;
        }

        /// <summary>Applies formatting to an error message, based on the data field where the error occurred.</summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>The localized formatted error message</returns>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:26:54 GMT"/>
        public override string FormatErrorMessage(string name)
        {
            return CultureInfo.CurrentCulture.Format(this.ErrorMessage, name);
        }

        /// <summary>Determines whether the specified value of the object is valid.</summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>true if the specified value is valid; otherwise, false.</returns>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:26:54 GMT"/>
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            long retNum;

            var parseSuccess = long.TryParse(Convert.ToString(value), out retNum);

            return parseSuccess && retNum >= 0;
        }
    }
}