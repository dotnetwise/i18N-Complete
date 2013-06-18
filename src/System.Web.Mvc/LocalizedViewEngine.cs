using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System.Web.Mvc
{
	/// <summary>The LocalizedViewEngine class</summary>
	/// <created author="laurentiu.macovei" date="Fri, 11 Nov 2011 23:48:59 GMT"/>
	public class LocalizedViewEngine
		: RazorViewEngine
	{

		#region Override Methods

		/// <summary>Finds the localized partial view</summary>
		/// <param name="controllerContext"></param>
		/// <param name="partialViewName"></param>
		/// <param name="useCache"></param>
		/// <returns></returns>
		/// <created author="laurentiu.macovei" date="Fri, 11 Nov 2011 23:48:59 GMT"/>
		public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
		{
			List<string> searched = new List<string>();

			if (!string.IsNullOrEmpty(partialViewName))
			{
				ViewEngineResult result;

				result = base.FindPartialView(controllerContext, string.Format("{0}.{1}", partialViewName, CultureInfo.CurrentUICulture.Name), useCache);

				if (result.View != null)
				{
					return result;
				}

				searched.AddRange(result.SearchedLocations);

				result = base.FindPartialView(controllerContext, string.Format("{0}.{1}", partialViewName, CultureInfo.CurrentUICulture.TwoLetterISOLanguageName), useCache);

				if (result.View != null)
				{
					return result;
				}

				searched.AddRange(result.SearchedLocations);
			}

			return new ViewEngineResult(searched.Distinct().ToList());
		}

		/// <summary>Finds the localized view</summary>
		/// <param name="controllerContext"></param>
		/// <param name="viewName"></param>
		/// <param name="masterName"></param>
		/// <param name="useCache"></param>
		/// <created author="laurentiu.macovei" date="Fri, 11 Nov 2011 23:48:59 GMT"/>
		public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
		{
			List<string> searched = new List<string>();

			if (!string.IsNullOrEmpty(viewName))
			{
				ViewEngineResult result;

				result = base.FindView(controllerContext, string.Format("{0}.{1}", viewName, CultureInfo.CurrentUICulture.Name), masterName, useCache);

				if (result.View != null)
				{
					return result;
				}

				searched.AddRange(result.SearchedLocations);

				result = base.FindView(controllerContext, string.Format("{0}.{1}", viewName, CultureInfo.CurrentUICulture.TwoLetterISOLanguageName), masterName, useCache);

				if (result.View != null)
				{
					return result;
				}

				searched.AddRange(result.SearchedLocations);
			}

			return new ViewEngineResult(searched.Distinct().ToList());
		}


		#endregion Override Methods


	}
}
