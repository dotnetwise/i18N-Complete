using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace System.Web.Mvc.ClientValidation.Adapters
{
	/// <summary>The CreditCardAttributeAdapter class</summary>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:33 GMT"/>
    public class CreditCardAttributeAdapter 
	: DataAnnotationsModelValidator<CreditCardAttribute>
    {
		/// <summary>Creates a new instance of CreditCardAttributeAdapter</summary>
		/// <param name="metadata"></param>
		/// <param name="context"></param>
		/// <param name="attribute"></param>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:33 GMT"/>
        public CreditCardAttributeAdapter(ModelMetadata metadata, ControllerContext context, CreditCardAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

		/// <summary>
		/// </summary>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:52:33 GMT"/>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationCreditCardRule(ErrorMessage) };
        }
    }
}