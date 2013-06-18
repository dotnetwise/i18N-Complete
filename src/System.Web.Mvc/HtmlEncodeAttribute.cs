using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Web.Mvc
{
	/// <summary>Specify whether a property should be automatically html-encoded to prevent XSS</summary>
	/// <created author="laurentiu.macovei" date="Mon, 11 Apr 2011 23:44:22 GMT"/>
	[AttributeUsage(AttributeTargets.Property)]
	public class HtmlEncodeAttribute
		: Attribute
	{

		#region Constructors

		/// <summary>Creates a new instance of HtmlEncodeAttribute</summary>
		/// <param name="encode"></param>
		/// <created author="laurentiu.macovei" date="Mon, 11 Apr 2011 23:44:22 GMT"/>
		public HtmlEncodeAttribute(bool encode = true)
		{
			this.Encode = encode;
		}


		#endregion Constructors


		#region Properties

		/// <summary>
		/// Specify true to automatically encode this property, or false to keep the text the user entered.
		/// <para>Warning! Letting the text un-encoded might be a security issue such as XSS, code injection etc!</para></summary>
		/// <created author="laurentiu.macovei" date="Mon, 11 Apr 2011 23:44:22 GMT"/>
		public bool Encode { get; set; }


		#endregion Properties

	}
}
