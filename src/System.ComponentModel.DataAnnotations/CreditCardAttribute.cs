using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;




namespace System.ComponentModel.DataAnnotations
{
    /// <summary>A localized version of the CreditCardAttribute</summary>
    /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:51 GMT"/>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CreditCardAttribute
    : DataTypeAttribute
    {
        /// <summary>Creates a new instance of CreditCardAttribute</summary>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:52 GMT"/>
        public CreditCardAttribute()
            : this("The {0} field is not a valid credit card number..")
        {
        }
        /// <summary>Creates a new instance of CreditCardAttribute</summary>
        /// <param name="errorMessage"></param>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:52 GMT"/>
        public CreditCardAttribute(string errorMessage)
            : base("creditcard")
        {
            this.ErrorMessage = errorMessage;
        }

        /// <summary>Applies formatting to an error message, based on the data field where the error occurred.</summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>The localized formatted error message</returns>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:52 GMT"/>
        public override string FormatErrorMessage(string name)
        {
            return CultureInfo.CurrentCulture.Format(this.ErrorMessage, name);
        }

        /// <summary>Determines whether the specified value of the object is valid.</summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>true if the specified value is valid; otherwise, false.</returns>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:52 GMT"/>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var ccValue = value as string;
            if (ccValue == null)
            {
                return false;
            }

            ccValue = ccValue.Replace("-", string.Empty);

            if (string.IsNullOrEmpty(ccValue)) return false; //Don't accept only dashes

            int checksum = 0;
            bool evenDigit = false;

            // http://www.beachnet.com/~hstiles/cardtype.html
            foreach (char digit in ccValue.Reverse())
            {
                if (!Char.IsDigit(digit))
                {
                    return false;
                }

                int digitValue = (digit - '0') * (evenDigit ? 2 : 1);
                evenDigit = !evenDigit;

                while (digitValue > 0)
                {
                    checksum += digitValue % 10;
                    digitValue /= 10;
                }
            }

            return (checksum % 10) == 0;
        }
    }
}