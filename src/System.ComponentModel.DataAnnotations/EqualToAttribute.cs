using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;






namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Validates that the property has the same value as the given 'otherProperty' 
    /// </summary>
    /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:49:30 GMT"/>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EqualToAttribute
    : ValidationAttribute
    {
        /// <summary>Creates a new instance of EqualToAttribute</summary>
        /// <param name="otherProperty"></param>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:49:30 GMT"/>
        public EqualToAttribute(string otherProperty)
            : this("'{0}' and '{1}' do not match.", otherProperty)
        {

        }
        /// <summary>Creates a new instance of EqualToAttribute</summary>
        /// <param name="errorMessage"></param>
        /// <param name="otherProperty"></param>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:49:30 GMT"/>
        public EqualToAttribute(string errorMessage, string otherProperty)
        {
            if (otherProperty == null)
            {
                throw new ArgumentNullException("otherProperty");
            }
            OtherProperty = otherProperty;
            OtherPropertyDisplayName = null;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>Gets the OtherProperty</summary>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:49:30 GMT"/>
        public string OtherProperty { get; private set; }

        /// <summary>Gets or sets the OtherPropertyDisplayName</summary>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:49:31 GMT"/>
        public string OtherPropertyDisplayName { get; set; }


        /// <summary>Applies formatting to an error message, based on the data field where the error occurred.</summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>The localized formatted error message</returns>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:49:31 GMT"/>
        public override string FormatErrorMessage(string name)
        {
            var otherPropertyDisplayName = OtherPropertyDisplayName ?? OtherProperty;
            return CultureInfo.CurrentCulture.Format(this.ErrorMessage, name, otherPropertyDisplayName);
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:49:31 GMT"/>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var memberNames = new[] { validationContext.MemberName };

            PropertyInfo otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
            if (otherPropertyInfo == null)
            {
                return new ValidationResult(CultureInfo.CurrentCulture.Format("Could not find a property named {0}.", OtherProperty), memberNames);
            }

            var displayAttribute =
                otherPropertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;

            if (displayAttribute != null && !string.IsNullOrWhiteSpace(displayAttribute.Name))
            {
                OtherPropertyDisplayName = displayAttribute.Name;
            }

            object otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
            if (!Equals(value, otherPropertyValue))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), memberNames);
            }
            return null;
        }
    }
}