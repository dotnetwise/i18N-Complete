using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;




namespace System.ComponentModel.DataAnnotations
{
	/// <summary>The CreditCardAttribute class</summary>
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

		/// <summary>
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:52 GMT"/>
        public override string FormatErrorMessage(string name)
        {
			return CultureInfo.CurrentCulture.Format(this.ErrorMessage, name);
        }

		/// <summary>
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
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