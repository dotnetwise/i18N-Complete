using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>The CuitAttribute class</summary>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:53:38 GMT"/>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CuitAttribute 
		: DataTypeAttribute
    {
        private static Regex _regex = new Regex(@"^[0-9]{2}-?[0-9]{8}-?[0-9]$");

		/// <summary>Gets the Regex</summary>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:53:39 GMT"/>
        public string Regex
        {
            get
            {
                return _regex.ToString();
            }
        }

		/// <summary>Creates a new instance of CuitAttribute</summary>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:53:39 GMT"/>
		public CuitAttribute()
			: this("The {0} field is not a valid CUIT number.")
		{

		}
		/// <summary>Creates a new instance of CuitAttribute</summary>
		/// <param name="errorMessage"></param>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:53:39 GMT"/>
        public CuitAttribute(string errorMessage)
            : base("cuit")
		{
			this.ErrorMessage = errorMessage;
		}

		/// <summary>
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:53:40 GMT"/>
		public override string FormatErrorMessage(string name)
		{
			return CultureInfo.CurrentCulture.Format(this.ErrorMessage, name);
		}

		/// <summary>
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:53:40 GMT"/>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var valueAsString = value as string;
            return valueAsString != null && _regex.Match(valueAsString).Length > 0;
        }
    }
}
