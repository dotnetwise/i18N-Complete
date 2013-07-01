using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>A localized version of the EmailAttribute</summary>
    /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:49:42 GMT"/>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EmailAttribute
        : DataTypeAttribute
    {
        private static Regex _regex = new Regex(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>Creates a new instance of EmailAttribute</summary>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:49:42 GMT"/>
        public EmailAttribute()
            : this("The {0} field is not a valid e-mail address.")
        {

        }
        /// <summary>Creates a new instance of EmailAttribute</summary>
        /// <param name="errorMessage"></param>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:49:43 GMT"/>
        public EmailAttribute(string errorMessage)
            : base(DataType.EmailAddress)
        {
            this.ErrorMessage = errorMessage;
        }

        /// <summary>Applies formatting to an error message, based on the data field where the error occurred.</summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>The localized formatted error message</returns>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:49:43 GMT"/>
        public override string FormatErrorMessage(string name)
        {
            return CultureInfo.CurrentCulture._(this.ErrorMessage, name);
        }
        /// <summary>Determines whether the specified value of the object is valid.</summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>true if the specified value is valid; otherwise, false.</returns>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:49:43 GMT"/>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string valueAsString = value as string;
            return valueAsString != null && _regex.Match(valueAsString).Length > 0;
        }
    }
}
