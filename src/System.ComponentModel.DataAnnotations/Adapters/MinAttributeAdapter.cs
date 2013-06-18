using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.ClientValidation.Rules;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc.ClientValidation.Adapters
{
    public class MinAttributeAdapter : DataAnnotationsModelValidator<MinAttribute>
    {
        public MinAttributeAdapter(ModelMetadata metadata, ControllerContext context, MinAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationMinRule(ErrorMessage, Attribute.Min) };
        }
    }
}