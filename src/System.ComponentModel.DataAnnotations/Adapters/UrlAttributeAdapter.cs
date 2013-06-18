using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc.ClientValidation.Adapters
{
    public class UrlAttributeAdapter : DataAnnotationsModelValidator<UrlAttribute>
    {
        public UrlAttributeAdapter(ModelMetadata metadata, ControllerContext context, UrlAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] {new ModelClientValidationRegexRule(ErrorMessage, Attribute.Regex)};
        }
    }
}
