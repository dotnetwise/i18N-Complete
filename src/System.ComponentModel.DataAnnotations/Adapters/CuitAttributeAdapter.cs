using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.ClientValidation.Rules;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc.ClientValidation.Adapters
{
    public class CuitAttributeAdapter : DataAnnotationsModelValidator<CuitAttribute>
    {
        public CuitAttributeAdapter(ModelMetadata metadata, ControllerContext context, CuitAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationCuitRule(ErrorMessage, Attribute.Regex) };
        }
    }
}