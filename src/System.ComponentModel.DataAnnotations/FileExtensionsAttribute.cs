using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;

namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FileExtensionsAttribute : DataTypeAttribute
    {
        public string Extensions { get; private set; }

        public FileExtensionsAttribute(string allowedExtensions = "png,jpg,jpeg,gif")
			: this("The {0} field only accepts files with the following extensions: {1}.", allowedExtensions)
		{

		}
        /// <summary>
        /// Provide the allowed file extensions, seperated via "|" (or a comma, ","), defaults to "png|jpe?g|gif" 
        /// </summary>
        public FileExtensionsAttribute(string errorMessage, string allowedExtensions)
            : base("fileextension")
        {
            Extensions = string.IsNullOrWhiteSpace(allowedExtensions) ? "png,jpg,jpeg,gif" : allowedExtensions.Replace("|", ",").Replace(" ", "");
			this.ErrorMessage = errorMessage;
		}

		public override string FormatErrorMessage(string name)
		{
			return CultureInfo.CurrentCulture.Format(this.ErrorMessage, name, Extensions);
		}

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            
            string valueAsString = value as string;
            if (valueAsString != null)
            {
                return ValidateExtension(valueAsString);
            }

            return false;
        }

        private bool ValidateExtension(string fileName)
        {
            try
            {
                return Extensions.Split(',').Contains(Path.GetExtension(fileName).Replace(".","").ToLowerInvariant());
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}