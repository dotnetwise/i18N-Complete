using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.ClientValidation.Rules;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc.ClientValidation.Adapters
{
    public class MaxAttributeAdapter : DataAnnotationsModelValidator<MaxAttribute>
    {
        public MaxAttributeAdapter(ModelMetadata metadata, ControllerContext context, MaxAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationMaxRule(ErrorMessage, Attribute.Max) };
        }
    }
}