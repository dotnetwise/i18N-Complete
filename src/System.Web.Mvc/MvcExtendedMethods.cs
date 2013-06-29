using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Web.WebPages;

namespace System.Web.Mvc
{
    /// <summary>The MvcExtendedMethods class</summary>
    /// <created author="laurentiu.macovei" date="Sat, 07 Jan 2012 18:15:27 GMT"/>
    public static class MvcExtendedMethods
    {

        #region Fields


        #region Const

        private static readonly object _o = new object();
        private static readonly HelperResult EmptyContent = new HelperResult(_ => { });

        #endregion Const


        #region Static

        static HelperResult Empty = new HelperResult(_ => { });
        private static PropertyInfo PreviousSectionWriters = typeof(WebPageBase).GetProperty("PreviousSectionWriters", BindingFlags.Instance | BindingFlags.NonPublic);
        private static PropertyInfo SectionWritersStack = typeof(WebPageBase).GetProperty("SectionWritersStack", BindingFlags.Instance | BindingFlags.NonPublic);

        #endregion Static


        #endregion Fields


        #region Util Methods

        /// <summary>
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Fri, 18 Mar 2011 02:23:32 GMT"/>
        private static Dictionary<string, SectionWriter> GetPreviousWriters(this WebPageBase page)
        {
            var sections = (Dictionary<string, SectionWriter>)PreviousSectionWriters.GetValue(page, null);
            return sections;
        }

        /// <summary>
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Fri, 18 Mar 2011 02:23:32 GMT"/>
        private static Stack<Dictionary<string, SectionWriter>> GetSectionWritersStack(this WebPageBase page)
        {
            var sections = (Stack<Dictionary<string, SectionWriter>>)SectionWritersStack.GetValue(page, null);
            return sections;
        }


        #endregion Util Methods

        #region Public Methods

        /// <summary>Encodes a string and returns it  as MvcHtmlString.</summary>
        /// <param name="text">A string to encode.</param>
        /// <param name="addDoubleQuotes">
        /// A value that indicates whether double quotation marks will be included around 
        /// the encoded string. 
        /// </param>
        /// <returns>An encoded string.</returns>
        /// <created author="laurentiu.macovei" date="Mon, 13 Feb 2012 15:40:38 GMT"/>
        public static MvcHtmlString js(this string text, bool addDoubleQuotes = true)
        {
            return new MvcHtmlString(HttpUtility.JavaScriptStringEncode(text, addDoubleQuotes));
        }

        /// <summary>Encodes a string and returns it as String.</summary>
        /// <param name="text">A string to encode.</param>
        /// <param name="addDoubleQuotes">
        /// A value that indicates whether double quotation marks will be included around 
        /// the encoded string. 
        /// </param>
        /// <returns>An encoded string.</returns>
        /// <created author="laurentiu.macovei" date="Mon, 13 Feb 2012 15:40:38 GMT"/>
        public static string Js(this string text, bool addDoubleQuotes = true)
        {
            return HttpUtility.JavaScriptStringEncode(text, addDoubleQuotes);
        }

        /// <summary>A Simple ActionLink Image</summary>
        /// <param name="actionName">name of the action in controller</param>
        /// <param name="imgUrl">url of the image</param>
        /// <param name="alt">alt text of the image</param>
        /// <param name="helper">The html helper to extend</param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Sat, 12 Nov 2011 20:22:14 GMT"/>
        public static MvcHtmlString ImageLink(this HtmlHelper helper, string actionName, string imgUrl, string alt)
        {
            return ImageLink(helper, actionName, imgUrl, alt, null, null, null);
        }

        /// <summary>A Simple ActionLink Image</summary>
        /// <param name="actionName">name of the action in controller</param>
        /// <param name="imgUrl">url of the iamge</param>
        /// <param name="alt">alt text of the image</param>
        /// <param name="helper">The HTML helper to extend</param>
        /// <param name="routeValues">An anonymous object with values to construct the route</param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Sat, 12 Nov 2011 20:22:15 GMT"/>
        public static MvcHtmlString ImageLink(this HtmlHelper helper, string actionName, string imgUrl, string alt, object routeValues)
        {
            return ImageLink(helper, actionName, imgUrl, alt, routeValues, null, null);
        }

        /// <summary>A Simple ActionLink Image</summary>
        /// <param name="actionName">name of the action in controller</param>
        /// <param name="imgUrl">url of the image</param>
        /// <param name="alt">alt text of the image</param>
        /// <param name="linkHtmlAttributes">attributes for the link</param>
        /// <param name="imageHtmlAttributes">attributes for the image</param>
        /// <param name="helper">The html helper being extended</param>
        /// <param name="routeValues">An anonymous object with values to construct the route</param>
        /// <created author="laurentiu.macovei" date="Sat, 12 Nov 2011 20:22:15 GMT"/>
        public static MvcHtmlString ImageLink(this HtmlHelper helper, string actionName, string imgUrl, string alt, object routeValues, object linkHtmlAttributes, object imageHtmlAttributes)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            var url = urlHelper.Action(actionName, routeValues);

            //Create the link
            var linkTagBuilder = new TagBuilder("a");
            linkTagBuilder.MergeAttribute("href", url);
            linkTagBuilder.MergeAttribute("title", urlHelper.Content(alt));
            linkTagBuilder.MergeAttributes(new RouteValueDictionary(linkHtmlAttributes));

            //Create image
            var imageTagBuilder = new TagBuilder("img");
            imageTagBuilder.MergeAttribute("src", urlHelper.Content(imgUrl));
            imageTagBuilder.MergeAttribute("alt", urlHelper.Content(alt));
            imageTagBuilder.MergeAttributes(new RouteValueDictionary(imageHtmlAttributes));

            //Add image to link
            linkTagBuilder.InnerHtml = imageTagBuilder.ToString(TagRenderMode.SelfClosing);

            return new MvcHtmlString(linkTagBuilder.ToString());
        }

        /// <summary>A Simple ActionLink Image</summary>
        /// <param name="url">name of the action in controller</param>
        /// <param name="imgUrl">url of the image</param>
        /// <param name="alt">alt text of the image</param>
        /// <param name="linkHtmlAttributes">attributes for the link</param>
        /// <param name="imageHtmlAttributes">attributes for the image</param>
        /// <param name="helper">Thge HTML Helper to extend</param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Sat, 12 Nov 2011 20:22:15 GMT"/>
        public static MvcHtmlString ImageLink(this HtmlHelper helper, string url, string imgUrl, string alt, object linkHtmlAttributes, object imageHtmlAttributes)
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            //Create the link
            var linkTagBuilder = new TagBuilder("a");
            linkTagBuilder.MergeAttribute("href", url);
            linkTagBuilder.MergeAttribute("title", urlHelper.Content(alt));
            linkTagBuilder.MergeAttributes(new RouteValueDictionary(linkHtmlAttributes));

            //Create image
            var imageTagBuilder = new TagBuilder("img");
            imageTagBuilder.MergeAttribute("src", urlHelper.Content(imgUrl));
            imageTagBuilder.MergeAttribute("alt", urlHelper.Content(alt));
            imageTagBuilder.MergeAttributes(new RouteValueDictionary(imageHtmlAttributes));

            //Add image to link
            linkTagBuilder.InnerHtml = imageTagBuilder.ToString(TagRenderMode.SelfClosing);

            return new MvcHtmlString(linkTagBuilder.ToString());
        }

        /// <summary>Returns the url updated with the specified language</summary>
        /// <param name="helper">The url helper</param>
        /// <param name="language">The language you want to create the change to</param>
        /// <created author="laurentiu.macovei" date="Sat, 12 Nov 2011 20:22:15 GMT"/>
        public static string ChangeLanguage(this UrlHelper helper, string language)
        {
            var url = helper.RequestContext.HttpContext.Request.Url.PathAndQuery ?? "";
            var match = CultureRegex.Match(url);
            if (match.Success)
                url = "/" + language + match.Groups["url"];
            else url = "/" + language + (url.StartsWith("/") ? url : "/" + url);
            return url;
        }
        private static Regex CultureRegex = new Regex(@"^\/(?<lang>..(\-..)?)(?<url>\/?.*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>Returns the value if the request came from a mobile browser</summary>
        /// <param name="request">The httpRequest to extend</param>
        /// <param name="mobile">The value to be returned if Request.Browser.IsMobileDevice</param>
        /// <param name="notMobile">The value to be returned if !Request.Browser.IsMobileDevice</param>
        /// <created author="laurentiu.macovei" date="Thu, 12 Jan 2012 17:45:19 GMT"/>
        public static T IsMobile<T>(this HttpRequestBase request, T mobile, T notMobile = default(T))
        {
            return request.Browser.IsMobileDevice ? mobile : notMobile;
        }
        /// <summary>Returns the value of the appropriate func if the request came from a mobile browser</summary>
        /// <param name="request">The httpRequest to extend</param>
        /// <param name="mobile">The func to be called if Request.Browser.IsMobileDevice</param>
        /// <param name="notMobile">The func to be called if !Request.Browser.IsMobileDevice</param>
        /// <created author="laurentiu.macovei" date="Fri, 23 Mar 2012 13:16:24 GMT"/>
        public static T IsMobile<T>(this HttpRequestBase request, Func<T> mobile, Func<T> notMobile = default(Func<T>))
        {
            return request.Browser.IsMobileDevice ? (mobile == null ? default(T) : mobile()) : (notMobile == null ? default(T) : notMobile());
        }
        /// <summary>Returns the value if the request did NOT come from a mobile browser</summary>
        /// <param name="request">The httpRequest to extend</param>
        /// <param name="mobile">The value to be returned if Request.Browser.IsMobileDevice</param>
        /// <param name="notMobile">The value to be returned if !Request.Browser.IsMobileDevice</param>
        /// <created author="laurentiu.macovei" date="Thu, 12 Jan 2012 17:46:55 GMT"/>
        public static T NotMobile<T>(this HttpRequestBase request, T notMobile, T mobile = default(T))
        {
            return request.Browser.IsMobileDevice ? mobile : notMobile;
        }

        /// <summary>Returns the mobileFunc's value if the request came from a mobile browser, otherwise notMobileFunc's value</summary>
        /// <param name="request">The httpRequest to extend</param>
        /// <param name="mobileFunc">The value to be returned if Request.Browser.IsMobileDevice</param>
        /// <param name="notMobileFunc">The value to be returned if NOT Request.Browser.IsMobileDevice</param>
        /// <created author="laurentiu.macovei" date="Thu, 12 Jan 2012 17:45:19 GMT"/>
        public static MvcHtmlString IsMobile(this HttpRequestBase request, Func<object, HelperResult> mobileFunc, Func<object, HelperResult> notMobileFunc = null)
        {
            return request.Browser.IsMobileDevice ? mobileFunc == null ? null : mobileFunc(_o).ToHtml() : notMobileFunc == null ? null : notMobileFunc(_o).ToHtml();
        }
        /// <summary>Returns the notMobileFunc's value if the request did NOT came from a mobile browser, otherwise mobileFunc's value</summary>
        /// <param name="request">The httpRequest to extend</param>
        /// <param name="mobileFunc">The value to be returned if Request.Browser.IsMobileDevice</param>
        /// <param name="notMobileFunc">The value to be returned if NOT Request.Browser.IsMobileDevice</param>
        /// <created author="laurentiu.macovei" date="Thu, 12 Jan 2012 17:45:19 GMT"/>
        public static MvcHtmlString NotMobile(this HttpRequestBase request, Func<object, HelperResult> notMobileFunc, Func<object, HelperResult> mobileFunc = null)
        {
            return request.Browser.IsMobileDevice ? mobileFunc == null ? null : mobileFunc(_o).ToHtml() : notMobileFunc == null ? null : notMobileFunc(_o).ToHtml();
        }
        /// <summary>Returns true if the request came from a mobile browser</summary>
        /// <param name="request">The httpRequest to extend</param>
        /// <created author="laurentiu.macovei" date="Thu, 12 Jan 2012 23:23:10 GMT"/>
        public static bool IsMobile(this HttpRequestBase request)
        {
            return request.Browser.IsMobileDevice;
        }
        /// <summary>Returns true if the request did not come from a mobile browser</summary>
        /// <param name="request">The httpRequest to extend</param>
        /// <created author="laurentiu.macovei" date="Thu, 26 Jan 2012 13:22:35 GMT"/>
        public static bool NotMobile(this HttpRequestBase request)
        {
            return !request.Browser.IsMobileDevice;
        }



        /// <summary>Returns the value if the request came from a mobile browser</summary>
        /// <param name="request">The httpRequest to extend</param>
        /// <param name="mobile">The value to be returned if Request.Browser.IsMobileDevice</param>
        /// <param name="notMobile">The value to be returned if !Request.Browser.IsMobileDevice</param>
        /// <created author="laurentiu.macovei" date="Thu, 26 Jan 2012 13:18:14 GMT"/>
        public static T IsMobile<T>(this HttpRequest request, T mobile, T notMobile = default(T))
        {
            return request.Browser.IsMobileDevice ? mobile : notMobile;
        }
        /// <summary>Returns the value if the request did NOT come from a mobile browser</summary>
        /// <param name="request">The httpRequest to extend</param>
        /// <param name="notMobile">The value to be returned if !Request.Browser.IsMobileDevice</param>
        /// <param name="mobile">The value to be returned if Request.Browser.IsMobileDevice</param>
        /// <created author="laurentiu.macovei" date="Thu, 26 Jan 2012 13:18:14 GMT"/>
        public static T NotMobile<T>(this HttpRequest request, T notMobile, T mobile = default(T))
        {
            return request.Browser.IsMobileDevice ? mobile : notMobile;
        }

        /// <summary>Returns the value of the appropriate func if the request did NOT come from a mobile browser</summary>
        /// <param name="request">The httpRequest to extend</param>
        /// <param name="notMobile">The func to be called if !Request.Browser.IsMobileDevice</param>
        /// <param name="mobile">The func to be called if Request.Browser.IsMobileDevice</param>
        /// <created author="laurentiu.macovei" date="Thu, 26 Jan 2012 13:18:14 GMT"/>
        public static T NotMobile<T>(this HttpRequest request, Func<T> notMobile, Func<T> mobile = default(Func<T>))
        {
            return request.Browser.IsMobileDevice ? (mobile == null ? default(T) : mobile()) : (notMobile == null ? default(T) : notMobile());
        }

        /// <summary>Returns the mobileFunc's value if the request came from a mobile browser, otherwise notMobileFunc's value</summary>
        /// <param name="request">The httpRequest to extend</param>
        /// <param name="mobileFunc">The value to be returned if Request.Browser.IsMobileDevice</param>
        /// <param name="notMobileFunc">The value to be returned if NOT Request.Browser.IsMobileDevice</param>
        /// <created author="laurentiu.macovei" date="Thu, 26 Jan 2012 13:18:14 GMT"/>
        public static MvcHtmlString IsMobile(this HttpRequest request, Func<object, HelperResult> mobileFunc, Func<object, HelperResult> notMobileFunc = null)
        {
            return request.Browser.IsMobileDevice ? mobileFunc == null ? null : mobileFunc(_o).ToHtml() : notMobileFunc == null ? null : notMobileFunc(_o).ToHtml();
        }
        /// <summary>Returns the notMobileFunc's value if the request did NOT came from a mobile browser, otherwise mobileFunc's value</summary>
        /// <param name="request">The httpRequest to extend</param>
        /// <param name="mobileFunc">The value to be returned if Request.Browser.IsMobileDevice</param>
        /// <param name="notMobileFunc">The value to be returned if NOT Request.Browser.IsMobileDevice</param>
        /// <created author="laurentiu.macovei" date="Thu, 26 Jan 2012 13:18:15 GMT"/>
        public static MvcHtmlString NotMobile(this HttpRequest request, Func<object, HelperResult> notMobileFunc, Func<object, HelperResult> mobileFunc = null)
        {
            return request.Browser.IsMobileDevice ? mobileFunc == null ? null : mobileFunc(_o).ToHtml() : notMobileFunc == null ? null : notMobileFunc(_o).ToHtml();
        }
        /// <summary>Returns true if the request came from a mobile browser</summary>
        /// <param name="request">The httpRequest to extend</param>
        /// <created author="laurentiu.macovei" date="Thu, 26 Jan 2012 13:18:15 GMT"/>
        public static bool IsMobile(this HttpRequest request)
        {
            return request.Browser.IsMobileDevice;
        }

        /// <summary>Returns the trueValue if the codition is true otherwise falseValue</summary>
        /// <param name="condition">The condition to be evaluated</param>
        /// <param name="trueValue">The value to be returned if codition == true</param>
        /// <param name="falseValue">The value to be returned if condition == false</param>
        /// <created author="laurentiu.macovei" date="Thu, 12 Jan 2012 19:53:05 GMT"/>
        public static MvcHtmlString True(this bool condition, Func<object, HelperResult> trueValue, Func<object, HelperResult> falseValue = null)
        {
            return condition ? trueValue == null ? null : trueValue(_o).ToHtml() : falseValue == null ? null : falseValue(_o).ToHtml();
        }
        /// <summary>Returns the value if the codition is false</summary>
        /// <param name="condition">The condition to be evaluated</param>
        /// <param name="trueValue">The value to be returned if codition == true</param>
        /// <param name="falseValue">The value to be returned if condition == false</param>
        /// <created author="laurentiu.macovei" date="Thu, 12 Jan 2012 19:53:05 GMT"/>
        public static MvcHtmlString False(this bool condition, Func<object, HelperResult> falseValue, Func<object, HelperResult> trueValue = null)
        {
            return condition ? trueValue == null ? null : trueValue(_o).ToHtml() : falseValue == null ? null : falseValue(_o).ToHtml();
        }
        /// <summary>Returns the trueValue if the codition is true otherwise falseValue</summary>
        /// <param name="condition">The condition to be evaluated</param>
        /// <param name="trueValue">The value to be returned if codition == true</param>
        /// <param name="falseValue">The value to be returned if condition == false</param>
        /// <created author="laurentiu.macovei" date="Fri, 30 Mar 2012 14:28:44 GMT"/>
        public static T True<T>(this bool condition, T trueValue, T falseValue = default(T))
        {
            return condition ? trueValue : falseValue;
        }
        /// <summary>Returns the value if the codition is false</summary>
        /// <param name="condition">The condition to be evaluated</param>
        /// <param name="trueValue">The value to be returned if codition == true</param>
        /// <param name="falseValue">The value to be returned if condition == false</param>
        /// <created author="laurentiu.macovei" date="Fri, 30 Mar 2012 14:28:44 GMT"/>
        public static T False<T>(this bool condition, T falseValue, T trueValue = default(T))
        {
            return condition ? trueValue : falseValue;
        }

        /// <summary>Converts an anonymous object to html attributes to be written inline a tag</summary>
        /// <param name="anonymousObject">The html attributes object</param>
        /// <param name="moreAttributes">Additionally you can specify other html attributes to be merged</param>
        /// <created author="laurentiu.macovei" date="Sat, 31 Mar 2012 16:25:38 GMT"/>
        public static HtmlString AsHtmlAttributes(this object anonymousObject, params object[] moreAttributes)
        {
            if (anonymousObject == null)
                return null;
            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(anonymousObject);
            var tagBuilder = new TagBuilder("a");
            tagBuilder.MergeAttributes(attrs);
            if (moreAttributes != null && moreAttributes.Length > 0)
                foreach (var item in moreAttributes)
                    if (item != null)
                        tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(item));
            var tag = tagBuilder.ToString(TagRenderMode.StartTag);
            var a = tag.Substring(2, tag.Length - 3);
            return new HtmlString(a);
        }

        /// <summary>Keeps the current ViewData.Model if not null OR sets the given model if null</summary>
        /// <param name="viewData">The viewData to fix the model into</param>
        /// <param name="model">The default empty model if the ViewData.Model is null</param>
        /// <created author="laurentiu.macovei" date="Thu, 12 Jan 2012 19:14:25 GMT"/>
        public static ViewDataDictionary<T> FixModel<T>(this ViewDataDictionary<T> viewData, T model)
            where T : class
        {
            if (viewData != null)
                viewData.Model = viewData.Model ?? model;
            return viewData;
        }

        /// <summary>Executes the helper and returns its MvcHtmlString output or null if the helper was null</summary>
        /// <param name="helper">The helper to execute the result of</param>
        /// <created author="laurentiu.macovei" date="Sat, 07 Jan 2012 18:15:29 GMT"/>
        public static MvcHtmlString ToHtml(this HelperResult helper)
        {
            return helper == null ? null : new MvcHtmlString(helper.ToHtmlString());
        }
        /// <summary>Renders the section with some new default content</summary>
        /// <param name="page"></param>
        /// <param name="sectionName"></param>
        /// <param name="defaultContent"></param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Sun, 09 Jan 2011 21:37:57 GMT"/>
        public static HelperResult RenderSection(this WebPageBase page,
                            string sectionName,
                            Func<object, HelperResult> defaultContent)
        {
            if (page.IsSectionDefined(sectionName))
            {
                return page.RenderSection(sectionName);
            }
            else
            {
                return defaultContent(_o);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sectionName"></param>
        /// <param name="defaultContent"></param>
        /// <param name="dependinces"></param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Fri, 18 Mar 2011 02:23:32 GMT"/>
        public static DependentSection DefaultSection(this WebPageBase page, string sectionName,
                            Func<object, HelperResult> defaultContent, params DependentSection[] dependinces)
        {
            if (page.IsSectionDefined(sectionName))
            {
                return null;
            }
            else
            {
                return new DependentSection { Name = sectionName, DefaultContent = defaultContent, Dependinces = dependinces };
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sectionName"></param>
        /// <param name="defaultContent"></param>
        /// <param name="dependinces"></param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Fri, 18 Mar 2011 02:23:32 GMT"/>
        public static HelperResult RenderSection(this WebPageBase page,
                            string sectionName,
                            Func<object, HelperResult> defaultContent,
                            params DependentSection[] dependinces
            )
        {
            if (page.IsSectionDefined(sectionName))
            {
                return page.RenderSection(sectionName);
            }
            else
            {
                if (dependinces != null && dependinces.Length > 0)
                {
                    var stack = page.GetSectionWritersStack().ToArray();
                    foreach (var section in dependinces)
                    {
                        section.DefineDefaults(page, stack);
                    }
                }
                return defaultContent(_o);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sectionName"></param>
        /// <param name="defaultContent"></param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Fri, 11 Mar 2011 21:47:47 GMT"/>
        public static HelperResult RenderSection(this WebPageBase page,
                            string sectionName,
                            string defaultContent)
        {
            if (page.IsSectionDefined(sectionName))
            {
                return page.RenderSection(sectionName);
            }
            else
            {
                return new HelperResult(a => a.Write(defaultContent));
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sectionName"></param>
        /// <param name="defaultContent"></param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Fri, 11 Mar 2011 21:59:50 GMT"/>
        public static HelperResult RenderSection(this WebPageBase page,
                            string sectionName,
                            MvcHtmlString defaultContent)
        {
            if (page.IsSectionDefined(sectionName))
            {
                return page.RenderSection(sectionName);
            }
            else
            {
                return new HelperResult(a => a.Write(defaultContent));
            }
        }

        /// <summary>Returns the MvcHtmlString of the given section if defined or the fallback default value</summary>
        /// <param name="page"></param>
        /// <param name="sectionName"></param>
        /// <param name="defaultContent"></param>
        /// <param name="dependinces"></param>
        /// <created author="laurentiu.macovei" date="Sun, 20 Mar 2011 02:55:35 GMT"/>
        public static MvcHtmlString RenderSectionAsHtml(this WebPageBase page,
                            string sectionName,
                            Func<object, HelperResult> defaultContent,
                            params DependentSection[] dependinces
            )
        {
            if (page.IsSectionDefined(sectionName))
            {
                return new MvcHtmlString(page.RenderSection(sectionName).ToHtmlString());
            }
            else
            {
                if (dependinces != null && dependinces.Length > 0)
                {
                    var stack = page.GetSectionWritersStack().ToArray();
                    foreach (var section in dependinces)
                    {
                        section.DefineDefaults(page, stack);
                    }
                }
                return new MvcHtmlString(defaultContent(_o).ToHtmlString());
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sectionName"></param>
        /// <param name="defaultContent"></param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Sun, 20 Mar 2011 02:55:35 GMT"/>
        public static MvcHtmlString RenderSectionAsHtml(this WebPageBase page,
                            string sectionName,
                            string defaultContent)
        {
            if (page.IsSectionDefined(sectionName))
            {
                return new MvcHtmlString(page.RenderSection(sectionName).ToHtmlString());
            }
            else
            {
                return new MvcHtmlString(defaultContent);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sectionName"></param>
        /// <param name="defaultContent"></param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Sun, 20 Mar 2011 02:55:35 GMT"/>
        public static MvcHtmlString RenderSectionAsHtml(this WebPageBase page,
                            string sectionName,
                            MvcHtmlString defaultContent)
        {
            if (page.IsSectionDefined(sectionName))
            {
                return new MvcHtmlString(page.RenderSection(sectionName).ToHtmlString());
            }
            else
            {
                return defaultContent;
            }
        }

        /// <summary>Redefines a section and keeps the old default content</summary>
        /// <param name="page"></param>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Sun, 09 Jan 2011 21:37:57 GMT"/>
        public static HelperResult RedefineSection(this WebPageBase page,
            string sectionName)
        {
            return RedefineSection(page, sectionName, defaultContent: null);
        }

        /// <summary>Redefines a section with some new "default content"</summary>
        /// <param name="page"></param>
        /// <param name="sectionName"></param>
        /// <param name="defaultContent"></param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Sun, 09 Jan 2011 21:37:58 GMT"/>
        public static HelperResult RedefineSection(this WebPageBase page,
                                string sectionName,
                                Func<object, HelperResult> defaultContent)
        {
            if (page.IsSectionDefined(sectionName))
            {
                page.DefineSection(sectionName,
                                   () => page.Write(page.RenderSection(sectionName)));
                //var hiddenResult = page.RenderSection(sectionName);
                //page.GetPreviousWriters[sectionName]
            }
            else if (defaultContent != null)
            {
                page.DefineSection(sectionName,
                                   () => page.Write(defaultContent(_o)));
            }
            return Empty;
        }

        private static readonly char[] Comma = { ',' };
        /// <summary>Deletes the comma separated sections so they will not be forwarded anymore, hence avoiding the error</summary>
        /// <param name="page"></param>
        /// <param name="sections"></param>
        /// <param name="onlyChildSections"></param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Fri, 18 Mar 2011 02:23:32 GMT"/>
        public static HelperResult PurgeSections(this WebPageBase page, string sections, bool onlyChildSections = false)
        {
            if (!string.IsNullOrEmpty(sections))
            {
                var previousWriters = page.GetPreviousWriters();
                var currentWriters = page.GetSectionWritersStack().Peek();
                foreach (var section in sections.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    previousWriters.Remove(sections);
                    if (!onlyChildSections)
                        currentWriters.Remove(sections);
                }
            }
            return Empty;
        }

        /// <summary>Redefines all the previous defined sections</summary>
        /// <param name="page"></param>
        /// <param name="includeSections">Specify which sections to be redefined. If null provided, all of them will be redefined</param>
        /// <param name="exceptSections">Specify which sections not to be redefined</param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Sat, 12 Mar 2011 01:25:40 GMT"/>
        public static HelperResult RedefineSections(this WebPageBase page, string includeSections = null, string exceptSections = null)
        {
            var previousWriters = page.GetPreviousWriters();
            var sections = includeSections == null ? (previousWriters == null ? null : previousWriters.Select(i => i.Key)) : includeSections.Split(Comma, StringSplitOptions.RemoveEmptyEntries);
            var except = exceptSections == null ? null : exceptSections.Split(Comma, StringSplitOptions.RemoveEmptyEntries);
            if (sections != null)
                foreach (var item in sections.Except(except ?? new string[0]))
                    page.RedefineSection(item);
            return new HelperResult(_ => { });
        }

        /// <summary>Encapsulates the given item as an IEnumerable&lt;T&gt;</summary>
        /// <param name="item">The item to be returned as an IEnumerable</param>
        /// <created author="laurentiu.macovei" date="Mon, 25 Jul 2011 23:20:01 GMT"/>
        public static IEnumerable<T> ToEnumerable<T>(this T item)
        {
            yield return item;
        }

        /// <summary>Sets the properties of another dictionary or anonymous object into this dictionary</summary>
        /// <param name="dictionary">The dictionary to set the values into</param>
        /// <param name="anonymousObject">An anonymous object or IDictionary&lt;string,object&gt; to copy the properties from</param>
        /// <returns>The original dictionary updated</returns>
        /// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 18:44:48 GMT"/>
        public static T SetValues<T>(this T dictionary, object anonymousObject)
            where T : IDictionary<string, object>
        {
            if (anonymousObject != null && dictionary != null)
            {
                var dic = anonymousObject as IDictionary<string, object>;
                if (dic != null)
                {
                    foreach (var item in dic)
                        dictionary[item.Key] = item.Value;
                }
                else
                {
                    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(anonymousObject))
                    {
                        object obj2 = descriptor.GetValue(anonymousObject);
                        dictionary[descriptor.Name] = obj2;
                    }
                }
            }
            return dictionary;
        }
        /// <summary>Sets the properties of another dictionaries or anonymous objects into this dictionary</summary>
        /// <param name="dictionary">The dictionary to set the values into</param>
        /// <param name="anonymousObjects">Any anonymous objects or IDictionary&lt;string,object&gt; to copy the properties from</param>
        /// <returns>The original dictionary updated</returns>
        /// <created author="laurentiu.macovei" date="Fri, 06 Jan 2012 18:49:11 GMT"/>
        public static T SetValues<T>(this T dictionary, params object[] anonymousObjects)
            where T : IDictionary<string, object>
        {
            if (anonymousObjects != null && dictionary != null)
            {
                foreach (var anonymousObject in anonymousObjects)
                {

                    var dic = anonymousObject as IDictionary<string, object>;
                    if (dic != null)
                    {
                        foreach (var item in dic)
                            dictionary[item.Key] = item.Value;
                    }
                    else
                    {
                        foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(anonymousObject))
                        {
                            object obj2 = descriptor.GetValue(anonymousObject);
                            dictionary[descriptor.Name] = obj2;
                        }
                    }
                }
            }
            return dictionary;
        }
        /// <summary>Sets the properties of another dictionary or anonymous object into this dictionary</summary>
        /// <param name="dictionary">The dictionary to set the values into</param>
        /// <param name="htmlAttributes">Specify true in order to detect the css class properties and concatenate them</param>
        /// <param name="anonymousObject">An anonymous object or IDictionary&lt;string,object&gt; to copy the properties from</param>
        /// <returns>The original dictionary updated</returns>
        /// <created author="laurentiu.macovei" date="Fri, 13 Jan 2012 02:35:09 GMT"/>
        public static T SetValues<T>(this T dictionary, bool htmlAttributes, object anonymousObject)
            where T : IDictionary<string, object>
        {
            if (!htmlAttributes)
                return SetValues(dictionary, anonymousObject);
            if (anonymousObject != null && dictionary != null)
            {
                var dic = anonymousObject as IDictionary<string, object>;
                if (dic != null)
                {
                    foreach (var item in dic)
                        dictionary[item.Key] = item.Value;
                }
                else
                {
                    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(anonymousObject))
                    {
                        object obj2 = descriptor.GetValue(anonymousObject);
                        dictionary[descriptor.Name.Replace('_', '-')] = obj2;
                    }
                }
            }
            return dictionary;
        }
        /// <summary>Sets the properties of another dictionaries or anonymous objects into this dictionary</summary>
        /// <param name="dictionary">The dictionary to set the values into</param>
        /// <param name="htmlAttributes">The dictionary to get the values from</param>
        /// <param name="anonymousObjects">Any anonymous objects or IDictionary&lt;string,object&gt; to copy the properties from</param>
        /// <returns>The original dictionary updated</returns>
        /// <created author="laurentiu.macovei" date="Fri, 13 Jan 2012 02:35:09 GMT"/>
        public static T SetValues<T>(this T dictionary, bool htmlAttributes, params object[] anonymousObjects)
            where T : IDictionary<string, object>
        {
            if (!htmlAttributes)
                return SetValues(dictionary, anonymousObjects);
            if (anonymousObjects != null && dictionary != null)
            {
                foreach (var anonymousObject in anonymousObjects)
                {

                    var dic = anonymousObject as IDictionary<string, object>;
                    if (dic != null)
                    {
                        foreach (var item in dic)
                        {
                            var key = item.Key;
                            var value = item.Value;
                            dictionary[key] = "class".Equals(key, StringComparison.OrdinalIgnoreCase)
                                ? (string.IsNullOrWhiteSpace(value as string) || string.IsNullOrWhiteSpace(dictionary[key] as string) ? value : dictionary[key] as string + " " + (value ?? string.Empty).ToString())
                                : value;
                        }

                    }
                    else
                    {
                        foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(anonymousObject))
                        {
                            var key = descriptor.Name;
                            var value = descriptor.GetValue(anonymousObject);
                            dictionary[key.Replace('_', '-')] = "class".Equals(key, StringComparison.OrdinalIgnoreCase)
                                ? (string.IsNullOrWhiteSpace(value as string) || string.IsNullOrWhiteSpace(dictionary[key] as string) ? value : dictionary[key] as string + " " + (value ?? string.Empty).ToString())
                                : value;
                        }
                    }
                }
            }
            return dictionary;
        }


        /// <summary>Creates a form context in a partial view so that the unobtrusive validation will work</summary>
        /// <param name="helper"></param>
        /// <created author="laurentiu.macovei" date="Fri, 13 Jan 2012 04:24:37 GMT"/>
        public static void EnablePartialViewValidation(this HtmlHelper helper)
        {
            if (helper.ViewContext.FormContext == null)
            {
                helper.ViewContext.FormContext = new FormContext();
            }
        }

        /// <summary>
        /// Returns an anchor element (a element) that contains the virtual path of the
        /// specified action.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="linkText">The inner text of the anchor element.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="area">Specify the areas name where the controller resides <para>Specify "" - empty string for the root controllers</para></param>
        /// <returns>An anchor element (a element).</returns>
        /// <exception cref="System.ArgumentException">The linkText parameter is null or empty.</exception>
        /// <created author="laurentiu.macovei" date="Wed, 25 Jan 2012 14:56:26 GMT"/>
        public static MvcHtmlString ActionLinkA(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string area)
        {
            var routeValuesMerged = new RouteValueDictionary() { { "area", area } };
            var vpd = GetVirtualPathData(htmlHelper, actionName, controllerName, routeValuesMerged, true);
            return htmlHelper.ActionLink(linkText, actionName, controllerName,
                vpd.GetScheme(), vpd.GetHostName(), vpd.GetFragment(), 
                routeValuesMerged, null);
        }
        /// <summary>
        /// Returns `scheme` || `protocol `from the given Virtual path data
        /// </summary>
        /// <param name="vpd">The virtual path data to get the data from</param>
        public static string GetScheme(this VirtualPathData vpd)
        {
            return vpd == null || vpd.Route == null || vpd.DataTokens == null ? null
                : (vpd.DataTokens["scheme"] as string ?? vpd.DataTokens["protocol"] as string);
        }
        /// <summary>
        /// Returns `hostname` from the given Virtual path data
        /// </summary>
        /// <param name="vpd">The virtual path data to get the data from</param>
        public static string GetHostName(this VirtualPathData vpd)
        {
            return vpd == null || vpd.Route == null || vpd.DataTokens == null ? null
                : (vpd.DataTokens["hostname"] as string);
        }

        /// <summary>
        /// Returns `fragment` || `hash` from the given Virtual path data
        /// </summary>
        /// <param name="vpd">The virtual path data to get the data from</param>
        public static string GetFragment(this VirtualPathData vpd)
        {
            return vpd == null || vpd.Route == null || vpd.DataTokens == null ? null
                : (vpd.DataTokens["fragment"] as string ?? vpd.DataTokens["hash"] as string);
        }

        /// <summary>
        /// Returns an anchor element (a element) that contains the virtual path of the
        /// specified action.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="linkText">The inner text of the anchor element.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="routeValues">
        /// An object that contains the parameters for a route. The parameters are retrieved
        /// through reflection by examining the properties of the object. The object
        /// is typically created by using object initializer syntax.
        /// </param>
        /// <param name="controllerName">
        /// An object that contains the HTML attributes for the element. The attributes
        /// are retrieved through reflection by examining the properties of the object.
        /// The object is typically created by using object initializer syntax.
        /// </param>
        /// <returns>An anchor element (a element).</returns>
        /// <created author="laurentiu.macovei" date="Sat, 14 Jan 2012 01:07:14 GMT"/>
        public static MvcHtmlString ActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues)
        {

            return htmlHelper.ActionLink(linkText, actionName, controllerName: controllerName, routeValues: routeValues, htmlAttributes: null);
        }
        /// <summary>
        /// Returns an anchor element (a element) that contains the virtual path of the
        /// specified action.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="linkText">The inner text of the anchor element.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">
        /// An object that contains the parameters for a route. The parameters are retrieved
        /// through reflection by examining the properties of the object. The object
        /// is typically created by using object initializer syntax.
        /// </param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <param name="area">Specify the areas name where the controller resides <para>Specify "" - empty string for the root controllers</para></param>
        /// <returns>An anchor element (a element).</returns>
        /// <exception cref="System.ArgumentException">The linkText parameter is null or empty.</exception>
        /// <created author="laurentiu.macovei" date="Wed, 25 Jan 2012 14:56:26 GMT"/>
        public static MvcHtmlString ActionLinkA(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string area, object routeValues = null, object htmlAttributes = null)
        {
            var routeValuesMerged = new RouteValueDictionary(routeValues) { { "area", area } };
            var vpd = GetVirtualPathData(htmlHelper, actionName, controllerName, routeValuesMerged, true);

            return htmlHelper.ActionLink(linkText, actionName, controllerName,
                protocol: vpd.GetScheme(), 
                hostName: vpd.GetHostName(),
                fragment: vpd.GetFragment(),
                htmlAttributes: htmlAttributes, 
                routeValues: routeValuesMerged);
        }

        /// <summary>
        /// Returns an anchor element (a element) that contains the virtual path of the
        /// specified action.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="linkText">The inner text of the anchor element.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">An object that contains the parameters for a route.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <param name="area">Specify the areas name where the controller resides <para>Specify "" - empty string for the root controllers</para></param>
        /// <returns>An anchor element (a element).</returns>
        /// <exception cref="System.ArgumentException">The linkText parameter is null or empty.</exception>
        /// <created author="laurentiu.macovei" date="Wed, 25 Jan 2012 14:56:26 GMT"/>
        public static MvcHtmlString ActionLinkA(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string area, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            var routeValuesMerged = new RouteValueDictionary(routeValues) { { "area", area } };
            var vpd = GetVirtualPathData(htmlHelper, actionName, controllerName, routeValuesMerged, true);

            return htmlHelper.ActionLink(linkText, actionName, controllerName,
                protocol: vpd.GetScheme(),
                hostName: vpd.GetHostName(),
                fragment: vpd.GetFragment(),
                htmlAttributes: htmlAttributes,
                routeValues: routeValuesMerged);
        }

        /// <summary>
        /// Returns an anchor element (a element) that contains the virtual path of the
        /// specified action.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="linkText">The inner text of the anchor element.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="protocol">The protocol for the URL, such as "http" or "https".</param>
        /// <param name="hostName">The host name for the URL.</param>
        /// <param name="fragment">The URL fragment name (the anchor name).</param>
        /// <param name="routeValues">
        /// An object that contains the parameters for a route. The parameters are retrieved
        /// through reflection by examining the properties of the object. The object
        /// is typically created by using object initializer syntax.
        /// </param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <param name="area">Specify the areas name where the controller resides <para>Specify "" - empty string for the root controllers</para></param>
        /// <returns>An anchor element (a element).</returns>
        /// <exception cref="System.ArgumentException">The linkText parameter is null or empty.</exception>
        /// <created author="laurentiu.macovei" date="Wed, 25 Jan 2012 14:56:26 GMT"/>
        public static MvcHtmlString ActionLinkA(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string area, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
        {
            return htmlHelper.ActionLink(linkText, actionName, controllerName, protocol, hostName, fragment, htmlAttributes: htmlAttributes, routeValues: new RouteValueDictionary(routeValues) { { "area", area } });
        }

        /// <summary>
        /// Returns an anchor element (a element) that contains the virtual path of the
        /// specified action.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="linkText">The inner text of the anchor element.</param>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="protocol">The protocol for the URL, such as "http" or "https".</param>
        /// <param name="hostName">The host name for the URL.</param>
        /// <param name="fragment">The URL fragment name (the anchor name).</param>
        /// <param name="routeValues">An object that contains the parameters for a route.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <param name="area">Specify the areas name where the controller resides <para>Specify "" - empty string for the root controllers</para></param>
        /// <returns>An anchor element (a element).</returns>
        /// <exception cref="System.ArgumentException">The linkText parameter is null or empty.</exception>
        /// <created author="laurentiu.macovei" date="Wed, 25 Jan 2012 14:56:26 GMT"/>
        public static MvcHtmlString ActionLinkA(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string area, string protocol, string hostName, string fragment, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.ActionLink(linkText, actionName, controllerName, protocol, hostName, fragment, htmlAttributes: htmlAttributes, routeValues: new RouteValueDictionary(routeValues) { { "area", area } });
        }

        /// <summary>
        /// Returns route's virtual path data, including route and DataTokens
        /// </summary>
        /// <param name="htmlHelper">An HTML helper to get the virtual path data for</param>
        /// <param name="actionName">The name of the action</param>
        /// <param name="controllerName">The name of the controller</param>
        /// <param name="routeValues">Custom other route values data</param>
        /// <param name="includeImplicitMvcValues">Specify whther to merge route-configured implicict values</param>
        public static VirtualPathData GetVirtualPathData(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues, bool includeImplicitMvcValues)
        {
            var requestContext = htmlHelper.ViewContext.RequestContext;
            var values = MergeRouteValues(actionName, controllerName, requestContext.RouteData.Values, routeValues, true);
            var vpd = htmlHelper.RouteCollection.GetVirtualPath(requestContext, values);
            return vpd;
        }
        /// <summary>
        /// Returns route's virtual path data, including route and DataTokens
        /// </summary>
        /// <param name="urlHelper">An URL helper to get the virtual path data for</param>
        /// <param name="actionName">The name of the action</param>
        /// <param name="controllerName">The name of the controller</param>
        /// <param name="routeValues">Custom other route values data</param>
        /// <param name="includeImplicitMvcValues">Specify whther to merge route-configured implicict values</param>
        public static VirtualPathData GetVirtualPathData(this UrlHelper urlHelper, string actionName, string controllerName, ref RouteValueDictionary routeValues, bool includeImplicitMvcValues)
        {
            var requestContext = urlHelper.RequestContext;
            var values = MergeRouteValues(actionName, controllerName, requestContext.RouteData.Values, routeValues, true);
            var vpd = urlHelper.RouteCollection.GetVirtualPath(requestContext, values);
            routeValues = values;
            return vpd;
        }
        /// <summary>
        /// Merge multiple route values together into a RouteValueDictionary
        /// </summary>
        /// <param name="actionName">The name of the action</param>
        /// <param name="controllerName">The name of the controller</param>
        /// <param name="implicitRouteValues">Route's implicit values data</param>
        /// <param name="routeValues">Custom other route values data</param>
        /// <param name="includeImplicitMvcValues">Specify whther to merge route-configured implicict values</param>
        /// <returns></returns>
        public static RouteValueDictionary MergeRouteValues(string actionName, string controllerName, RouteValueDictionary implicitRouteValues, RouteValueDictionary routeValues, bool includeImplicitMvcValues)
        {
            // Create a new dictionary containing implicit and auto-generated values
            RouteValueDictionary mergedRouteValues = new RouteValueDictionary();

            if (includeImplicitMvcValues)
            {
                // We only include MVC-specific values like 'controller' and 'action' if we are generating an action link.
                // If we are generating a route link [as to MapRoute("Foo", "any/url", new { controller = ... })], including
                // the current controller name will cause the route match to fail if the current controller is not the same
                // as the destination controller.

                object implicitValue;
                if (implicitRouteValues != null && implicitRouteValues.TryGetValue("action", out implicitValue))
                {
                    mergedRouteValues["action"] = implicitValue;
                }

                if (implicitRouteValues != null && implicitRouteValues.TryGetValue("controller", out implicitValue))
                {
                    mergedRouteValues["controller"] = implicitValue;
                }
            }

            // Merge values from the user's dictionary/object
            if (routeValues != null)
            {
                foreach (KeyValuePair<string, object> routeElement in GetRouteValues(routeValues))
                {
                    mergedRouteValues[routeElement.Key] = routeElement.Value;
                }
            }

            // Merge explicit parameters when not null
            if (actionName != null)
            {
                mergedRouteValues["action"] = actionName;
            }

            if (controllerName != null)
            {
                mergedRouteValues["controller"] = controllerName;
            }

            return mergedRouteValues;
        }

        /// <summary>
        /// Returns a new RouteValuesDictionary if the given routeValues is null
        /// </summary>
        /// <param name="routeValues"></param>
        /// <returns></returns>
        public static RouteValueDictionary GetRouteValues(RouteValueDictionary routeValues)
        {
            return (routeValues != null) ? new RouteValueDictionary(routeValues) : new RouteValueDictionary();
        }
        /// <summary>
        /// Maps a Localized route setting route.RouteHandler = new LocalizedMvcRouteHandler(); and route.DataTokens = dataTokens
        /// </summary>
        /// <param name="routes"> A collection of routes for the application.</param>
        /// <param name="name">The name of the route to map.</param>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">An object that contains default route values.</param>
        /// <param name="constraints">A set of expressions that specify values for the url parameter.</param>
        /// <param name="namespaces">A set of namespaces for the application.</param>
        /// <param name="dataTokens">Custom route.DataTokens to be set</param>
        /// <param name="autoSetLocalizedRouteHandler">Automatically attach a new LocalizedMvcRouteHandler if routeHandler is null or not specified</param>
        /// <param name="routeHandler">Specify a custom route handler</param>
        /// <returns></returns>
        public static Route MapLocalizedRoute(this RouteCollection routes, string name, string url, object defaults = null, object constraints = null, string[] namespaces = null, object dataTokens = null, IRouteHandler routeHandler = null, bool autoSetLocalizedRouteHandler = true)
        {
            var route = routes.MapRoute(name, url, defaults, constraints, namespaces);
            if (autoSetLocalizedRouteHandler && routeHandler == null)
                routeHandler = new LocalizedMvcRouteHandler();
            route.RouteHandler = routeHandler;
            var data = dataTokens is RouteValueDictionary ? dataTokens as RouteValueDictionary : new RouteValueDictionary(dataTokens);
            data["name"] = name;
            foreach (var token in data)
                route.DataTokens[token.Key] = token.Value;
            return route;
        }

        /// <summary>
        /// Maps a Localized route setting route.RouteHandler = new LocalizedMvcRouteHandler(); and route.DataTokens = dataTokens
        /// </summary>
        /// <param name="routes"> A collection of routes for the application.</param>
        /// <param name="name">The name of the route to map.</param>
        /// <param name="url">The URL pattern for the route.</param>
        /// <param name="defaults">An object that contains default route values.</param>
        /// <param name="constraints">A set of expressions that specify values for the url parameter.</param>
        /// <param name="namespaces">A set of namespaces for the application.</param>
        /// <param name="dataTokens">Custom route.DataTokens to be set</param>
        /// <param name="autoSetLocalizedRouteHandler">Automatically attach a new LocalizedMvcRouteHandler if routeHandler is null or not specified</param>
        /// <param name="routeHandler">Specify a custom route handler</param>
        /// <returns></returns>
        public static Route MapLocalizedRoute(this AreaRegistrationContext routes, string name, string url, object defaults = null, object constraints = null, string[] namespaces = null, object dataTokens = null, IRouteHandler routeHandler = null, bool autoSetLocalizedRouteHandler = true)
        {
            var route = routes.MapRoute(name, url, defaults, constraints, namespaces);
            if (autoSetLocalizedRouteHandler && routeHandler == null)
                routeHandler = new LocalizedMvcRouteHandler();
            route.RouteHandler = routeHandler;
            var data = dataTokens is RouteValueDictionary ? dataTokens as RouteValueDictionary : new RouteValueDictionary(dataTokens);
            data["name"] = name;
            foreach (var token in data)
                route.DataTokens[token.Key] = token.Value;
            return route;
        }
        /// <summary>
        ///     Generates a fully qualified URL to an action method by using the specified
        ///     action name, controller name, and route values.
        /// </summary>
        /// <param name="url">The url being extended</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="area">The area name. "" or null to get main controllers action's url</param>
        /// <returns>The fully qualified URL to an action method.</returns>
        /// <created author="laurentiu.macovei" date="Thu, 19 Jan 2012 14:51:44 GMT"/>
        public static string ActionA(this UrlHelper url, string actionName, string controllerName, string area)
        {
            //return url.Action(actionName, controllerName, new RouteValueDictionary() { { "area", area } });
            var routeValuesMerged = new RouteValueDictionary() { { "area", area } };
            var vpd = GetVirtualPathData(url, actionName, controllerName, ref routeValuesMerged, true);
            
            return url.Action(actionName, controllerName, routeValues: routeValuesMerged, protocol: vpd.GetScheme(), hostName: vpd.GetHostName());
        }
        /// <summary>
        ///     Generates a fully qualified URL to an action method by using the specified
        ///     action name, controller name, and route values.
        /// </summary>
        /// <param name="url">The url being extended</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="area">The area name. "" or null to get main controllers action's url</param>
        /// <param name="routeValues">
        ///  An object that contains the parameters for a route. The parameters are retrieved
        ///  through reflection by examining the properties of the object. The object
        ///  is typically created by using object initializer syntax.
        /// </param>
        /// <returns>The fully qualified URL to an action method.</returns>
        /// <created author="laurentiu.macovei" date="Thu, 19 Jan 2012 14:51:44 GMT"/>
        public static string ActionA(this UrlHelper url, string actionName, string controllerName, string area, object routeValues)
        {
            var routeValuesMerged = new RouteValueDictionary(routeValues) { { "area", area } };
            var vpd = GetVirtualPathData(url, actionName, controllerName, ref routeValuesMerged, true);

            return url.Action(actionName, controllerName,
                routeValues: routeValuesMerged,
                protocol: vpd.GetScheme(),
                hostName: vpd.GetHostName());
        }

        /// <summary>
        /// Generates a fully qualified URL to an action method by using the specified
        /// action name, controller name, route values.
        /// </summary>
        /// <param name="url">The url being extended</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">An object that contains the parameters for a route.</param>
        /// <param name="area">The area name. "" or null to get main controllers action's url</param>
        /// <returns>The fully qualified URL to an action method.</returns>
        /// <created author="laurentiu.macovei" date="Thu, 19 Jan 2012 14:51:44 GMT"/>
        public static string ActionA(this UrlHelper url, string actionName, string controllerName, string area, RouteValueDictionary routeValues)
        {
            var routeValuesMerged = new RouteValueDictionary(routeValues) { { "area", area } };
            var vpd = GetVirtualPathData(url, actionName, controllerName, ref routeValuesMerged, true);

            return url.Action(actionName, controllerName,
                routeValues: routeValuesMerged,
                protocol: vpd.GetScheme(),
                hostName: vpd.GetHostName());
        }
        /// <summary>
        ///     Generates a fully qualified URL to an action method by using the specified
        ///     action name, controller name, route values, and protocol to use.
        /// </summary>
        /// <param name="url">The url being extended</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="routeValues">
        ///  An object that contains the parameters for a route. The parameters are retrieved
        ///  through reflection by examining the properties of the object. The object
        ///  is typically created by using object initializer syntax.
        /// </param>
        /// <param name="area">The area name. "" or null to get main controllers action's url</param>
        /// <param name="protocol">The protocol for the URL, such as "http" or "https".</param>
        /// <returns>The fully qualified URL to an action method.</returns>
        /// <created author="laurentiu.macovei" date="Thu, 19 Jan 2012 14:51:44 GMT"/>
        public static string ActionA(this UrlHelper url, string actionName, string controllerName, string area, object routeValues, string protocol)
        {
            var routeValuesMerged = new RouteValueDictionary(routeValues) { { "area", area } };
            var vpd = GetVirtualPathData(url, actionName, controllerName, ref routeValuesMerged, true);
            
            return url.Action(actionName, controllerName, routeValues: routeValuesMerged, protocol: protocol, hostName: vpd.GetHostName());
        }

        /// <summary>
        /// Generates a fully qualified URL to an action method by using the specified
        /// action name, controller name, route values, protocol to use, and host name.
        /// </summary>
        /// <param name="url">The url being extended</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">An object that contains the parameters for a route.</param>
        /// <param name="area">The area name. "" or null to get main controllers action's url</param>
        /// <param name="protocol">The protocol for the URL, such as "http" or "https".</param>
        /// <param name="hostName">The host name for the URL.</param>
        /// <returns>The fully qualified URL to an action method.</returns>
        /// <created author="laurentiu.macovei" date="Thu, 19 Jan 2012 14:51:44 GMT"/>
        public static string ActionA(this UrlHelper url, string actionName, string controllerName, string area, RouteValueDictionary routeValues, string protocol, string hostName)
        {
            return url.Action(actionName, controllerName, new RouteValueDictionary(routeValues) { { "area", area } }, protocol, hostName);
        }

        /// <summary>
        /// Returns an unordered list (ul element) of validation messages that are in
        /// the System.Web.Mvc.ModelStateDictionary object and optionally displays only
        /// model-level errors.
        /// </summary>
        /// <param name="html">The HTML helper instance that this method extends.</param>
        /// <param name="excludeProperties">true to have the summary display model-level errors only, or false to have the summary display all errors.</param>
        /// <param name="errorMessage">The message to display with the validation summary when isValid is false or isValid==null &amp;&amp; ModelState.IsValid is false</param>
        /// <param name="successMessage">The message to display with the validation summary when isValid is true or isValid==null &amp;&amp; ModelState.IsValid is true</param>
        /// <param name="htmlAttributes">An anonymous object or a dictionary that contains the HTML attributes for the element.</param>
        /// <param name="displaySuccessMessage"></param>
        /// <param name="isValid"></param>
        /// <param name="lookFirstInViewBag">Specify whether to look first in the ViewBag. 
        /// <para>Key names:</para>
        /// <para>ValidationSummaryError</para>
        /// <para>ValidationSummarySuccess</para>
        /// <para>ValidationSummaryOperation</para>
        /// <para>ValidationSummaryDisplay</para>
        /// </param>
        /// <param name="invalidMessage">The invalid message is displayed when client-side validation fails</param>
        /// <returns></returns>
        /// <created author="laurentiu.macovei" date="Thu, 19 Jan 2012 14:51:45 GMT"/>
        public static MvcHtmlString ValidationSummaryEnhanced(this HtmlHelper html, bool excludeProperties = false, object htmlAttributes = null, string errorMessage = null, string successMessage = null, string invalidMessage = null, bool? displaySuccessMessage = null, bool? isValid = null, bool lookFirstInViewBag = true)
        {
            if (lookFirstInViewBag)
            {
                var viewBag = html.ViewData;
                errorMessage = viewBag["ValidationSummaryError"] as string ?? errorMessage;
                successMessage = viewBag["ValidationSummarySuccess"] as string ?? successMessage;
                invalidMessage = viewBag["ValidationSummaryInvalid"] as string ?? invalidMessage;
                displaySuccessMessage = (bool?)viewBag["ValidationSummaryDisplay"] ?? displaySuccessMessage ?? false;
            }
            else
            {
                var viewBag = html.ViewData;
                errorMessage = errorMessage ?? viewBag["ValidationSummaryError"] as string;
                successMessage = successMessage ?? viewBag["ValidationSummarySuccess"] as string;
                invalidMessage = invalidMessage ?? viewBag["ValidationSummaryInvalid"] as string;
                displaySuccessMessage = displaySuccessMessage ?? (bool?)viewBag["ValidationSummaryDisplay"] ?? false;
            }
            var htmlAttribDictionary = htmlAttributes as IDictionary<string, object>;
            var dictionary = htmlAttribDictionary != null ? new RouteValueDictionary(htmlAttribDictionary) : HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            isValid = isValid ?? html.ViewData.ModelState.IsValid;
            var @class = dictionary["class"] as string;
            @class = @class + (string.IsNullOrEmpty(@class) ? "" : " ") + "formee-msg" +
                (isValid.Value
                    ? (displaySuccessMessage.Value || !"get".Equals(html.ViewContext.RequestContext.HttpContext.Request.HttpMethod, StringComparison.OrdinalIgnoreCase) ? " success" : "")
                    : " error");
            dictionary["class"] = @class;

            dictionary["data-msg-invalid"] = invalidMessage;
            dictionary["data-msg-error"] = errorMessage;
            dictionary["data-msg-success"] = successMessage;
            var message = isValid.Value ?
                successMessage
                //(displaySuccessMessage.Value ? successMessage : null) 
                : errorMessage;
            return html.ValidationSummary(excludeProperties, message, dictionary);
        }
        #endregion Public Methods


        #region DependentSection Class
        /// <summary>The DependentSection class</summary>
        /// <created author="laurentiu.macovei" date="Fri, 18 Mar 2011 02:23:33 GMT"/>
        public class DependentSection
        {

            #region Properties

            /// <summary>Gets or sets the Name</summary>
            /// <created author="laurentiu.macovei" date="Fri, 18 Mar 2011 02:23:33 GMT"/>
            public string Name { get; set; }

            /// <summary>Gets or sets the DefaultContent</summary>
            /// <created author="laurentiu.macovei" date="Fri, 18 Mar 2011 02:23:33 GMT"/>
            public Func<object, HelperResult> DefaultContent { get; set; }

            /// <summary>Gets or sets the Dependinces</summary>
            /// <created author="laurentiu.macovei" date="Fri, 18 Mar 2011 02:23:33 GMT"/>
            public DependentSection[] Dependinces { get; set; }


            #endregion Properties


            #region Public Methods

            /// <summary>
            /// </summary>
            /// <param name="page"></param>
            /// <param name="previousWriters"></param>
            /// <created author="laurentiu.macovei" date="Fri, 18 Mar 2011 02:23:33 GMT"/>
            internal void DefineDefaults(WebPageBase page, Dictionary<string, SectionWriter>[] previousWriters)
            {
                foreach (var previousWriter in previousWriters)
                {
                    if (!previousWriter.ContainsKey(Name))
                    //if (!page.IsSectionDefined(Name))
                    {
                        if (Dependinces != null && Dependinces.Length > 0)
                            foreach (var section in Dependinces)
                                section.DefineDefaults(page, previousWriters);
                        previousWriter[Name] = () => page.Write(DefaultContent(_o));
                        //page.DefineSection(Name, () => page.Write(DefaultContent(_o)));
                    }
                }
            }


            #endregion Public Methods


        }
        #endregion Class  DependentSection

    }
}
