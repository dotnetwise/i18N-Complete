using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc.ClientValidation.Adapters
{
    public class YearAttributeAdapter : DataAnnotationsModelValidator<YearAttribute>
    {
        public YearAttributeAdapter(ModelMetadata metadata, ControllerContext context, YearAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationRegexRule(ErrorMessage, Attribute.Regex) };
        }
    }
}