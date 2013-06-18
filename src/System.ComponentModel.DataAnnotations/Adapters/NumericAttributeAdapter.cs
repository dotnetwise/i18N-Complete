using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.Mvc.ClientValidation.Rules;




namespace System.Web.Mvc.ClientValidation.Adapters
{
	/// <summary>The NumericAttributeAdapter class</summary>
	/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:54:27 GMT"/>
    public class NumericAttributeAdapter 
	: DataAnnotationsModelValidator<NumericAttribute>
    {
        private static readonly HashSet<Type> NumericTypes = new HashSet<Type>(new Type[] {
            typeof(byte), typeof(sbyte),
            typeof(short), typeof(ushort),
            typeof(int), typeof(uint),
            typeof(long), typeof(ulong),
            typeof(float), typeof(double), typeof(decimal)
        });

		/// <summary>Creates a new instance of NumericAttributeAdapter</summary>
		/// <param name="metadata"></param>
		/// <param name="context"></param>
		/// <param name="attribute"></param>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:54:27 GMT"/>
        public NumericAttributeAdapter(ModelMetadata metadata, ControllerContext context, NumericAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

		/// <summary>
		/// </summary>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:54:27 GMT"/>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            //Don't add a client validator if the datatype is numeric (the mvc framework will handle that for us)
            Type type = Metadata.ModelType;
            if (!IsNumericType(type))
            {
                yield return new ModelClientValidationNumericRule(ErrorMessage);
            }
        }

		/// <summary>
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 24 Feb 2012 15:54:27 GMT"/>
        private static bool IsNumericType(Type type)
        {
            Type underlyingType = Nullable.GetUnderlyingType(type); // strip off the Nullable<>
            return NumericTypes.Contains(underlyingType ?? type);
        }
    }
}