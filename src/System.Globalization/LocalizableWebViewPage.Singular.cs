using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.WebPages;
using System.Globalization;

namespace System.Web.Mvc
{

	/// <summary>A transparent localizable class which represents the properties and methods that are needed in order to render a view that uses ASP.NET Razor syntax.</summary>
	/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 13:34:48 GMT"/>
	public abstract partial class LocalizableWebViewPage
		: WebViewPage
	{
		/// <summary>
		/// 	<para>@Alias <c>GetString</c> and <c>Format</c></para>
		/// 	<para>Translates the given text applying string.Format(text, arguments) to the current culture language. </para>
		/// 	<para>The text and argument values will be HTML Encoded when used in ASP.NET MVC</para>
		/// </summary>
		/// <param name="text">The text to be translated</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
		/// <returns>The translated formatted text as string</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 18:00:54 GMT"/>
		public static string _(string text, params object[] arguments)
		{
			var result = string.Format(I18NComplete.GetText(text), arguments);
            return
#if DEBUG
 I18NComplete.OnGetting_(result, CultureInfo.CurrentCulture, text, arguments) ??
#endif
 result;
        }
		/// <summary>
		/// 	<para>@Alias <c>_</c> and <c>Format</c></para>
		/// 	<para>Translates the given text applying string.Format(text, arguments) to the current culture language. </para>
		/// 	<para>The text and argument values will be HTML Encoded when used in ASP.NET MVC</para>
		/// </summary>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
		/// <param name="text">The text to be translated</param>
		/// <returns>The translated formatted text as string</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 17:52:04 GMT"/>
		public static string GetString(string text, params object[] arguments)
		{
			return string.Format(I18NComplete.GetText(text), arguments);
		}
		/// <summary>
		/// 	<para>@Alias <c>_</c> and <c>GetString</c></para>
		/// 	<para>Translates the given text applying string.Format(text, arguments) to the current culture language. </para>
		/// 	<para>The text and argument values will be HTML Encoded when used in ASP.NET MVC</para>
		/// </summary>
		/// <param name="text">The text to be translated</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
		/// <returns>The translated formatted text as string</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 18:00:49 GMT"/>
		public static string Format(string text, params object[] arguments)
		{
			return string.Format(I18NComplete.GetText(text), arguments);
		}

		/// <summary>
		/// 	<para>@Alias <c>GetHtml</c> and <c>FormatHtml</c></para>
		/// 	<para>Translates the given html applying string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))) to the current culture language. </para>
		/// 	<para>The html will be kept as it is, while arguments will be automatically HTML Encoded</para>
		/// </summary>
		/// <param name="html">The html to be translated</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 18:00:45 GMT"/>
		public static MvcHtmlString __(string html, params object[] arguments)
		{
			var result = new MvcHtmlString(string.Format(I18NComplete.GetText(html),
				arguments.Select(a => HttpUtility.HtmlEncode(a)).ToArray()));
            return
#if DEBUG
 I18NComplete.OnGetting__(result, CultureInfo.CurrentCulture, html, arguments) ??
#endif
 result;
        }
		/// <summary>
		/// 	<para>@Alias <c>__</c> and <c>FormatHtml</c></para>
		/// 	<para>Translates the given html applying string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))) to the current culture language. </para>
		/// 	<para>The html will be kept as it is, while arguments will be automatically HTML Encoded</para>
		/// </summary>
		/// <param name="html">The html to be translated</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 18:00:32 GMT"/>
		public static MvcHtmlString GetHtml(string html, params object[] arguments)
		{
			return new MvcHtmlString(string.Format(I18NComplete.GetText(html),
				arguments.Select(a => HttpUtility.HtmlEncode(a)).ToArray()));
		}
		/// <summary>
		/// 	<para>@Alias <c>__</c> and <c>GetHtml</c></para>
		/// 	<para>Translates the given html applying string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))) to the current culture language. </para>
		/// 	<para>The html will be kept as it is, while arguments will be automatically HTML Encoded</para>
		/// </summary>
		/// <param name="html">The html to be translated</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 18:02:50 GMT"/>
		public static MvcHtmlString FormatHtml(string html, params object[] arguments)
		{
			return new MvcHtmlString(string.Format(I18NComplete.GetText(html),
				arguments.Select(a => HttpUtility.HtmlEncode(a)).ToArray()));
		}

		/// <summary>
		/// 	<para>Quotes the text as the xgettext cannot correctly extract values from attributes</para>
		/// 	<para>Translates the given html applying '"'+HttpUtility.HtmlAttributeEncode(string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))))+'"' to the current culture language. </para>
		/// 	<para>The html will be kept as it is, while arguments will be automatically HTML Encoded</para>
		/// 	<para>Usage: &lt;input type="submit" value=@__q("Save") /&gt;  -- Note the missing quotes!</para>
		/// </summary>
		/// <param name="html">The text to be translated</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Fri, 13 Jan 2012 03:55:43 GMT"/>
		public static MvcHtmlString __q(string html, params object[] arguments)
		{
			var result = new MvcHtmlString('"' + HttpUtility.HtmlAttributeEncode(string.Format(I18NComplete.GetText(html), arguments.Select(a => HttpUtility.HtmlEncode(a)).ToArray())) + '"');
            return
#if DEBUG
 I18NComplete.OnGetting__q(result, CultureInfo.CurrentCulture, html, arguments) ??
#endif
 result;
		}
		/// <summary>
		/// 	<para>Quotes the text as the xgettext cannot correctly extract values from attributes</para>
		/// 	<para>Translates the given html applying '"'+HttpUtility.HtmlAttributeEncode(string.Format(html, htmlArguments))+'"' to the current culture language. </para>
		/// 	<para>Warning! Neither the html nor the htmlArguments will be encoded whatsoever</para>
		/// 	<para>Usage: &lt;div title=@___q("&lt; class='tip'&gt;There are {0} {1} in cart.&lt;/div&gt;", 3, "&lt;b&gt;Items&lt;/b&gt;")&gt;...  -- Note the missing quotes!</para>
		/// </summary>
		/// <param name="html">The html to be translated</param>
		/// <param name="htmlArguments">Custom arguments list to be passed to string.Format</param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Fri, 13 Jan 2012 03:55:43 GMT"/>
		public static MvcHtmlString ___q(string html, params object[] htmlArguments)
		{
			var result = new MvcHtmlString('"' + HttpUtility.HtmlAttributeEncode(string.Format(I18NComplete.GetText(html), htmlArguments.Select(a => HttpUtility.HtmlEncode(a)).ToArray())) + '"');
            return
#if DEBUG
 I18NComplete.OnGetting___q(result, CultureInfo.CurrentCulture, html, htmlArguments) ??
#endif
 result;

		}
		/// <summary>
		/// 	<para>@Alias <c>GetRaw</c> and <c>FormatRaw</c></para>
		/// 	<para>Translates the given html applying string.Format(html, htmlArguments) to the current culture language. </para>
		/// 	<para>Warning! Neither the html nor the htmlArguments will be encoded whatsoever</para>
		/// </summary>
		/// <param name="html">The html to be translated</param>
		/// <param name="htmlArguments">The html arguments to be applied. Warning! The arguments will not be htmlEncoded!</param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 18:12:40 GMT"/>
		public static MvcHtmlString ___(string html, params object[] htmlArguments)
		{
			return new MvcHtmlString(string.Format(I18NComplete.GetText(html), htmlArguments));
		}
		/// <summary>
		/// 	<para>@Alias <c>___</c> (3 underscores) and <c>FormatRaw</c></para>
		/// 	<para>Translates the given html applying string.Format(html, htmlArguments) to the current culture language. </para>
		/// 	<para>Warning! Neither the html nor the htmlArguments will be encoded whatsoever</para>
		/// </summary>
		/// <param name="html">The html to be translated</param>
		/// <param name="htmlArguments">The html arguments to be applied. Warning! The arguments will not be htmlEncoded!</param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 18:15:15 GMT"/>
		public static MvcHtmlString GetRaw(string html, params object[] htmlArguments)
		{
			return new MvcHtmlString(string.Format(I18NComplete.GetText(html), htmlArguments));
		}

		/// <summary>
		/// 	<para>@Alias <c>___</c> (3 underscores) and <c>GetRaw</c></para>
		/// 	<para>Translates the given html applying string.Format(html, htmlArguments) to the current culture language. </para>
		/// 	<para>For each argument the escape func will be called before applying the format</para>
		/// </summary>
		/// <param name="html">The html to be translated</param>
		/// <param name="htmlArguments">The html arguments to be applied. For each argument will apply the escape func!</param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		public static MvcHtmlString FormatRaw(string html, params object[] htmlArguments)
		{ 
			return new MvcHtmlString(string.Format(I18NComplete.GetText(html), htmlArguments));
		} 

		/// <summary>
		/// 	<para>@Alias <c>GetRaw</c> and <c>FormatRaw</c></para>
		/// 	<para>Translates the given html applying string.Format(html, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
		/// 	<para>For each argument the escape func will be called before applying the format</para>
		/// </summary>
		/// <param name="text">The text to be translated</param>
		/// <param name="arguments">The html arguments to be applied. For each argument will apply the escape func!</param>
		/// <param name="escapeArgumentFunc">The func to be applied for each argument .i.e. <c>a =&gt; HttpUtility.HtmlAttributeEncode(a)</c></param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 18:47:27 GMT"/>
		public static MvcHtmlString ___(string text, Func<object, string> escapeArgumentFunc, params object[] arguments)
		{
			return new MvcHtmlString(string.Format(escapeArgumentFunc(I18NComplete.GetText(text)), arguments
				.Select(a => escapeArgumentFunc(a)).ToArray()));
		}
		/// <summary>
		/// 	<para>@Alias <c>___</c> (3 underscores) and <c>FormatRaw</c></para>
		/// 	<para>Translates the given html applying string.Format(html, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
		/// 	<para>For each argument the escape func will be called before applying the format</para>
		/// </summary>
		/// <param name="text">The html to be translated</param>
		/// <param name="arguments">The html arguments to be applied. For each argument will apply the escape func!</param>
		/// <param name="escapeArgumentFunc">The func to be applied for each argument .i.e. <c>a =&gt; HttpUtility.HtmlAttributeEncode(a)</c></param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 18:47:17 GMT"/>
		public static MvcHtmlString GetRaw(string text, Func<object, object> escapeArgumentFunc, params object[] arguments)
		{
			return new MvcHtmlString(string.Format(I18NComplete.GetText(text), arguments
				.Select(a => escapeArgumentFunc(a)).ToArray()));
		}

		/// <summary>
		/// 	<para>@Alias <c>___</c> (3 underscores) and <c>FormatRaw</c></para>
		/// 	<para>Translates the given html applying string.Format(html, arguments) to the current culture language. </para>
		/// 	<para>For each argument the escape func will be called before applying the format</para>
		/// </summary>
		/// <param name="text">The html to be translated</param>
		/// <param name="arguments">The html arguments to be applied. For each argument will apply the escape func!</param>
		/// <param name="escapeArgumentFunc">The func to be applied for each argument .i.e. <c>a =&gt; HttpUtility.HtmlAttributeEncode(a)</c></param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 18:25:01 GMT"/>
		public static MvcHtmlString FormatRaw(string text, Func<object, object> escapeArgumentFunc, params object[] arguments)
		{
			return new MvcHtmlString(string.Format(I18NComplete.GetText(text), arguments
				.Select(a => escapeArgumentFunc(a)).ToArray()));
		}
	}
}
