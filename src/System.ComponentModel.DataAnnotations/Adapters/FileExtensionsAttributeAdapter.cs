using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc.ClientValidation.Rules;

namespace System.Web.Mvc.ClientValidation.Adapters
{
    /// <summary>
    ///  Provides a model validator for a specified validation file extensions type.
    /// </summary>
    public class FileExtensionsAttributeAdapter : DataAnnotationsModelValidator<FileExtensionsAttribute>
    {
        /// <summary>
        /// Creates a new FileExtensionsAttributeAdapter
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="context"></param>
        /// <param name="attribute"></param>
        public FileExtensionsAttributeAdapter(ModelMetadata metadata, ControllerContext context, FileExtensionsAttribute attribute)
            : base(metadata, context, attribute)
        {

        }
        /// <summary>
        /// Returns the client validation rules
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationFileExtensionsRule(ErrorMessage, Attribute.Extensions) };
        }
    }
}