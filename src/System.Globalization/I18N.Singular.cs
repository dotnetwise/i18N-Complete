using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.WebPages;
using System.Web.Mvc;
using System.Web;

namespace System.Globalization
{

	/// <summary>A transparent localizable class which represents the properties and methods that are needed in order to render a view that uses ASP.NET Razor syntax.</summary>
	/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 23:28:10 GMT"/>
	public static partial class I18N
	{
		/// <summary>
		/// 	<para>@Alias <c>GetString</c> and <c>Format</c></para>
		/// 	<para>Translates the given text applying string.Format(text, arguments) to the current culture language. </para>
		/// 	<para>The text and argument values will be HTML Encoded when used in ASP.NET MVC</para>
		/// </summary>
		/// <param name="culture">The culture being extended</param>
		/// <param name="text">The text to be translated</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
		/// <returns>The translated formatted text as string</returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 23:47:10 GMT"/>
		public static string _(this CultureInfo culture, string text,params object[] arguments)
		{
			var result = string.Format(I18NComplete.GetText(text, lcid: culture.LCID), arguments);
            return
#if DEBUG
 I18NComplete.OnGetting_(result, culture, text, arguments) ??
#endif
 result;
		}
		/// <summary>
		/// 	<para>@Alias <c>_</c> and <c>Format</c></para>
		/// 	<para>Translates the given text applying string.Format(text, arguments) to the current culture language. </para>
		/// 	<para>The text and argument values will be HTML Encoded when used in ASP.NET MVC</para>
		/// </summary>
		/// <param name="culture">The culture being extended</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
		/// <param name="text">The text to be translated</param>
		/// <returns>The translated formatted text as string</returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 23:47:15 GMT"/>
		public static string GetString(this CultureInfo culture, string text,params object[] arguments)
		{
			return string.Format(I18NComplete.GetText(text, lcid: culture.LCID), arguments);
		}
		/// <summary>
		/// 	<para>@Alias <c>_</c> and <c>GetString</c></para>
		/// 	<para>Translates the given text applying string.Format(text, arguments) to the current culture language. </para>
		/// 	<para>The text and argument values will be HTML Encoded when used in ASP.NET MVC</para>
		/// </summary>
		/// <param name="culture">The culture being extended</param>
		/// <param name="text">The text to be translated</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
		/// <returns>The translated formatted text as string</returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 23:47:20 GMT"/>
		public static string Format(this CultureInfo culture, string text,params object[] arguments)
		{
			return string.Format(I18NComplete.GetText(text, lcid: culture.LCID), arguments);
		}

		/// <summary>
		/// 	<para>Quotes the text as the xgettext cannot correctly extract values from attributes</para>
		/// 	<para>Translates the given html applying '"'+HttpUtility.HtmlAttributeEncode(string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))))+'"' to the current culture language. </para>
		/// 	<para>The html will be kept as it is, while arguments will be automatically HTML Encoded</para>
		/// 	<para>Usage: &lt;input type="submit" value=@__q("Save") /&gt;  -- Note the missing quotes!</para>
		/// </summary>
		/// <param name="culture">The culture being extended</param>
		/// <param name="html">The html text to be translated</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Fri, 13 Jan 2012 03:55:43 GMT"/>
		public static MvcHtmlString __q(this CultureInfo culture, string html, params object[] arguments)
		{
			var result = new MvcHtmlString('"' + HttpUtility.HtmlAttributeEncode(string.Format(I18NComplete.GetText(html, lcid: culture.LCID), arguments.Select(a => HttpUtility.HtmlEncode(a)).ToArray())) + '"');
            return
#if DEBUG
 I18NComplete.OnGetting__q(result, culture, html, arguments) ??
#endif
 result;

		}
		/// <summary>
		/// 	<para>Quotes the text as the xgettext cannot correctly extract values from attributes</para>
		/// 	<para>Translates the given html applying '"'+HttpUtility.HtmlAttributeEncode(string.Format(html, htmlArguments))+'"' to the current culture language. </para>
		/// 	<para>Warning! Neither the html nor the htmlArguments will be encoded whatsoever</para>
		/// 	<para>Usage: &lt;div title=@___q("&lt; class='tip'&gt;There are {0} {1} in cart.&lt;/div&gt;", 3, "&lt;b&gt;Items&lt;/b&gt;")&gt;...  -- Note the missing quotes!</para>
		/// </summary>
		/// <param name="culture">The culture being extended</param>
		/// <param name="html">The html text to be translated</param>
		/// <param name="htmlArguments">Custom arguments list to be passed to string.Format</param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Fri, 13 Jan 2012 03:55:43 GMT"/>
		public static MvcHtmlString ___q(this CultureInfo culture, string html, params object[] htmlArguments)
		{
			var result = new MvcHtmlString('"' + HttpUtility.HtmlAttributeEncode(string.Format(I18NComplete.GetText(html, lcid: culture.LCID), htmlArguments.Select(a => HttpUtility.HtmlEncode(a)).ToArray())) + '"');
            return
#if DEBUG
 I18NComplete.OnGetting___q(result, culture, html, htmlArguments) ??
#endif
 result;

		}

		/// <summary>
		/// 	<para>@Alias <c>GetHtml</c> and <c>FormatHtml</c></para>
		/// 	<para>Translates the given html applying string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))) to the current culture language. </para>
		/// 	<para>The html will be kept as it is, while arguments will be automatically HTML Encoded</para>
		/// </summary>
		/// <param name="culture">The culture being extended</param>
		/// <param name="html">The html to be translated</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 23:49:22 GMT"/>
		public static MvcHtmlString __(this CultureInfo culture,string html, params object[] arguments)
		{
			var result = new MvcHtmlString(string.Format(I18NComplete.GetText(html, lcid: culture.LCID),
				arguments.Select(a => HttpUtility.HtmlEncode(a)).ToArray()));
            return
#if DEBUG
 I18NComplete.OnGetting__(result, culture, html, arguments) ??
#endif
 result;

		}
		/// <summary>
		/// 	<para>@Alias <c>__</c> and <c>FormatHtml</c></para>
		/// 	<para>Translates the given html applying string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))) to the current culture language. </para>
		/// 	<para>The html will be kept as it is, while arguments will be automatically HTML Encoded</para>
		/// </summary>
		/// <param name="culture">The culture being extended</param>
		/// <param name="html">The html to be translated</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 23:49:22 GMT"/>
		public static MvcHtmlString GetHtml(this CultureInfo culture, string html, params object[] arguments)
		{
			return new MvcHtmlString(string.Format(I18NComplete.GetText(html, lcid: culture.LCID),
				arguments.Select(a => HttpUtility.HtmlEncode(a)).ToArray()));
		}
		/// <summary>
		/// 	<para>@Alias <c>__</c> and <c>GetHtml</c></para>
		/// 	<para>Translates the given html applying string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))) to the current culture language. </para>
		/// 	<para>The html will be kept as it is, while arguments will be automatically HTML Encoded</para>
		/// </summary>
		/// <param name="culture">The culture being extended</param>
		/// <param name="html">The html to be translated</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 23:49:22 GMT"/>
		public static MvcHtmlString FormatHtml(this CultureInfo culture, string html, params object[] arguments)
		{
			return new MvcHtmlString(string.Format(I18NComplete.GetText(html, lcid: culture.LCID),
				arguments.Select(a => HttpUtility.HtmlEncode(a)).ToArray()));
		}

		/// <summary>
		/// 	<para>@Alias <c>GetRaw</c> and <c>FormatRaw</c></para>
		/// 	<para>Translates the given html applying string.Format(html, htmlArguments) to the current culture language. </para>
		/// 	<para>Warning! Neither the html nor the htmlArguments will be encoded whatsoever</para>
		/// </summary>
		/// <param name="culture">The culture being extended</param>
		/// <param name="html">The html to be translated</param>
		/// <param name="htmlArguments">The html arguments to be applied. Warning! The arguments will not be htmlEncoded!</param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 23:46:39 GMT"/>
		public static MvcHtmlString ___(this CultureInfo culture, string html, params object[] htmlArguments)
		{
			var result = new MvcHtmlString(string.Format(I18NComplete.GetText(html, lcid: culture.LCID), htmlArguments));
            return
#if DEBUG
 I18NComplete.OnGetting___(result, culture, html, htmlArguments) ??
#endif
 result;

		}
		/// <summary>
		/// 	<para>@Alias <c>___</c> (3 underscores) and <c>FormatRaw</c></para>
		/// 	<para>Translates the given html applying string.Format(html, htmlArguments) to the current culture language. </para>
		/// 	<para>Warning! Neither the html nor the htmlArguments will be encoded whatsoever</para>
		/// </summary>
		/// <param name="culture">The culture being extended</param>
		/// <param name="html">The html to be translated</param>
		/// <param name="htmlArguments">The html arguments to be applied. Warning! The arguments will not be htmlEncoded!</param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 23:46:44 GMT"/>
		public static MvcHtmlString GetRaw(this CultureInfo culture, string html, params object[] htmlArguments)
		{
			return new MvcHtmlString(string.Format(I18NComplete.GetText(html, lcid: culture.LCID), htmlArguments));
		}

		/// <summary>
		/// 	<para>@Alias <c>___</c> (3 underscores) and <c>GetRaw</c></para>
		/// 	<para>Translates the given html applying string.Format(html, htmlArguments) to the current culture language. </para>
		/// 	<para>For each argument the escape func will be called before applying the format</para>
		/// </summary>
		/// <param name="culture">The culture being extended</param>
		/// <param name="html">The html to be translated</param>
		/// <param name="htmlArguments">The html arguments to be applied. For each argument will apply the escape func!</param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 23:46:51 GMT"/>
		public static MvcHtmlString FormatRaw(this CultureInfo culture, string html, params object[] htmlArguments)
		{
			return new MvcHtmlString(string.Format(I18NComplete.GetText(html, lcid: culture.LCID), htmlArguments));
		}

		/// <summary>
		/// 	<para>@Alias <c>GetRaw</c> and <c>FormatRaw</c></para>
		/// 	<para>Translates the given html applying string.Format(html, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
		/// 	<para>For each argument the escape func will be called before applying the format</para>
		/// </summary>
		/// <param name="culture">The culture being extended</param>
		/// <param name="html">The text to be translated</param>
		/// <param name="htmlArguments">The html arguments to be applied. For each argument will apply the escape func!</param>
		/// <param name="escapeArgumentFunc">The func to be applied for each argument .i.e. <c>a =&gt; HttpUtility.HtmlAttributeEncode(a)</c></param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 23:46:56 GMT"/>
		public static MvcHtmlString ___(this CultureInfo culture, string html, Func<object, string> escapeArgumentFunc, params object[] htmlArguments)
		{
			var result = new MvcHtmlString(string.Format(I18NComplete.GetText(html, lcid: culture.LCID), htmlArguments
				.Select(a => escapeArgumentFunc(a)).ToArray()));
            return
#if DEBUG
 I18NComplete.OnGetting___(result, CultureInfo.CurrentCulture, html, escapeArgumentFunc, htmlArguments) ??
#endif
 result;
		}
		/// <summary>
		/// 	<para>@Alias <c>___</c> (3 underscores) and <c>FormatRaw</c></para>
		/// 	<para>Translates the given html applying string.Format(html, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
		/// 	<para>For each argument the escape func will be called before applying the format</para>
		/// </summary>
		/// <param name="culture">The culture being extended</param>
		/// <param name="text">The html to be translated</param>
		/// <param name="arguments">The html arguments to be applied. For each argument will apply the escape func!</param>
		/// <param name="escapeArgumentFunc">The func to be applied for each argument .i.e. <c>a =&gt; HttpUtility.HtmlAttributeEncode(a)</c></param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 23:47:00 GMT"/>
		public static MvcHtmlString GetRaw(this CultureInfo culture, string text,Func<object, object> escapeArgumentFunc, params object[] arguments)
		{
			return new MvcHtmlString(string.Format(I18NComplete.GetText(text, lcid: culture.LCID), arguments
				.Select(a => escapeArgumentFunc(a)).ToArray()));
		}

		/// <summary>
		/// 	<para>@Alias <c>___</c> (3 underscores) and <c>FormatRaw</c></para>
		/// 	<para>Translates the given html applying string.Format(html, arguments) to the current culture language. </para>
		/// 	<para>For each argument the escape func will be called before applying the format</para>
		/// </summary>
		/// <param name="culture">The culture being extended</param>
		/// <param name="text">The html to be translated</param>
		/// <param name="arguments">The html arguments to be applied. For each argument will apply the escape func!</param>
		/// <param name="escapeArgumentFunc">The func to be applied for each argument .i.e. <c>a =&gt; HttpUtility.HtmlAttributeEncode(a)</c></param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 23:47:04 GMT"/>
		public static MvcHtmlString FormatRaw(this CultureInfo culture, string text,Func<object, object> escapeArgumentFunc, params object[] arguments)
		{
			return new MvcHtmlString(string.Format(I18NComplete.GetText(text, lcid: culture.LCID), arguments
				.Select(a => escapeArgumentFunc(a)).ToArray()));
		}
	}
}
