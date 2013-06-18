using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Mvc.ClientValidation.Rules;

namespace System.Web.Mvc.ClientValidation.Adapters
{
	/// <summary>The DigitsAttributeAdapter class</summary>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:04 GMT"/>
    public class DigitsAttributeAdapter 
	: DataAnnotationsModelValidator<DigitsAttribute>
    {
		/// <summary>Creates a new instance of DigitsAttributeAdapter</summary>
		/// <param name="metadata"></param>
		/// <param name="context"></param>
		/// <param name="attribute"></param>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:04 GMT"/>
        public DigitsAttributeAdapter(ModelMetadata metadata, ControllerContext context, DigitsAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

		/// <summary>
		/// </summary>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:05 GMT"/>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationDigitsRule(ErrorMessage) };
        }
    }

	/// <summary>The IntegerAttributeAdapter class</summary>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:05 GMT"/>
    public class IntegerAttributeAdapter 
	: DataAnnotationsModelValidator<IntegerAttribute>
    {
		/// <summary>Creates a new instance of IntegerAttributeAdapter</summary>
		/// <param name="metadata"></param>
		/// <param name="context"></param>
		/// <param name="attribute"></param>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:05 GMT"/>
        public IntegerAttributeAdapter(ModelMetadata metadata, ControllerContext context, IntegerAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

		/// <summary>
		/// </summary>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:05 GMT"/>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationIntegerRule(ErrorMessage) };
        }
    }
}