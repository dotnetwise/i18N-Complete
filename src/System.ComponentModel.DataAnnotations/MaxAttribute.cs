using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;




namespace System.ComponentModel.DataAnnotations
{
	/// <summary>The MaxAttribute class</summary>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:24:29 GMT"/>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MaxAttribute 
		: DataTypeAttribute
    {
		/// <summary>Gets the Max value as object</summary>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:24:31 GMT"/>
        public object Max { get { return _max; } }

        private readonly double _max;

		/// <summary>Creates a new instance of MaxAttribute</summary>
		/// <param name="max"></param>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:24:30 GMT"/>
		public MaxAttribute(int max)
			: this((double)max)
		{

		}
		/// <summary>Creates a new instance of MaxAttribute</summary>
		/// <param name="errorMessage"></param>
		/// <param name="max"></param>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:24:30 GMT"/>
        public MaxAttribute(string errorMessage, int max)
            : this(errorMessage, (double)max)
        {
		}

		/// <summary>Creates a new instance of MaxAttribute</summary>
		/// <param name="max"></param>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:24:30 GMT"/>
		public MaxAttribute(double max)
			: this("The field {0} must be less than or equal to {1}.", max)
		{

		}
		/// <summary>Creates a new instance of MaxAttribute</summary>
		/// <param name="errorMessage"></param>
		/// <param name="max"></param>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:24:30 GMT"/>
        public MaxAttribute(string errorMessage, double max)
            : base("max")
        {
            _max = max;
			this.ErrorMessage = errorMessage;
        }

		/// <summary>
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:24:31 GMT"/>
		public override string FormatErrorMessage(string name)
		{
			return CultureInfo.CurrentCulture.Format(this.ErrorMessage, name, _max);
		}

		/// <summary>
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:24:32 GMT"/>
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            double valueAsDouble;

            var isDouble = double.TryParse(Convert.ToString(value), out valueAsDouble);

            return isDouble && valueAsDouble <= _max;
        }
    }
	
}