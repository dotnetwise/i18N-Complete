using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;




namespace System.ComponentModel.DataAnnotations
{
	/// <summary>The YearAttribute class</summary>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:53:18 GMT"/>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class YearAttribute 
	: DataTypeAttribute
    {
        private static Regex _regex = new Regex(@"^[0-9]{4}$");

		/// <summary>Gets the Regex</summary>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:53:19 GMT"/>
        public string Regex
        {
            get
            {
                return _regex.ToString();
            }
        }

		/// <summary>Creates a new instance of YearAttribute</summary>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:53:19 GMT"/>
		public YearAttribute()
			: this("The {0} field is not a valid year")
		{

		}
		/// <summary>Creates a new instance of YearAttribute</summary>
		/// <param name="errorMessage"></param>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:53:19 GMT"/>
        public YearAttribute(string errorMessage)
            : base("year")
		{
			this.ErrorMessage = errorMessage;
		}

		/// <summary>
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:53:19 GMT"/>
		public override string FormatErrorMessage(string name)
		{
			return CultureInfo.CurrentCulture.Format(this.ErrorMessage, name);
		}

		/// <summary>
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:53:19 GMT"/>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            int retNum;
            var parseSuccess = int.TryParse(Convert.ToString(value), out retNum);

            return parseSuccess && retNum >= 1 && retNum <= 9999;
        }
    }
}
