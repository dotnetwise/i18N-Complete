using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.ClientValidation.Rules;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc.ClientValidation.Adapters
{
    public class DateAttributeAdapter : DataAnnotationsModelValidator<DateAttribute>
    {
        public DateAttributeAdapter(ModelMetadata metadata, ControllerContext context, DateAttribute attribute)
            : base(metadata, context, attribute)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationDateRule(ErrorMessage) };
        }
    }
}