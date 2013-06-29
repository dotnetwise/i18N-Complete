using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.WebPages;
using System.Web.Mvc;
using System.Web;

namespace System.Globalization
{
    public enum TranslationKind
    {
        _,
        __,
        ___,
        _s,
        __s,
        ___s,
        __q,
        ___q,
    }
    /// <summary>A transparent localizable class which represents the properties and methods that are needed in order to render a view that uses ASP.NET Razor syntax.</summary>
    /// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 23:21:16 GMT"/>
    public static partial class I18N
    {
        /// <summary>
        /// 	<para>@Alias <c>GetPluralString</c> and <c>FormatPlural</c></para>
        /// 	<para>Translates the given singular or plural text applying string.Format(text, arguments) to the current culture language. </para>
        /// 	<para>The singular/plural text and argument values will be HTML Encoded when used in ASP.NET MVC</para>
        /// </summary>
        /// <param name="culture">The culture being extended</param>
        /// <param name="singular">The text to be translated when count is 1</param>
        /// <param name="plural">The text to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular text will be used, otherwise the plural text</param>
        /// <param name="arguments">Custom arguments list to be passed to string.Format</param>
        /// <returns>The translated formatted text as string</returns>
        /// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 23:21:19 GMT"/>
        public static string _s(this CultureInfo culture, string singular, string plural, int count, params object[] arguments)
        {
            var result = ((arguments == null || arguments.Length == 0)
                ? string.Format(count == 1 ? I18NComplete.GetText(singular, lcid: culture.LCID) : I18NComplete.GetText(singular, plural: true, lcid: culture.LCID), count)
                : string.Format(count == 1 ? I18NComplete.GetText(singular, lcid: culture.LCID) : I18NComplete.GetText(singular, plural: true, lcid: culture.LCID), new object[] { count }.Concat(arguments).ToArray()));
            return
#if DEBUG
 I18NComplete.OnGetting_s(result, culture, singular, plural, count, arguments) ??
#endif
 result;
        }
        /// <summary>
        /// 	<para>@Alias <c>_</c> (1 underscore) and <c>FormatPlural</c></para>
        /// 	<para>Translates the given singular or plural text applying string.Format(text, arguments) to the current culture language. </para>
        /// 	<para>The singular/plural text and argument values will be HTML Encoded when used in ASP.NET MVC</para>
        /// </summary>
        /// <param name="culture">The culture being extended</param>
        /// <param name="singular">The text to be translated when count is 1</param>
        /// <param name="plural">The text to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular text will be used, otherwise the plural text</param>
        /// <param name="arguments">Custom arguments list to be passed to string.Format</param>
        /// <returns>The translated formatted text as string</returns>
        /// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 23:22:00 GMT"/>
        public static string GetPluralString(this CultureInfo culture, string singular, string plural, int count, params object[] arguments)
        {
            return _s(culture, singular, plural, count, arguments);
        }
        /// <summary>
        /// 	<para>@Alias <c>_</c> (1 underscore) and <c>GetPluralString</c></para>
        /// 	<para>Translates the given singular or plural text applying string.Format(text, arguments) to the current culture language. </para>
        /// 	<para>The singular/plural text and argument values will be HTML Encoded when used in ASP.NET MVC</para>
        /// </summary>
        /// <param name="culture">The culture being extended</param>
        /// <param name="singular">The text to be translated when count is 1</param>
        /// <param name="plural">The text to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular text will be used, otherwise the plural text</param>
        /// <param name="arguments">Custom arguments list to be passed to string.Format</param>
        /// <returns>The translated formatted text as string</returns>
        /// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 23:22:00 GMT"/>
        public static string FormatPlural(this CultureInfo culture, string singular, string plural, int count, params object[] arguments)
        {
            return _s(culture, singular, plural, count, arguments);
        }

        /// <summary>
        /// 	<para>@Alias <c>GetPluralHtml</c> and <c>FormatHtmlPlural</c></para>
        /// 	<para>Translates the given singular or plural HTML applying string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))) to the current culture language. </para>
        /// 	<para>The singular/plural HTML will be kept as it is, while arguments will be automatically HTML Encoded</para>
        /// </summary>
        /// <param name="culture">The culture being extended</param>
        /// <param name="singularHTML">The HTML to be translated when count is 1</param>
        /// <param name="pluralHTML">The HTML to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular HTML will be used, otherwise the plural text</param>
        /// <param name="arguments">The arguments to be applied as arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))</param>
        /// <returns>The translated formatted HTML as MvcHtmlString</returns>
        /// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 23:24:19 GMT"/>
        public static MvcHtmlString __s(this CultureInfo culture, string singularHTML, string pluralHTML, int count, params object[] arguments)
        {

            var result = new MvcHtmlString(
                (arguments == null || arguments.Length == 0)
                ? string.Format(count == 1 ? I18NComplete.GetText(singularHTML, lcid: culture.LCID) : I18NComplete.GetText(singularHTML, plural: true, lcid: culture.LCID), count)
                : string.Format(count == 1 ? I18NComplete.GetText(singularHTML, lcid: culture.LCID) : I18NComplete.GetText(singularHTML, plural: true, lcid: culture.LCID), new object[] { count }.Concat(arguments.Select(a => HttpUtility.HtmlEncode(a))).ToArray()));
            return
#if DEBUG
 I18NComplete.OnGetting__s(result, culture, singularHTML, pluralHTML, count, arguments) ??
#endif
 result;
        }
        /// <summary>
        /// 	<para>@Alias <c>__</c> (2 underscores) and <c>FormatHtmlPlural</c></para>
        /// 	<para>Translates the given singular or plural HTML applying string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))) to the current culture language. </para>
        /// 	<para>The singular/plural HTML will be kept as it is, while arguments will be automatically HTML Encoded</para>
        /// </summary>
        /// <param name="culture">The culture being extended</param>
        /// <param name="singularHTML">The HTML to be translated when count is 1</param>
        /// <param name="pluralHTML">The HTML to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular HTML will be used, otherwise the plural text</param>
        /// <param name="arguments">The arguments to be applied as arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))</param>
        /// <returns>The translated formatted HTML as MvcHtmlString</returns>
        /// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 23:24:15 GMT"/>
        public static MvcHtmlString GetPluralHtml(this CultureInfo culture, string singularHTML, string pluralHTML, int count, params object[] arguments)
        {
            return __s(culture, singularHTML, pluralHTML, count, arguments);
        }
        /// <summary>
        /// 	<para>@Alias <c>GetPluralHtml</c> and <c>FormatHtmlPlural</c></para>
        /// 	<para>Translates the given singular or plural HTML applying string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))) to the current culture language. </para>
        /// 	<para>The singular/plural HTML will be kept as it is, while arguments will be automatically HTML Encoded</para>
        /// </summary>
        /// <param name="culture">The culture being extended</param>
        /// <param name="singularHTML">The HTML to be translated when count is 1</param>
        /// <param name="pluralHTML">The HTML to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular HTML will be used, otherwise the plural text</param>
        /// <param name="arguments">The arguments to be applied as arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))</param>
        /// <returns>The translated formatted HTML as MvcHtmlString</returns>
        public static MvcHtmlString FormatHtmlPlural(this CultureInfo culture, string singularHTML, string pluralHTML, int count, params object[] arguments)
        {
            return __s(culture, singularHTML, pluralHTML, count, arguments);
        }


        /// <summary>
        /// 	<para>@Alias <c>GetPluralRaw</c> and <c>FormatRawPlural</c></para>
        /// 	<para>Translates the given singular or plural HTML applying string.Format(html, arguments) to the current culture language. </para>
        /// 	<para>Warning! Neither the html nor the htmlArguments will be encoded whatsoever</para>
        /// </summary>
        /// <param name="culture">The culture being extended</param>
        /// <param name="singularHTML">The HTML to be translated when count is 1</param>
        /// <param name="pluralHTML">The HTML to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular HTML will be used, otherwise the plural HTML</param>
        /// <param name="htmlArguments">The html arguments to be applied. Warning! The arguments will not be htmlEncoded!</param>
        /// <returns>The translated formatted HTML as MvcHtmlString</returns>
        public static MvcHtmlString ___s(this CultureInfo culture, string singularHTML, string pluralHTML, int count, params object[] htmlArguments)
        {
            var result = new MvcHtmlString(
 ((htmlArguments == null || htmlArguments.Length == 0)
                ? string.Format(count == 1 ? I18NComplete.GetText(singularHTML, lcid: culture.LCID) : I18NComplete.GetText(singularHTML, plural: true, lcid: culture.LCID), count)
                : string.Format(count == 1 ? I18NComplete.GetText(singularHTML, lcid: culture.LCID) : I18NComplete.GetText(singularHTML, plural: true, lcid: culture.LCID), new object[] { count }.Concat(htmlArguments).ToArray())));
            return
#if DEBUG
 I18NComplete.OnGetting___s(result, culture, singularHTML, pluralHTML, count, htmlArguments) ??
#endif
 result;
        }
        /// <summary>
        /// 	<para>@Alias <c>___s</c> (3 underscores and s) and <c>FormatRawPlural</c></para>
        /// 	<para>Translates the given singular or plural HTML applying string.Format(html, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
        /// 	<para>For each argument the escape func will be called before applying the format</para>
        /// </summary>
        /// <param name="culture">The culture being extended</param>
        /// <param name="singularHTML">The HTML to be translated when count is 1</param>
        /// <param name="pluralHTML">The HTML to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular HTML will be used, otherwise the plural HTML</param>
        /// <param name="htmlArguments">The html arguments to be applied. Warning! The arguments will not be htmlEncoded!</param>
        /// <returns>The translated formatted HTML as MvcHtmlString</returns>
        public static MvcHtmlString GetPluralRaw(this CultureInfo culture, string singularHTML, string pluralHTML, int count, params object[] htmlArguments)
        {
            return __s(culture, singularHTML, pluralHTML, count, htmlArguments);
        }

        /// <summary>
        /// 	<para>@Alias <c>___s</c> (3 underscores and s) and <c>GetPluralRaw</c></para>
        /// 	<para>Translates the given singular or plural HTML applying string.Format(html, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
        /// 	<para>For each argument the escape func will be called before applying the format</para>
        /// </summary>
        /// <param name="culture">The culture being extended</param>
        /// <param name="singularHTML">The HTML to be translated when count is 1</param>
        /// <param name="pluralHTML">The HTML to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular HTML will be used, otherwise the plural HTML</param>
        /// <param name="htmlArguments">The html arguments to be applied. Warning! The arguments will not be htmlEncoded!</param>
        /// <returns>The translated formatted HTML as MvcHtmlString</returns>
        public static MvcHtmlString FormatRawPlural(this CultureInfo culture, string singularHTML, string pluralHTML, int count, params object[] htmlArguments)
        {
            return __s(culture, singularHTML, pluralHTML, count, htmlArguments);
        }

        /// <summary>
        /// 	<para>@Alias <c>GetPluralRaw</c> and <c>FormatRawPlural</c></para>
        /// 	<para>Translates the given singular or plural text/html applying string.Format(count == 1 ? singular : plural, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
        /// 	<para>For each argument the escape func will be called before applying the format</para>
        /// </summary>
        /// <param name="culture">The culture being extended</param>
        /// <param name="singularHTML">The text/HTML to be translated when count is 1</param>
        /// <param name="pluralHTML">The text/HTML to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular text/HTML will be used, otherwise the plural text</param>
        /// <param name="htmlArguments">The text/HTML arguments to be applied. For each argument will apply the escape func!</param>
        /// <param name="escapeArgumentFunc">The func to be applied for each argument .i.e. <c>a =&gt; HttpUtility.HtmlAttributeEncode(a)</c></param>
        /// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
        /// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 20:09:53 GMT"/>
        public static MvcHtmlString ___s(this CultureInfo culture, string singularHTML, string pluralHTML, int count, Func<object, string> escapeArgumentFunc, params object[] htmlArguments)
        {
            var result = new MvcHtmlString(
 ((htmlArguments == null || htmlArguments.Length == 0)
                ? string.Format(count == 1 ? I18NComplete.GetText(singularHTML, lcid: culture.LCID) : I18NComplete.GetText(singularHTML, plural: true, lcid: culture.LCID), escapeArgumentFunc(count))
                : string.Format(count == 1 ? I18NComplete.GetText(singularHTML, lcid: culture.LCID) : I18NComplete.GetText(singularHTML, plural: true, lcid: culture.LCID),
                new object[] { escapeArgumentFunc(count) }.Concat(htmlArguments.Select(a => escapeArgumentFunc(a))).ToArray())));
            return
#if DEBUG
 I18NComplete.OnGetting___s(result, culture, singularHTML, pluralHTML, count, escapeArgumentFunc, htmlArguments) ??
#endif
 result;
        }

        /// <summary>
        /// 	<para>@Alias <c>___s</c> (3 underscores and s) and <c>FormatRawPlural</c></para>
        /// 	<para>Translates the given singular or plural text/html applying string.Format(count == 1 ? singular : plural, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
        /// 	<para>For each argument the escape func will be called before applying the format</para>
        /// </summary>
        /// <param name="culture">The culture being extended</param>
        /// <param name="singular">The text/HTML to be translated when count is 1</param>
        /// <param name="plural">The text/HTML to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular text/HTML will be used, otherwise the plural text</param>
        /// <param name="arguments">The text/HTML arguments to be applied. For each argument will apply the escape func!</param>
        /// <param name="escapeArgumentFunc">The func to be applied for each argument .i.e. <c>a =&gt; HttpUtility.HtmlAttributeEncode(a)</c></param>
        /// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
        /// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 20:12:07 GMT"/>
        public static MvcHtmlString GetPluralRaw(this CultureInfo culture, string singular, string plural, int count, Func<object, string> escapeArgumentFunc, params object[] arguments)
        {
            return __s(culture, singular, plural, count, escapeArgumentFunc, arguments);
        }

        /// <summary>
        /// 	<para>@Alias <c>___s</c> (3 underscores and s) and <c>GetPluralRaw</c></para>
        /// 	<para>Translates the given singular or plural text/html applying string.Format(count == 1 ? singular : plural, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
        /// 	<para>For each argument the escape func will be called before applying the format</para>
        /// </summary>
        /// <param name="culture">The culture being extended</param>
        /// <param name="singular">The text/HTML to be translated when count is 1</param>
        /// <param name="plural">The text/HTML to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular text/HTML will be used, otherwise the plural text</param>
        /// <param name="arguments">The text/HTML arguments to be applied. For each argument will apply the escape func!</param>
        /// <param name="escapeArgumentFunc">The func to be applied for each argument .i.e. <c>a =&gt; HttpUtility.HtmlAttributeEncode(a)</c></param>
        /// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
        /// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 20:13:54 GMT"/>
        public static MvcHtmlString FormatRawPlural(this CultureInfo culture, string singular, string plural, int count, Func<object, string> escapeArgumentFunc, params object[] arguments)
        {
            return __s(culture, singular, plural, count, escapeArgumentFunc, arguments);
        }
    }
}
