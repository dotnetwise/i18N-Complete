﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
namespace System.Globalization
{
	/// <summary>The MvcConfig class</summary>
	/// <created author="laurentiu.macovei" date="Thu, 05 Jan 2012 21:55:41 GMT"/>
	public class LocalizationAppConfig
	{

		#region Constructors

		/// <summary>Static constructor of MvcConfig</summary>
		/// <created author="laurentiu.macovei" date="Thu, 05 Jan 2012 21:55:41 GMT"/>
		static LocalizationAppConfig()
		{
            var app = ConfigurationManager.AppSettings;
            SupportedLanguages = (app["SupportedLanguages"] ?? string.Empty).Split(new[] { '\n', ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(d => d.Trim())
                .Where(d => !string.IsNullOrEmpty(d))
                .ToArray();
            SupportedLanguages = SupportedLanguages.Contains("*") ? SupportedLanguages.Take(0).ToArray() : SupportedLanguages;
            LocalizationLoadComments = IsTrue(app["LocalizationLoadComments"]);
        }

        /// <summary>Returns true if the value is 1 or true, or default value if null or string.Empty, otherwise false</summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Tue, 24 Jul 2007 16:33:34 GMT"/>
        public static bool IsTrue(string value, bool defaultValue = false) {
            if (string.IsNullOrEmpty(value))
                return defaultValue;
            return value == "1" || "true".Equals(value, StringComparison.OrdinalIgnoreCase)
                || "!false".Equals(value, StringComparison.OrdinalIgnoreCase);
        }

		#endregion Constructors


		#region Properties
        public static readonly string[] SupportedLanguages = new string[0];

        public static bool LocalizationLoadComments { get; set; }

		#endregion Properties


    }
}
