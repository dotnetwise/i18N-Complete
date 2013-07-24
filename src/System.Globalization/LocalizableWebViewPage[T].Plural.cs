using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.WebPages;
using System.Globalization;
using System.Web.WebPages.Instrumentation;
using System.IO;

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
            return CultureInfo.CurrentUICulture._s(singular, plural, count, arguments);
        }
        /// <summary>
        /// 	<para>@Alias <c>_s</c> (1 underscore) and <c>FormatPlural</c></para>
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
            return CultureInfo.CurrentUICulture._s(singular, plural, count, arguments);
        }
        /// <summary>
        /// 	<para>@Alias <c>_s</c> (1 underscore) and <c>GetPluralString</c></para>
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
            return CultureInfo.CurrentUICulture._s(singular, plural, count, arguments);
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
            return CultureInfo.CurrentUICulture.__s(singularHTML, pluralHTML, count, arguments);
        }
        /// <summary>
        /// 	<para>@Alias <c>__s</c> (2 underscores) and <c>FormatHtmlPlural</c></para>
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
            return CultureInfo.CurrentUICulture.__s(singularHTML, pluralHTML, count, arguments);
        }
        /// <summary>
        /// 	<para>@Alias <c>GetPluralHtml</c> and <c>__s</c></para>
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
            return CultureInfo.CurrentUICulture.__s(singularHTML, pluralHTML, count, arguments);
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
            return CultureInfo.CurrentUICulture.___s(singularHTML, pluralHTML, count, htmlArguments);
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
            return CultureInfo.CurrentUICulture.___s(singularHTML, pluralHTML, count, htmlArguments);
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
            return CultureInfo.CurrentUICulture.___s(singularHTML, pluralHTML, count, htmlArguments);
        }

        /// <summary>
        /// 	<para>@Alias <c>GetPluralRaw</c> and <c>FormatRawPlural</c></para>
        /// 	<para>Translates the given singular or plural text/html applying string.Format(count == 1 ? singular : plural, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
        /// 	<para>For each argument the escape func will be called before applying the format</para>
        /// </summary>
        /// <param name="singularHTML">The text/HTML to be translated when count is 1</param>
        /// <param name="pluralHTML">The text/HTML to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular text/HTML will be used, otherwise the plural text</param>
        /// <param name="htmlArguments">The text/HTML arguments to be applied. For each argument will apply the escape func!</param>
        /// <param name="escapeArgumentFunc">The func to be applied for each argument .i.e. <c>a =&gt; HttpUtility.HtmlAttributeEncode(a)</c></param>
        /// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
        /// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 20:09:53 GMT"/>
        public static MvcHtmlString ___s(string singularHTML, string pluralHTML, int count, Func<object, string> escapeArgumentFunc, params object[] htmlArguments)
        {
            return CultureInfo.CurrentUICulture.___s(singularHTML, pluralHTML, count, escapeArgumentFunc, htmlArguments);
        }
        /// <summary>
        /// 	<para>@Alias <c>___s</c> (3 underscores and s) and <c>FormatRawPlural</c></para>
        /// 	<para>Translates the given singular or plural text/html applying string.Format(count == 1 ? singular : plural, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
        /// 	<para>For each argument the escape func will be called before applying the format</para>
        /// </summary>
        /// <param name="singularHTML">The text/HTML to be translated when count is 1</param>
        /// <param name="pluralHTML">The text/HTML to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular text/HTML will be used, otherwise the plural text</param>
        /// <param name="htmlArguments">The text/HTML arguments to be applied. For each argument will apply the escape func!</param>
        /// <param name="escapeArgumentFunc">The func to be applied for each argument .i.e. <c>a =&gt; HttpUtility.HtmlAttributeEncode(a)</c></param>
        /// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
        /// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 20:12:07 GMT"/>
        public static MvcHtmlString GetPluralRaw(string singularHTML, string pluralHTML, int count, Func<object, string> escapeArgumentFunc, params object[] htmlArguments)
        {
            return CultureInfo.CurrentUICulture.___s(singularHTML, pluralHTML, count, escapeArgumentFunc, htmlArguments);
        }

        /// <summary>
        /// 	<para>@Alias <c>___s</c> (3 underscores and s) and <c>GetPluralRaw</c></para>
        /// 	<para>Translates the given singular or plural text/html applying string.Format(count == 1 ? singular : plural, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
        /// 	<para>For each argument the escape func will be called before applying the format</para>
        /// </summary>
        /// <param name="singularHTML">The text/HTML to be translated when count is 1</param>
        /// <param name="pluralHTML">The text/HTML to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular text/HTML will be used, otherwise the plural text</param>
        /// <param name="htmlArguments">The text/HTML arguments to be applied. For each argument will apply the escape func!</param>
        /// <param name="escapeArgumentFunc">The func to be applied for each argument .i.e. <c>a =&gt; HttpUtility.HtmlAttributeEncode(a)</c></param>
        /// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
        /// <created author="laurentiu.macovei" date="Thu, 24 Nov 2011 20:13:54 GMT"/>
        public static MvcHtmlString FormatRawPlural(string singularHTML, string pluralHTML, int count, Func<object, string> escapeArgumentFunc, params object[] htmlArguments)
        {
            return CultureInfo.CurrentUICulture.___s(singularHTML, pluralHTML, count, escapeArgumentFunc, htmlArguments);
        }

        //https://gist.github.com/anurse/4036121
        //http://stackoverflow.com/questions/12321616/why-is-mvc-4-razor-escaping-ampersand-when-using-html-raw-in-a-title-attribute
        /// <summary>Fixed MVC 4 bug &lt;div title=@Html.Raw("\"A&amp;B\") to render correctly &lt;div title="A&amp;B"</summary><param name="pageVirtualPath"></param><param name="writer"></param><param name="name"></param><param name="prefix"></param><param name="suffix"></param><param name="values"></param>
        protected override void WriteAttributeTo(string pageVirtualPath, TextWriter writer, string name, PositionTagged<string> prefix, PositionTagged<string> suffix, params AttributeValue[] values)
        {
            bool first = true;
            bool wroteSomething = false;
            if (values.Length == 0)
            {
                // Explicitly empty attribute, so write the prefix and suffix
                WritePositionTaggedLiteral(writer, pageVirtualPath, prefix);
                WritePositionTaggedLiteral(writer, pageVirtualPath, suffix);
            }
            else
            {
                for (int i = 0; i < values.Length; i++)
                {
                    AttributeValue attrVal = values[i];
                    PositionTagged<object> val = attrVal.Value;
                    PositionTagged<string> next = i == values.Length - 1 ?
                        suffix : // End of the list, grab the suffix
                        values[i + 1].Prefix; // Still in the list, grab the next prefix

                    bool? boolVal = null;
                    if (val.Value is bool)
                    {
                        boolVal = (bool)val.Value;
                    }

                    if (val.Value != null && (boolVal == null || boolVal.Value))
                    {
                        string valStr = val.Value as string;
                        // This shouldn't be needed any more
                        //if (valStr == null)
                        //{
                        //    valStr = val.Value.ToString();
                        //}
                        if (boolVal != null)
                        {
                            valStr = name;
                        }

                        if (first)
                        {
                            WritePositionTaggedLiteral(writer, pageVirtualPath, prefix);
                            first = false;
                        }
                        else
                        {
                            WritePositionTaggedLiteral(writer, pageVirtualPath, attrVal.Prefix);
                        }

                        // Calculate length of the source span by the position of the next value (or suffix)
                        int sourceLength = next.Position - attrVal.Value.Position;

                        BeginContext(writer, pageVirtualPath, attrVal.Value.Position, sourceLength, isLiteral: attrVal.Literal);
                        if (attrVal.Literal)
                        {
                            WriteLiteralTo(writer, val.Value);
                        }
                        else
                        {
                            // Patch: Don't use valStr, use val.Value
                            WriteTo(writer, val.Value); // Write value
                        }
                        EndContext(writer, pageVirtualPath, attrVal.Value.Position, sourceLength, isLiteral: attrVal.Literal);
                        wroteSomething = true;
                    }
                }
                if (wroteSomething)
                {
                    WritePositionTaggedLiteral(writer, pageVirtualPath, suffix);
                }
            }
        }

        /// <summary>Fixed MVC 4 bug &lt;div title=@Html.Raw("\"A&amp;B\") to render correctly &lt;div title="A&amp;B"</summary><param name="writer"></param><param name="pageVirtualPath"></param><param name="value"></param><param name="position"></param>
        private void WritePositionTaggedLiteral(TextWriter writer, string pageVirtualPath, string value, int position)
        {
            BeginContext(writer, pageVirtualPath, position, value.Length, isLiteral: true);
            WriteLiteralTo(writer, value);
            EndContext(writer, pageVirtualPath, position, value.Length, isLiteral: true);
        }

        /// <summary>Fixed MVC 4 bug &lt;div title=@Html.Raw("\"A&amp;B\") to render correctly &lt;div title="A&amp;B"</summary><param name="writer"></param><param name="pageVirtualPath"></param><param name="value"></param>
        private void WritePositionTaggedLiteral(TextWriter writer, string pageVirtualPath, PositionTagged<string> value)
        {
            WritePositionTaggedLiteral(writer, pageVirtualPath, value.Value, value.Position);
        }
    }
}
