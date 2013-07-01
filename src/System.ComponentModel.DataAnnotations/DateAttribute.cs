using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>A localized version of the DateAttribute</summary>
    /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:25:17 GMT"/>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DateAttribute
        //: ValidationAttribute
    : DataTypeAttribute //- not inherit from data type as of mvc 3/4 throws error:  Validation type names in unobtrusive client validation rules must be unique. The following validation type was seen more than once: date
    {
        /// <summary>Creates a new instance of DateAttribute</summary>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:25:17 GMT"/>
        public DateAttribute()
            : this("The field {0} is not a valid date.")
        {

        }
        /// <summary>Creates a new instance of DateAttribute</summary>
        /// <param name="errorMessage"></param>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:25:17 GMT"/>
        public DateAttribute(string errorMessage)
            : base(DataType.Date)
        {
            this.ErrorMessage = errorMessage;
        }

        /// <summary>Applies formatting to an error message, based on the data field where the error occurred.</summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>The localized formatted error message</returns>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:25:17 GMT"/>
        public override string FormatErrorMessage(string name)
        {
            return CultureInfo.CurrentCulture._(this.ErrorMessage, name);
        }

        /// <summary>Determines whether the specified value of the object is valid.</summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>true if the specified value is valid; otherwise, false.</returns>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:25:17 GMT"/>
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            DateTime retDate;

            return DateTime.TryParse(Convert.ToString(value), out retDate);
        }
    }
}