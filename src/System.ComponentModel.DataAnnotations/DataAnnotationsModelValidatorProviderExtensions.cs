using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Mvc.ClientValidation.Adapters;


namespace System.Web.Mvc
{
	/// <summary>
	/// Registers adapters for 
	/// EmailAttribute
	/// UrlAttribute
	/// CreditCardAttribute
	/// EqualToAttribute
	/// FileExtensionAttribute
	/// NumericAttribute
	/// DigitsAttribute
	/// MinAttribute
	/// MaxAttribute
	/// DateAttribute
	/// IntegerAttribute
	/// CuitAttribute
	/// YearAttribute
	/// </summary>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:21:09 GMT"/>
    public static class DataAnnotationsModelValidatorProviderExtensions
    {
		/// <summary>
		/// </summary>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 13:21:10 GMT"/>
        public static void RegisterValidationExtensions()
        {
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(EmailAttribute), typeof(EmailAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(UrlAttribute), typeof(UrlAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(CreditCardAttribute), typeof(CreditCardAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(EqualToAttribute), typeof(EqualToAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(FileExtensionsAttribute), typeof(FileExtensionsAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(NumericAttribute), typeof(NumericAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(DigitsAttribute), typeof(DigitsAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(MinAttribute), typeof(MinAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(MaxAttribute), typeof(MaxAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(DateAttribute), typeof(DateAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(IntegerAttribute), typeof(IntegerAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(CuitAttribute), typeof(CuitAttributeAdapter));
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(YearAttribute), typeof(YearAttributeAdapter));
        }
    }
}
