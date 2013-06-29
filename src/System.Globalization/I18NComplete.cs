using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace System.Globalization
{
    /// <summary>The I18NComplete class</summary>
    /// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 14:55:18 GMT"/>
    public static partial class I18NComplete
    {
        /// <summary>The language that MsgIDs are given in (generally the language the project is being developed in).</summary>
        /// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 14:55:18 GMT"/>
        public static readonly string DefaultWorkingLanguage = ConfigurationManager.AppSettings["Internalization.DefaultMessagesLanguage"] ?? "en";
        /// <summary>The default culture LCID</summary>
        /// <created author="laurentiu.macovei" date="Tue, 29 Nov 2011 12:59:18 GMT"/>
        public static readonly int DefaultWorkingLanguageLCID = I18NComplete.LCID(DefaultWorkingLanguage);

        /// <summary>
        /// This is the base path under which localizations will   
        /// be stored.  
        /// </summary>
        /// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 14:55:18 GMT"/>
        public static readonly string BasePath = ConfigurationManager.AppSettings["Internalization.Location"] ?? "~/Properties/Localization";

        /// <summary>
        /// FOR TESTING ONLY!  If true, all calls to GetText will return an empty string.  
        /// this is useful when searching for any strings that might not be flagged for localization.  
        /// </summary>
        /// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 14:55:18 GMT"/>
        public static readonly bool HideAllLocalizedText = ConfigurationManager.AppSettings["Internalization.HideAllLocalizedText"] == "true";

        /// <summary>
        /// The cached localization
        /// </summary>
        public static Dictionary<int, Localization> Localizations;

        /// <summary>Static constructor of I18NComplete</summary>
        /// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 14:55:18 GMT"/>
        static I18NComplete()
        {
            Localizations = new Dictionary<int, Localization>();
            string _basePathAbsolute = HttpContext.Current.Server.MapPath(I18NComplete.BasePath);


            if (Directory.Exists(_basePathAbsolute))
            {
                Localization l;
                foreach (string filename in Directory.GetFiles(_basePathAbsolute, "*.po", SearchOption.AllDirectories))
                {
                    var culture = Path.GetFileNameWithoutExtension(filename);
                    culture = Path.GetExtension(culture);
                    culture = culture.StartsWith(".") ? culture.Substring(1) : culture;
                    var cultureHash = string.IsNullOrWhiteSpace(culture) ? I18NComplete.DefaultWorkingLanguageLCID : LCID(culture);
                    if (!Localizations.TryGetValue(cultureHash, out l))
                    {
                        l = new Localization();
                        l.LoadFromFile(filename);
                        Localizations.Add(cultureHash, l);
                    }
                    else l.LoadFromFile(filename);
                }
            }

            if (!Localizations.ContainsKey(I18NComplete.DefaultWorkingLanguageLCID))
                Localizations.Add(I18NComplete.DefaultWorkingLanguageLCID, new Localization());
        }

        #region public methods

        /// <summary>Gets a translated message version of the supplied text message.</summary>
        /// <param name="msgID">Text to be translated</param>
        /// <param name="languageCode">Language to translate into</param>
        /// <param name="lcid">Specify the Culture LCID for faster access if you know it</param>
        /// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 14:55:19 GMT"/>
        public static Message GetMessage(string msgID, string languageCode = null, int? lcid = null)
        {
            if (I18NComplete.HideAllLocalizedText)
                return Message.Empty;
            int languageHash = lcid.HasValue ? lcid.Value : string.IsNullOrWhiteSpace(languageCode) ? CultureInfo.CurrentUICulture.LCID : LCID(languageCode);
            Localization localization;

            if (!Localizations.TryGetValue(languageHash, out localization))
            {
                if (CultureInfo.CurrentUICulture.IsNeutralCulture)
                {
                    Localizations[languageHash] = localization = new Localization();
                    languageHash = I18NComplete.DefaultWorkingLanguageLCID;
                }
                else
                {
                    var nativeCultureHash = CultureInfo.GetCultureInfo(languageHash).Parent.LCID;
                    if (!Localizations.TryGetValue(nativeCultureHash, out localization))
                    {
                        languageHash = I18NComplete.DefaultWorkingLanguageLCID;
                        Localizations.TryGetValue(languageHash, out localization);
                    }
                    else
                    {
                        //save the specific culture as the generic culture, so it will be found next time
                        Localizations[languageHash] = localization;
                    }
                }
            }

            return localization.GetMessageObject(msgID);
        }

        /// <summary>Gets a translated version of the supplied text message.</summary>
        /// <param name="msgID">Text to be translated</param>
        /// <param name="languageCode">Language to translate into</param>
        /// <param name="lcid">Specify the Culture LCID for faster access if you know it</param>
        /// <param name="plural">Specify whether you want the plural form of it or not</param>
        /// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 14:55:19 GMT"/>
        public static string GetText(string msgID, string languageCode = null,
            int? lcid = null, bool plural = false)
        {
            var msg = GetMessage(msgID, languageCode, lcid);
            //return msg == null ? null : msg.GetText(plural);
            var result = msg == null ? null : msg.GetText(plural);
            if (GettingText != null)
            {
                result = GettingText(msg, msgID, result, languageCode, lcid, plural);
            }
            return result;
        }
        /// <summary>Global interceptor event for getting every translation!</summary>
        public static event GettingTextEventHandler GettingText;
        /// <summary>Global interceptor delagate for getting every translation!</summary>
        /// <param name="msg">The message to be translated</param>
        /// <param name="msgID">The id (key to be translated) of the message</param>
        /// <param name="translation">The default translation I18NComplete provides to you</param>
        /// <param name="languageCode">The friendly language code</param>
        /// <param name="lcid">The Local Culture Identifier</param>
        /// <param name="plural">Whether the translation should be for the plural version of this message</param>
        /// <returns>The translated text</returns>
        public delegate string GettingTextEventHandler(Message msg, string msgID, string translation, string languageCode, int? lcid, bool plural);
        /// <summary>
        ///  <para>Global interceptor event for __ methods.</para>
        /// 	<para>@Alias <c>_</c> and <c>GetString</c></para>
        /// 	<para>Translates the given text applying string.Format(text, arguments) to the current culture language. </para>
        /// 	<para>The text and argument values will be HTML Encoded when used in ASP.NET MVC</para>
        /// </summary>
        /// <returns>The translated formatted text as string</returns>
        public static event GettingSingularStringEventHandler Getting_;
        /// <summary>
        ///  <para>Global interceptor event for ___ methods.</para>
        /// 	<para>@Alias <c>GetHtml</c> and <c>FormatHtml</c></para>
        /// 	<para>Translates the given html applying string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))) to the current culture language. </para>
        /// 	<para>The html will be kept as it is, while arguments will be automatically HTML Encoded</para>
        /// </summary>
        /// <returns>The translated formatted html as MvcHtmlString</returns>
        public static event GettingSingularHtmlEventHandler Getting__;
        /// <summary>
        ///  <para>Global interceptor event for ____ methods.</para>
        /// 	<para>@Alias <c>GetPluralRaw</c> and <c>FormatRawPlural</c></para>
        /// 	<para>Translates the given singular or plural text/html applying string.Format(count == 1 ? singular : plural, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
        /// 	<para>For each argument the escape func will be called before applying the format</para>
        /// </summary>
        /// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
        public static event GettingSingularHtmlAllEventHandler Getting___;
        /// <summary>
        ///  <para>Global interceptor event for ____ methods.</para>
        /// 	<para>@Alias <c>GetPluralRaw</c> and <c>FormatRawPlural</c></para>
        /// 	<para>Translates the given singular or plural text/html applying string.Format(count == 1 ? singular : plural, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
        /// 	<para>For each argument the escape func will be called before applying the format</para>
        /// </summary>
        /// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
        public static event GettingSingularHtmlAllEscapeEventHandler Getting___e;
        /// <summary>
        ///  <para>Global interceptor event for _s methods.</para>
        /// 	<para>@Alias <c>GetPluralString</c> and <c>FormatPlural</c></para>
        /// 	<para>Translates the given singular or plural text applying string.Format(text, arguments) to the current culture language. </para>
        /// 	<para>The singular/plural text and argument values will be HTML Encoded when used in ASP.NET MVC</para>
        /// </summary>
        /// <returns>The translated formatted text as string</returns>
        public static event GettingPluralStringEventHandler Getting_s;
        /// <summary>
        ///  <para>Global interceptor event for __s methods.</para>
        /// 	<para>@Alias <c>GetPluralRaw</c> and <c>FormatRawPlural</c></para>
		/// 	<para>Translates the given singular or plural text/html applying string.Format(count == 1 ? singular : plural, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
		/// 	<para>For each argument the escape func will be called before applying the format</para>
		/// </summary>
		/// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
        public static event GettingPluralHtmlEventHandler Getting__s;
        /// <summary>
        ///  <para>Global interceptor event for ___s methods.</para>
        /// 	<para>@Alias <c>GetPluralRaw</c> and <c>FormatRawPlural</c></para>
		/// 	<para>Translates the given singular or plural HTML applying string.Format(html, arguments) to the current culture language. </para>
		/// 	<para>Warning! Neither the html nor the htmlArguments will be encoded whatsoever</para>
		/// </summary>
		/// <returns>The translated formatted HTML as MvcHtmlString</returns>
        public static event GettingPluralHtmlAllEventHandler Getting___s;
        /// <summary>
        ///  <para>Global interceptor event for ___s methods.</para>
        /// 	<para>@Alias <c>GetPluralRaw</c> and <c>FormatRawPlural</c></para>
        /// 	<para>Translates the given singular or plural text/html applying string.Format(count == 1 ? singular : plural, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
        /// 	<para>For each argument the escape func will be called before applying the format</para>
        /// </summary>
        /// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
        public static event GettingPluralHtmlAllEscapeEventHandler Getting___se;
        /// <summary>
        ///  <para>Global interceptor event for __q methods.</para>
        /// 	<para>Quotes the text as the xgettext cannot correctly extract values from attributes</para>
		/// 	<para>Translates the given html applying '"'+HttpUtility.HtmlAttributeEncode(string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))))+'"' to the current culture language. </para>
		/// 	<para>The html will be kept as it is, while arguments will be automatically HTML Encoded</para>
		/// 	<para>Usage: &lt;input type="submit" value=@__q("Save") /&gt;  -- Note the missing quotes!</para>
		/// </summary>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
        public static event GettingQuotedEventHandler Getting__q;
        /// <summary>
        ///  <para>Global interceptor delegate for ___q methods.</para>
        /// 	<para>Quotes the text as the xgettext cannot correctly extract values from attributes</para>
        /// 	<para>Translates the given html applying '"'+HttpUtility.HtmlAttributeEncode(string.Format(html, htmlArguments))+'"' to the current culture language. </para>
        /// 	<para>Warning! Neither the html nor the htmlArguments will be encoded whatsoever</para>
        /// 	<para>Usage: &lt;div title=@___q("&lt; class='tip'&gt;There are {0} {1} in cart.&lt;/div&gt;", 3, "&lt;b&gt;Items&lt;/b&gt;")&gt;...  -- Note the missing quotes!</para>
        /// </summary>
        /// <returns>The translated formatted html as MvcHtmlString</returns>
        public static event GettingQuotedAllEventHandler Getting___q;
        /// <summary>
        ///  <para>Global interceptor delegate for __ methods.</para>
        /// 	<para>@Alias <c>_</c> and <c>GetString</c></para>
        /// 	<para>Translates the given text applying string.Format(text, arguments) to the current culture language. </para>
        /// 	<para>The text and argument values will be HTML Encoded when used in ASP.NET MVC</para>
        /// </summary>
        /// <returns>The translated formatted text as string</returns>
        public delegate string GettingSingularStringEventHandler(string defaultResult, CultureInfo culture, string text, params object[] arguments);
        /// <summary>
        ///  <para>Global interceptor delegate for ___ methods.</para>
        /// 	<para>@Alias <c>GetHtml</c> and <c>FormatHtml</c></para>
        /// 	<para>Translates the given html applying string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))) to the current culture language. </para>
        /// 	<para>The html will be kept as it is, while arguments will be automatically HTML Encoded</para>
        /// </summary>
        /// <param name="html">The html to be translated</param>
        /// <param name="arguments">Custom arguments list to be passed to string.Format</param>
        /// <param name="culture">The culture this translation is being translated</param>
        /// <param name="defaultResult">The I18NComplete default result. Returning null will fallback on this result, otherwise return a custom value to oveerride this.</param>
        /// <returns>The translated formatted html as MvcHtmlString</returns>
        public delegate MvcHtmlString GettingSingularHtmlEventHandler(MvcHtmlString defaultResult, CultureInfo culture, string html, params object[] arguments);
        /// <summary>
        ///  <para>Global interceptor delegate for ____ methods.</para>
        /// 	<para>@Alias <c>GetPluralRaw</c> and <c>FormatRawPlural</c></para>
        /// 	<para>Translates the given singular or plural text/html applying string.Format(count == 1 ? singular : plural, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
        /// 	<para>For each argument the escape func will be called before applying the format</para>
        /// </summary>
        /// <param name="html">The html to be translated</param>
        /// <param name="htmlArguments">Custom arguments list to be passed to string.Format</param>
        /// <param name="culture">The culture this translation is being translated</param>
        /// <param name="defaultResult">The I18NComplete default result. Returning null will fallback on this result, otherwise return a custom value to oveerride this.</param>
        /// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
        public delegate MvcHtmlString GettingSingularHtmlAllEventHandler(MvcHtmlString defaultResult, CultureInfo culture, string html, params object[] htmlArguments);
        /// <summary>
        ///  <para>Global interceptor delegate for ____ methods.</para>
        /// 	<para>@Alias <c>GetPluralRaw</c> and <c>FormatRawPlural</c></para>
        /// 	<para>Translates the given singular or plural text/html applying string.Format(count == 1 ? singular : plural, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
        /// 	<para>For each argument the escape func will be called before applying the format</para>
        /// </summary>
        /// <param name="html">The html to be translated</param>
        /// <param name="htmlArguments">The text/HTML arguments to be applied. For each argument will apply the escape func!</param>
        /// <param name="escapeArgumentFunc">The func to be applied for each argument .i.e. <c>a =&gt; HttpUtility.HtmlAttributeEncode(a)</c></param>
        /// <param name="culture">The culture this translation is being translated</param>
        /// <param name="defaultResult">The I18NComplete default result. Returning null will fallback on this result, otherwise return a custom value to oveerride this.</param>
        /// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
        public delegate MvcHtmlString GettingSingularHtmlAllEscapeEventHandler(MvcHtmlString defaultResult, CultureInfo culture, string html, Func<object, string> escapeArgumentFunc, params object[] htmlArguments);
        /// <summary>
        ///  <para>Global interceptor delegate for _s methods.</para>
        /// 	<para>@Alias <c>GetPluralString</c> and <c>FormatPlural</c></para>
		/// 	<para>Translates the given singular or plural text applying string.Format(text, arguments) to the current culture language. </para>
		/// 	<para>The singular/plural text and argument values will be HTML Encoded when used in ASP.NET MVC</para>
		/// </summary>
		/// <param name="singular">The text to be translated when count is 1</param>
		/// <param name="plural">The text to be translated when count is NOT 1</param>
		/// <param name="count">If count is 1 the singular text will be used, otherwise the plural text</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
        /// <param name="culture">The culture this translation is being translated</param>
        /// <param name="defaultResult">The I18NComplete default result. Returning null will fallback on this result, otherwise return a custom value to oveerride this.</param>
        /// <returns>The translated formatted text as string</returns>
        public delegate string GettingPluralStringEventHandler(string defaultResult, CultureInfo culture, string singular, string plural, int count, params object[] arguments);
        /// <summary>
        ///  <para>Global interceptor delegate for __s methods.</para>
        /// 	<para>@Alias <c>GetPluralHtml</c> and <c>FormatHtmlPlural</c></para>
        /// 	<para>Translates the given singular or plural HTML applying string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))) to the current culture language. </para>
        /// 	<para>The singular/plural HTML will be kept as it is, while arguments will be automatically HTML Encoded</para>
        /// </summary>
        /// <param name="singularHTML">The HTML to be translated when count is 1</param>
        /// <param name="pluralHTML">The HTML to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular HTML will be used, otherwise the plural text</param>
        /// <param name="arguments">The arguments to be applied as arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))</param>
        /// <returns>The translated formatted HTML as MvcHtmlString</returns>
        /// <param name="culture">The culture this translation is being translated</param>
        /// <param name="defaultResult">The I18NComplete default result. Returning null will fallback on this result, otherwise return a custom value to oveerride this.</param>
        /// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
        public delegate MvcHtmlString GettingPluralHtmlEventHandler(MvcHtmlString defaultResult, CultureInfo culture, string singularHTML, string pluralHTML, int count, params object[] arguments);
        /// <summary>
        ///  <para>Global interceptor delegate for ___s methods.</para>
        /// 	<para>@Alias <c>GetPluralRaw</c> and <c>FormatRawPlural</c></para>
		/// 	<para>Translates the given singular or plural HTML applying string.Format(html, arguments) to the current culture language. </para>
		/// 	<para>Warning! Neither the html nor the htmlArguments will be encoded whatsoever</para>
		/// </summary>
		/// <param name="singularHTML">The HTML to be translated when count is 1</param>
		/// <param name="pluralHTML">The HTML to be translated when count is NOT 1</param>
		/// <param name="count">If count is 1 the singular HTML will be used, otherwise the plural HTML</param>
		/// <param name="htmlArguments">The html arguments to be applied. Warning! The arguments will not be htmlEncoded!</param>
        /// <param name="culture">The culture this translation is being translated</param>
        /// <param name="defaultResult">The I18NComplete default result. Returning null will fallback on this result, otherwise return a custom value to oveerride this.</param>
        /// <returns>The translated formatted HTML as MvcHtmlString</returns>
        public delegate MvcHtmlString GettingPluralHtmlAllEventHandler(MvcHtmlString defaultResult, CultureInfo culture, string singularHTML, string pluralHTML, int count, params object[] htmlArguments);
        /// <summary>
        ///  <para>Global interceptor delegate for ___s methods.</para>
        /// 	<para>@Alias <c>GetPluralRaw</c> and <c>FormatRawPlural</c></para>
        /// 	<para>Translates the given singular or plural text/html applying string.Format(count == 1 ? singular : plural, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
        /// 	<para>For each argument the escape func will be called before applying the format</para>
        /// </summary>
        /// <param name="singularHTML">The text/HTML to be translated when count is 1</param>
        /// <param name="pluralHTML">The text/HTML to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular text/HTML will be used, otherwise the plural text</param>
        /// <param name="htmlArguments">The text/HTML arguments to be applied. For each argument will apply the escape func!</param>
        /// <param name="escapeArgumentFunc">The func to be applied for each argument .i.e. <c>a =&gt; HttpUtility.HtmlAttributeEncode(a)</c></param>
        /// <param name="culture">The culture this translation is being translated</param>
        /// <param name="defaultResult">The I18NComplete default result. Returning null will fallback on this result, otherwise return a custom value to oveerride this.</param>
        /// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
        public delegate MvcHtmlString GettingPluralHtmlAllEscapeEventHandler(MvcHtmlString defaultResult, CultureInfo culture, string singularHTML, string pluralHTML, int count, Func<object, string> escapeArgumentFunc, params object[] htmlArguments);
        /// <summary>
        ///  <para>Global interceptor delegate for __q methods.</para>
        /// 	<para>Quotes the text as the xgettext cannot correctly extract values from attributes</para>
		/// 	<para>Translates the given html applying '"'+HttpUtility.HtmlAttributeEncode(string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))))+'"' to the current culture language. </para>
		/// 	<para>The html will be kept as it is, while arguments will be automatically HTML Encoded</para>
		/// 	<para>Usage: &lt;input type="submit" value=@__q("Save") /&gt;  -- Note the missing quotes!</para>
		/// </summary>
		/// <param name="html">The text to be translated</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
        /// <param name="culture">The culture this translation is being translated</param>
        /// <param name="defaultResult">The I18NComplete default result. Returning null will fallback on this result, otherwise return a custom value to oveerride this.</param>
        /// <returns>The translated formatted html as MvcHtmlString</returns>
        public delegate MvcHtmlString GettingQuotedEventHandler(MvcHtmlString defaultResult, CultureInfo culture, string html, params object[] arguments);
        /// <summary>
        ///  <para>Global interceptor delegate for ___q methods.</para>
        /// 	<para>Quotes the text as the xgettext cannot correctly extract values from attributes</para>
        /// 	<para>Translates the given html applying '"'+HttpUtility.HtmlAttributeEncode(string.Format(html, htmlArguments))+'"' to the current culture language. </para>
        /// 	<para>Warning! Neither the html nor the htmlArguments will be encoded whatsoever</para>
        /// 	<para>Usage: &lt;div title=@___q("&lt; class='tip'&gt;There are {0} {1} in cart.&lt;/div&gt;", 3, "&lt;b&gt;Items&lt;/b&gt;")&gt;...  -- Note the missing quotes!</para>
        /// </summary>
        /// <param name="html">The html to be translated</param>
        /// <param name="htmlArguments">Custom arguments list to be passed to string.Format</param>
        /// <param name="culture">The culture this translation is being translated</param>
        /// <param name="defaultResult">The I18NComplete default result. Returning null will fallback on this result, otherwise return a custom value to oveerride this.</param>
        /// <returns>The translated formatted html as MvcHtmlString</returns>
        public delegate MvcHtmlString GettingQuotedAllEventHandler(MvcHtmlString defaultResult, CultureInfo culture, string html, params object[] htmlArguments);
        /// <summary>
        ///  <para>Throws the Getting_s global event</para>
        /// 	<para>@Alias <c>GetPluralString</c> and <c>FormatPlural</c></para>
		/// 	<para>Translates the given singular or plural text applying string.Format(text, arguments) to the current culture language. </para>
		/// 	<para>The singular/plural text and argument values will be HTML Encoded when used in ASP.NET MVC</para>
		/// </summary>
		/// <param name="singular">The text to be translated when count is 1</param>
		/// <param name="plural">The text to be translated when count is NOT 1</param>
		/// <param name="count">If count is 1 the singular text will be used, otherwise the plural text</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
        /// <param name="culture">The culture this translation is being translated</param>
        /// <param name="defaultResult">The I18NComplete default result. Returning null will fallback on this result, otherwise return a custom value to oveerride this.</param>
        /// <returns>The translated formatted text as string</returns>
        internal static string OnGetting_s(string defaultResult, CultureInfo culture, string singular, string plural, int count, params object[] arguments)
        {
            if (Getting_s != null)
                return Getting_s(defaultResult, culture, singular, plural, count, arguments);
            return null;
        }
        /// <summary>
        ///  <para>Throws the Getting__s global event</para>
        /// 	<para>@Alias <c>GetPluralHtml</c> and <c>FormatHtmlPlural</c></para>
        /// 	<para>Translates the given singular or plural HTML applying string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))) to the current culture language. </para>
        /// 	<para>The singular/plural HTML will be kept as it is, while arguments will be automatically HTML Encoded</para>
        /// </summary>
        /// <param name="singularHTML">The HTML to be translated when count is 1</param>
        /// <param name="pluralHTML">The HTML to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular HTML will be used, otherwise the plural text</param>
        /// <param name="arguments">The arguments to be applied as arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))</param>        /// <param name="culture">The culture this translation is being translated</param>
        /// <param name="defaultResult">The I18NComplete default result. Returning null will fallback on this result, otherwise return a custom value to oveerride this.</param>
        /// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
        internal static MvcHtmlString OnGetting__s(MvcHtmlString defaultResult, CultureInfo culture, string singularHTML, string pluralHTML, int count, object[] arguments)
        {
            if (Getting__s != null)
                return Getting__s(defaultResult, culture, singularHTML, pluralHTML, count, arguments);
            return null;
        }
        /// <summary>
        ///  <para>Throws the Getting___s global event</para>
        /// 	<para>@Alias <c>GetPluralRaw</c> and <c>FormatRawPlural</c></para>
        /// 	<para>Translates the given singular or plural HTML applying string.Format(html, arguments) to the current culture language. </para>
        /// 	<para>Warning! Neither the html nor the htmlArguments will be encoded whatsoever</para>
        /// </summary>
        /// <param name="singularHTML">The HTML to be translated when count is 1</param>
        /// <param name="pluralHTML">The HTML to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular HTML will be used, otherwise the plural HTML</param>
        /// <param name="htmlArguments">The html arguments to be applied. Warning! The arguments will not be htmlEncoded!</param>
        /// <param name="culture">The culture this translation is being translated</param>
        /// <param name="defaultResult">The I18NComplete default result. Returning null will fallback on this result, otherwise return a custom value to oveerride this.</param>
        /// <returns>The translated formatted HTML as MvcHtmlString</returns>
        internal static MvcHtmlString OnGetting___s(MvcHtmlString defaultResult, CultureInfo culture, string singularHTML, string pluralHTML, int count, object[] htmlArguments)
        {
            if (Getting___s != null)
                return Getting___s(defaultResult, culture, singularHTML, pluralHTML, count, htmlArguments);
            return null;
        }
        /// <summary>
        ///  <para>Throws the Getting___s global event</para>
        /// 	<para>@Alias <c>GetPluralRaw</c> and <c>FormatRawPlural</c></para>
        /// 	<para>Translates the given singular or plural text/html applying string.Format(count == 1 ? singular : plural, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
        /// 	<para>For each argument the escape func will be called before applying the format</para>
        /// </summary>
        /// <param name="singularHTML">The text/HTML to be translated when count is 1</param>
        /// <param name="pluralHTML">The text/HTML to be translated when count is NOT 1</param>
        /// <param name="count">If count is 1 the singular text/HTML will be used, otherwise the plural text</param>
        /// <param name="htmlArguments">The text/HTML arguments to be applied. For each argument will apply the escape func!</param>
        /// <param name="escapeArgumentFunc">The func to be applied for each argument .i.e. <c>a =&gt; HttpUtility.HtmlAttributeEncode(a)</c></param>
        /// <param name="culture">The culture this translation is being translated</param>
        /// <param name="defaultResult">The I18NComplete default result. Returning null will fallback on this result, otherwise return a custom value to oveerride this.</param>
        /// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
        internal static MvcHtmlString OnGetting___s(MvcHtmlString defaultResult, CultureInfo culture, string singularHTML, string pluralHTML, int count, Func<object, string> escapeArgumentFunc, object[] htmlArguments)
        {
            if (Getting___se != null)
                return Getting___se(defaultResult, culture, singularHTML, pluralHTML, count, escapeArgumentFunc, htmlArguments);
            return null;
        }
        /// <summary>
        ///  <para>Throws the Getting__q global event</para>
        /// 	<para>Quotes the text as the xgettext cannot correctly extract values from attributes</para>
		/// 	<para>Translates the given html applying '"'+HttpUtility.HtmlAttributeEncode(string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))))+'"' to the current culture language. </para>
		/// 	<para>The html will be kept as it is, while arguments will be automatically HTML Encoded</para>
		/// 	<para>Usage: &lt;input type="submit" value=@__q("Save") /&gt;  -- Note the missing quotes!</para>
		/// </summary>
        /// <param name="culture">The culture this translation is being translated</param>
        /// <param name="defaultResult">The I18NComplete default result. Returning null will fallback on this result, otherwise return a custom value to oveerride this.</param>
		/// <param name="html">The text to be translated</param>
		/// <param name="arguments">Custom arguments list to be passed to string.Format</param>
		/// <returns>The translated formatted html as MvcHtmlString</returns>
        internal static MvcHtmlString OnGetting__q(MvcHtmlString defaultResult, CultureInfo culture, string html, params object[] arguments)
        {
            if (Getting__q != null)
                return Getting__q(defaultResult, culture, html, arguments);
            return null;
        }
        /// <summary>
        ///  <para>Throws the Getting___q global event</para>
        /// 	<para>Quotes the text as the xgettext cannot correctly extract values from attributes</para>
        /// 	<para>Translates the given html applying '"'+HttpUtility.HtmlAttributeEncode(string.Format(html, htmlArguments))+'"' to the current culture language. </para>
        /// 	<para>Warning! Neither the html nor the htmlArguments will be encoded whatsoever</para>
        /// 	<para>Usage: &lt;div title=@___q("&lt; class='tip'&gt;There are {0} {1} in cart.&lt;/div&gt;", 3, "&lt;b&gt;Items&lt;/b&gt;")&gt;...  -- Note the missing quotes!</para>
        /// </summary>
        /// <param name="html">The html to be translated</param>
        /// <param name="htmlArguments">Custom arguments list to be passed to string.Format</param>
        /// <param name="culture">The culture this translation is being translated</param>
        /// <param name="defaultResult">The I18NComplete default result. Returning null will fallback on this result, otherwise return a custom value to oveerride this.</param>
        /// <returns>The translated formatted html as MvcHtmlString</returns>
        internal static MvcHtmlString OnGetting___q(MvcHtmlString defaultResult, CultureInfo culture, string html, params object[] htmlArguments)
        {
            if (Getting___q != null)
                return Getting___q(defaultResult, culture, html, htmlArguments);
            return null;
        }
        /// <summary>
        ///  <para>Throws the Getting_ global event</para>
        /// 	<para>@Alias <c>_</c> and <c>GetString</c></para>
        /// 	<para>Translates the given text applying string.Format(text, arguments) to the current culture language. </para>
        /// 	<para>The text and argument values will be HTML Encoded when used in ASP.NET MVC</para>
        /// </summary>
        /// <param name="text">The text to be translated</param>
        /// <param name="arguments">Custom arguments list to be passed to string.Format</param>
        /// <param name="culture">The culture this translation is being translated</param>
        /// <param name="defaultResult">The I18NComplete default result. Returning null will fallback on this result, otherwise return a custom value to oveerride this.</param>
        /// <returns>The translated formatted text as string</returns>
        internal static string OnGetting_(string defaultResult, CultureInfo culture, string text, object[] arguments)
        {
            if (Getting_ != null)
                return Getting_(defaultResult, culture, text, arguments);
            return null;
        }
        /// <summary>
        ///  <para>Throws the Getting__ global event</para>
        /// 	<para>@Alias <c>GetHtml</c> and <c>FormatHtml</c></para>
        /// 	<para>Translates the given html applying string.Format(html, arguments.Select(a =&gt; HttpUtility.HtmlEncode(a))) to the current culture language. </para>
        /// 	<para>The html will be kept as it is, while arguments will be automatically HTML Encoded</para>
        /// </summary>
        /// <param name="html">The html to be translated</param>
        /// <param name="arguments">Custom arguments list to be passed to string.Format</param>
        /// <param name="culture">The culture this translation is being translated</param>
        /// <param name="defaultResult">The I18NComplete default result. Returning null will fallback on this result, otherwise return a custom value to oveerride this.</param>
        /// <returns>The translated formatted html as MvcHtmlString</returns>
        internal static MvcHtmlString OnGetting__(MvcHtmlString defaultResult, CultureInfo culture, string html, object[] arguments)
        {
            if (Getting__ != null)
                return Getting__(defaultResult, culture, html, arguments);
            return null;
        }

        /// <summary>
        ///  <para>Throws the Getting___ global event</para>
        /// 	<para>@Alias <c>GetRaw</c> and <c>FormatRaw</c></para>
        /// 	<para>Translates the given html applying string.Format(html, htmlArguments) to the current culture language. </para>
        /// 	<para>Warning! Neither the html nor the htmlArguments will be encoded whatsoever</para>
        /// </summary>
        /// <param name="html">The html to be translated</param>
        /// <param name="htmlArguments">The html arguments to be applied. Warning! The arguments will not be htmlEncoded!</param>
        /// <param name="culture">The culture this translation is being translated</param>
        /// <param name="defaultResult">The I18NComplete default result. Returning null will fallback on this result, otherwise return a custom value to oveerride this.</param>
        /// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
        internal static MvcHtmlString OnGetting___(MvcHtmlString defaultResult, CultureInfo culture, string html, object[] htmlArguments)
        {
            if (Getting___ != null)
                return Getting___(defaultResult, culture, html, htmlArguments);
            return null;
        }
        /// <summary>
        ///  <para>Throws the Getting___ global event</para>
        /// 	<para>@Alias <c>GetRaw</c> and <c>FormatRaw</c></para>
        /// 	<para>Translates the given html applying string.Format(html, arguments.Select(a =&gt; escapeArgumentFunc(a))) to the current culture language. </para>
        /// 	<para>For each argument the escape func will be called before applying the format</para>
        /// </summary>
        /// <param name="html">The text to be translated</param>
        /// <param name="htmlArguments">The html arguments to be applied. For each argument will apply the escape func!</param>
        /// <param name="escapeArgumentFunc">The func to be applied for each argument .i.e. <c>a =&gt; HttpUtility.HtmlAttributeEncode(a)</c></param>
        /// <param name="culture">The culture this translation is being translated</param>
        /// <param name="defaultResult">The I18NComplete default result. Returning null will fallback on this result, otherwise return a custom value to oveerride this.</param>
        /// <returns>The translated formatted text/HTML as MvcHtmlString</returns>
        internal static MvcHtmlString OnGetting___(MvcHtmlString defaultResult, CultureInfo culture, string html, Func<object, string> escapeArgumentFunc, object[] htmlArguments)
        {
            if (Getting___e != null)
                return Getting___e(defaultResult, culture, html, escapeArgumentFunc, htmlArguments);
            return null;
        }


        /// <summary>
        /// </summary>
        /// <param name="cultureName"></param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 14:55:19 GMT"/>
        public static int LCID(string cultureName)
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);
            return culture == null ? 0 : culture.LCID;
        }

        /// <summary>
        /// Spins through the language preferences in the supplied HttpRequest,
        /// returning the first complete or partial match on a loaded language.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 14:55:19 GMT"/>
        public static string GetBestLanguage(HttpRequest request, string fallback = null)
        {
            if (request.UserLanguages == null || request.UserLanguages.Length == 0)
            {
                return I18NComplete.DefaultWorkingLanguage;
            }

            foreach (string lang in request.UserLanguages)
            {
                string language = string.IsNullOrWhiteSpace(lang) ? I18NComplete.DefaultWorkingLanguage : lang;
                int languageHash = string.IsNullOrWhiteSpace(lang) ? I18NComplete.DefaultWorkingLanguageLCID : LCID(language);

                if (Localizations.ContainsKey(languageHash))
                    return lang;

                string fragment = language.Split('-')[0];
                if (Localizations.ContainsKey(fragment.GetHashCode()))
                    return fragment;
            }

            return fallback ?? I18NComplete.DefaultWorkingLanguage;
        }

        #endregion

    }
}
