using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>A localized version of the FileExtensionsAttribute</summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FileExtensionsAttribute : DataTypeAttribute
    {
        /// <summary>Gets the Allowed extensions</summary>
        public string Extensions { get; private set; }

        /// <summary>Provide the allowed file extensions, seperated via "|" (or a comma, ","), defaults to "png|jpe?g|gif" with the defaul error message "The {0} field only accepts files with the following extensions: {1} </summary>
        /// <param name="allowedExtensions">Specify the allowe dextensions separated by | or comma ,</param>
        public FileExtensionsAttribute(string allowedExtensions = "png,jpg,jpeg,gif")
            : this("The {0} field only accepts files with the following extensions: {1}.", allowedExtensions)
        {

        }
        /// <summary>Provide the allowed file extensions, seperated via "|" (or a comma, ","), defaults to "png|jpe?g|gif" </summary>
        /// <param name="errorMessage">Specify a custom error message to be translated</param>
        /// <param name="allowedExtensions">Specify the allowe dextensions separated by | or comma ,</param>
        public FileExtensionsAttribute(string errorMessage, string allowedExtensions)
            : base("fileextension")
        {
            Extensions = string.IsNullOrWhiteSpace(allowedExtensions) ? "png,jpg,jpeg,gif" : allowedExtensions.Replace("|", ",").Replace(" ", "");
            this.ErrorMessage = errorMessage;
        }

        /// <summary>Applies formatting to an error message, based on the data field where the error occurred.</summary>
        /// <param name="name">The name to include in the formatted message.</param>
        /// <returns>The localized formatted error message</returns>
        public override string FormatErrorMessage(string name)
        {
            return CultureInfo.CurrentCulture.Format(this.ErrorMessage, name, Extensions);
        }
        /// <summary>Determines whether the specified value of the object is valid.</summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>true if the specified value is valid; otherwise, false.</returns>
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
                return Extensions.Split(',').Contains(Path.GetExtension(fileName).Replace(".", "").ToLowerInvariant());
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
    }
}