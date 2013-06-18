using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc.ClientValidation.Rules;

namespace System.Web.Mvc.ClientValidation.Adapters
{
    public class FileExtensionsAttributeAdapter : DataAnnotationsModelValidator<FileExtensionsAttribute>
    {
        public FileExtensionsAttributeAdapter(ModelMetadata metadata, ControllerContext context, FileExtensionsAttribute attribute)
            : base(metadata, context, attribute)
        {
            
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationFileExtensionsRule(ErrorMessage, Attribute.Extensions) };
        }
    }
}