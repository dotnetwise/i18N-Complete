using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.WebPages;
using System.Globalization;

namespace System.Web.Mvc
{

	/// <summary>A transparent localizable class which represents the properties and methods that are needed in order to render a view that uses ASP.NET Razor syntax.</summary>
	/// <typeparam name="TModel">The type of the view data model.</typeparam>
	/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 20:41:18 GMT"/>
	public abstract partial class LocalizableWebViewPage<TModel>
		: WebViewPage<TModel>
	{
		/// <summary>
		/// 	<para>@Alias <c>GetPluralString</c> and <c>FormatPlural</c></para>
		/// 	<para>Translates the given singular or plural text applying string.Format(text, arguments) to the current culture language. </para>
		/// 	<para>The singular/plural text and argument values will be HTML Encoded when used in ASP.NET MVC</para>
		/// </summary>
		/// <param name="singular">The text to be translated when count is 1</param>
		/// <param name="plural">The text to be translated when count is NOT 1</param>
		/// <param name="count">If count is 1 the singular text will be used, otherwise the plural text</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
		/// <returns>The translated formatted text as string</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 19:22:32 GMT"/>
		public static string _s(string singular, string plural, int count, params object[] arguments)
		{
			return
				(arguments == null || arguments.Length == 0)
				? string.Format(count == 1 ? Internationalization.GetText(singular) : Internationalization.GetText(singular, plural: true), count)
				: string.Format(count == 1 ? Internationalization.GetText(singular) : Internationalization.GetText(singular, plural: true), new object[] { count }.Concat(arguments).ToArray());
		}
		/// <summary>
		/// 	<para>@Alias <c>_</c> (1 underscore) and <c>FormatPlural</c></para>
		/// 	<para>Translates the given singular or plural text applying string.Format(text, arguments) to the current culture language. </para>
		/// 	<para>The singular/plural text and argument values will be HTML Encoded when used in ASP.NET MVC</para>
		/// </summary>
		/// <param name="singular">The text to be translated when count is 1</param>
		/// <param name="plural">The text to be translated when count is NOT 1</param>
		/// <param name="count">If count is 1 the singular text will be used, otherwise the plural text</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
		/// <returns>The translated formatted text as string</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 19:24:55 GMT"/>
		public static string GetPluralString(string singular, string plural, int count, params object[] arguments)
		{
			return _s(singular, plural, count, arguments);
		}
		/// <summary>
		/// 	<para>@Alias <c>_</c> (1 underscore) and <c>GetPluralString</c></para>
		/// 	<para>Translates the given singular or plural text applying string.Format(text, arguments) to the current culture language. </para>
		/// 	<para>The singular/plural text and argument values will be HTML Encoded when used in ASP.NET MVC</para>
		/// </summary>
		/// <param name="singular">The text to be translated when count is 1</param>
		/// <param name="plural">The text to be translated when count is NOT 1</param>
		/// <param name="count">If count is 1 the singular text will be used, otherwise the plural text</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
		/// <returns>The translated formatted text as string</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 19:25:23 GMT"/>
		public static string FormatPlural(string singular, string plural, int count, params object[] arguments)
		{
			return _s(singular, plural, count, arguments);
		}

		/// <summary>
		/// 	<para>@Alias <c>GetPluralHtml</c> and <c>FormatHtmlPlural</c></para>
		/// 	<para>Translates the given singular or plural HTML applying string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))) to the current culture language. </para>
		/// 	<para>The singular/plural HTML will be kept as it is, while arguments will be automatically HTML Encoded</para>
		/// </summary>
		/// <param name="singularHTML">The HTML to be translated when count is 1</param>
		/// <param name="pluralHTML">The HTML to be translated when count is NOT 1</param>
		/// <param name="count">If count is 1 the singular HTML will be used, otherwise the plural text</param>
		/// <param name="arguments">The arguments to be applied as arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))</param>
		/// <returns>The translated formatted HTML as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 19:28:20 GMT"/>
		public static MvcHtmlString __s(string singularHTML, string pluralHTML, int count, params object[] arguments)
		{
			return new MvcHtmlString(
				(arguments == null || arguments.Length == 0)
				? string.Format(count == 1 ? Internationalization.GetText(singularHTML) : Internationalization.GetText(singularHTML, plural: true), count)
				: string.Format(count == 1 ? Internationalization.GetText(singularHTML) : Internationalization.GetText(singularHTML, plural: true), new object[] { count }.Concat(arguments.Select(a => HttpUtility.HtmlEncode(a))).ToArray()));
		}
		/// <summary>
		/// 	<para>@Alias <c>__</c> (2 underscores) and <c>FormatHtmlPlural</c></para>
		/// 	<para>Translates the given singular or plural HTML applying string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))) to the current culture language. </para>
		/// 	<para>The singular/plural HTML will be kept as it is, while arguments will be automatically HTML Encoded</para>
		/// </summary>
		/// <param name="singularHTML">The HTML to be translated when count is 1</param>
		/// <param name="pluralHTML">The HTML to be translated when count is NOT 1</param>
		/// <param name="count">If count is 1 the singular HTML will be used, otherwise the plural text</param>
		/// <param name="arguments">The arguments to be applied as arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))</param>
		/// <returns>The translated formatted HTML as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 19:32:19 GMT"/>
		public static MvcHtmlString GetPluralHtml(string singularHTML, string pluralHTML, int count, params object[] arguments)
		{
			return __s(singularHTML, pluralHTML, count, arguments);
		}
		/// <summary>
		/// 	<para>@Alias <c>GetPluralHtml</c> and <c>FormatHtmlPlural</c></para>
		/// 	<para>Translates the given singular or plural HTML applying string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))) to the current culture language. </para>
		/// 	<para>The singular/plural HTML will be kept as it is, while arguments will be automatically HTML Encoded</para>
		/// </summary>
		/// <param name="singularHTML">The HTML to be translated when count is 1</param>
		/// <param name="pluralHTML">The HTML to be translated when count is NOT 1</param>
		/// <param name="count">If count is 1 the singular HTML will be used, otherwise the plural text</param>
		/// <param name="arguments">The arguments to be applied as arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))</param>
		/// <returns>The translated formatted HTML as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 19:33:25 GMT"/>
		public static MvcHtmlString FormatHtmlPlural(string singularHTML, string pluralHTML, int count, params object[] arguments)
		{
			return __s(singularHTML, pluralHTML, count, arguments);
		}


		/// <summary>
		/// 	<para>@Alias <c>GetPluralRaw</c> and <c>FormatRawPlural</c></para>
		/// 	<para>Translates the given singular or plural HTML applying string.Format(html, arguments) to the current culture language. </para>
		/// 	<para>Warning! Neither the html nor the htmlArguments will be encoded whatsoever</para>
		/// </summary>
		/// <param name="singularHTML">The HTML to be translated when count is 1</param>
		/// <param name="pluralHTML">The HTML to be translated when count is NOT 1</param>
		/// <param name="count">If count is 1 the singular HTML will be used, otherwise the plural HTML</param>
		/// <param name="htmlArguments">The html arguments to be applied. Warning! The arguments will not be htmlEncoded!</param>
		/// <returns>The translated formatted HTML as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 20:55:00 GMT"/>
		public static MvcHtmlString ___s(string singularHTML, string pluralHTML, int count, params object[] htmlArguments)
		{
			return new MvcHtmlString(
				(htmlArguments == null || htmlArguments.Length == 0)
				? string.Format(count == 1 ? Internationalization.GetText(singularHTML) : Internationalization.GetText(singularHTML, plural:true), count)
				: string.Format(count == 1 ? Internationalization.GetText(singularHTML) : Internationalization.GetText(singularHTML, plural:true), new object[] { count }.Concat(htmlArguments).ToArray()));
		}
		/// <summary>
		/// 	<para>@Alias <c>___s</c> (3 underscores and s) and <c>FormatRawPlural</c></para>
		/// 	<para>Translates the given singular or plural HTML applying string.Format(html, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
		/// 	<para>For each argument the escape func will be called before applying the format</para>
		/// </summary>
		/// <param name="singularHTML">The HTML to be translated when count is 1</param>
		/// <param name="pluralHTML">The HTML to be translated when count is NOT 1</param>
		/// <param name="count">If count is 1 the singular HTML will be used, otherwise the plural HTML</param>
		/// <param name="htmlArguments">The html arguments to be applied. Warning! The arguments will not be htmlEncoded!</param>
		/// <returns>The translated formatted HTML as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 20:55:00 GMT"/>
		public static MvcHtmlString GetPluralRaw(string singularHTML, string pluralHTML, int count, params object[] htmlArguments)
		{
			return __s(singularHTML, pluralHTML, count, htmlArguments);
		}

		/// <summary>
		/// 	<para>@Alias <c>___s</c> (3 underscores and s) and <c>GetPluralRaw</c></para>
		/// 	<para>Translates the given singular or plural HTML applying string.Format(html, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
		/// 	<para>For each argument the escape func will be called before applying the format</para>
		/// </summary>
		/// <param name="singularHTML">The HTML to be translated when count is 1</param>
		/// <param name="pluralHTML">The HTML to be translated when count is NOT 1</param>
		/// <param name="count">If count is 1 the singular HTML will be used, otherwise the plural HTML</param>
		/// <param name="htmlArguments">The html arguments to be applied. Warning! The arguments will not be htmlEncoded!</param>
		/// <returns>The translated formatted HTML as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 20:49:47 GMT"/>
		public static MvcHtmlString FormatRawPlural(string singularHTML, string pluralHTML, int count, params object[] htmlArguments)
		{
			return __s(singularHTML, pluralHTML, count, htmlArguments);
		}

		/// <summary>
		/// 	<para>@Alias <c>GetPluralRaw</c> and <c>FormatRawPlural</c></para>
		/// 	<para>Translates the given singular or plural text/html applying string.Format(count == 1 ? singular : plural, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
		/// 	<para>For each argument the escape func will be called before applying the format</para>
		/// </summary>
		/// <param name="singular">The text/HTML to be translated when count is 1</param>
		/// <param name="plural">The text/HTML to be translated when count is NOT 1</param>
		/// <param name="count">If count is 1 the singular text/HTML will be used, otherwise the plural text</param>
		/// <param name="arguments">The text/HTML arguments to be applied. For each argument will apply the escape func!</param>
		/// <param name="escapeArgumentFunc">The func to be applied for each argument .i.e. <c>a =&gt; HttpUtility.HtmlAttributeEncode(a)</c></param>
		/// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 20:09:53 GMT"/>
		public static MvcHtmlString ___s(string singular, string plural, int count, Func<object, string> escapeArgumentFunc, params object[] arguments)
		{
			return new MvcHtmlString(
				(arguments == null || arguments.Length == 0)
				? string.Format(count == 1 ? escapeArgumentFunc(Internationalization.GetText(singular)) : escapeArgumentFunc(Internationalization.GetText(singular, plural:true)), escapeArgumentFunc(count))
				: string.Format(count == 1 ? escapeArgumentFunc(Internationalization.GetText(singular)) : escapeArgumentFunc(Internationalization.GetText(singular, plural:true)),
				new object[] { escapeArgumentFunc(count) }.Concat(arguments.Select(a => escapeArgumentFunc(a))).ToArray()));
		}
		/// <summary>
		/// 	<para>@Alias <c>___s</c> (3 underscores and s) and <c>FormatRawPlural</c></para>
		/// 	<para>Translates the given singular or plural text/html applying string.Format(count == 1 ? singular : plural, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
		/// 	<para>For each argument the escape func will be called before applying the format</para>
		/// </summary>
		/// <param name="singular">The text/HTML to be translated when count is 1</param>
		/// <param name="plural">The text/HTML to be translated when count is NOT 1</param>
		/// <param name="count">If count is 1 the singular text/HTML will be used, otherwise the plural text</param>
		/// <param name="arguments">The text/HTML arguments to be applied. For each argument will apply the escape func!</param>
		/// <param name="escapeArgumentFunc">The func to be applied for each argument .i.e. <c>a =&gt; HttpUtility.HtmlAttributeEncode(a)</c></param>
		/// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 20:12:07 GMT"/>
		public static MvcHtmlString GetPluralRaw(string singular, string plural, int count, Func<object, string> escapeArgumentFunc, params object[] arguments)
		{
			return __s(singular, plural, count, escapeArgumentFunc, arguments);
		}

		/// <summary>
		/// 	<para>@Alias <c>___s</c> (3 underscores and s) and <c>GetPluralRaw</c></para>
		/// 	<para>Translates the given singular or plural text/html applying string.Format(count == 1 ? singular : plural, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
		/// 	<para>For each argument the escape func will be called before applying the format</para>
		/// </summary>
		/// <param name="singular">The text/HTML to be translated when count is 1</param>
		/// <param name="plural">The text/HTML to be translated when count is NOT 1</param>
		/// <param name="count">If count is 1 the singular text/HTML will be used, otherwise the plural text</param>
		/// <param name="arguments">The text/HTML arguments to be applied. For each argument will apply the escape func!</param>
		/// <param name="escapeArgumentFunc">The func to be applied for each argument .i.e. <c>a =&gt; HttpUtility.HtmlAttributeEncode(a)</c></param>
		/// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
		/// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 20:13:54 GMT"/>
		public static MvcHtmlString FormatRawPlural(string singular, string plural, int count, Func<object, string> escapeArgumentFunc, params object[] arguments)
		{
			return __s(singular, plural, count, escapeArgumentFunc, arguments);
		}
	}
}
