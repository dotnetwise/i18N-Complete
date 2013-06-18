using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Routing;
using System.Web.Security;

namespace System.Web.Mvc
{
	/// <summary>The LocalizedMvcRouteHandler class</summary>
	/// <created author="laurentiu.macovei" date="Sat, 12 Nov 2011 00:13:18 GMT"/>
	public class LocalizedMvcRouteHandler
		: MvcRouteHandler
	{

		#region Override Methods

		/// <summary>Initializes the thread culture based on the current requested language</summary>
		/// <param name="requestContext"></param>
		/// <created author="laurentiu.macovei" date="Sat, 12 Nov 2011 00:13:18 GMT"/>
		protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			var language = requestContext.RouteData.Values["language"] as string;
			if (!string.IsNullOrWhiteSpace(language)
				&& (!LocalizationAppConfig.SupportedLanguages.Any() || LocalizationAppConfig.SupportedLanguages.Contains(language, StringComparer.OrdinalIgnoreCase)))
				try
				{
					var request =requestContext.HttpContext.Request;
					if (request.Url.PathAndQuery.StartsWith(FormsAuthentication.LoginUrl, StringComparison.OrdinalIgnoreCase))
					{
						var returnUrl = request.QueryString["returnUrl"];
						if (!string.IsNullOrWhiteSpace(returnUrl))
						{
							var routeInfo = new RouteInfo(new Uri(request.Url, returnUrl), HttpContext.Current.Request.ApplicationPath);
							var returnUrlLanguage = routeInfo.RouteData.Values["language"] as string;
							language = string.IsNullOrWhiteSpace(returnUrlLanguage) ? language : returnUrlLanguage;
						}
					}
					if (language == "lib" || language == "css")
						requestContext.RouteData.Values["language"] = language = "en";
					var cultureInfo = CultureInfo.CreateSpecificCulture(language);
					if (cultureInfo != null)
					{

						Thread.CurrentThread.CurrentUICulture = cultureInfo;
						Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
					}
				}
				catch
				{
					//if invalid culture, do nothing
				}

			return base.GetHttpHandler(requestContext);
		}


		#endregion Override Methods

	}
}
