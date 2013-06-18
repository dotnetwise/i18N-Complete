using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace System.Web.Mvc
{
	/// <summary>The RouteInfo class</summary>
	/// <created author="scott.schluer" date="Fri, 18 Nov 2011 14:25:55 GMT"/>
	public class RouteInfo
	{

		#region Constructors

		/// <summary>Creates a new instance of RouteInfo</summary>
		/// <param name="uri"></param>
		/// <param name="applicationPath"></param>
		/// <created author="scott.schluer" date="Fri, 18 Nov 2011 14:25:55 GMT"/>
		public RouteInfo(Uri uri, string applicationPath)
		{
			RouteData = RouteTable.Routes.GetRouteData(new InternalHttpContext(uri, applicationPath));
		}


		#endregion Constructors


		#region Properties

		/// <summary>Gets the RouteData</summary>
		/// <created author="scott.schluer" date="Fri, 18 Nov 2011 14:25:55 GMT"/>
		public RouteData RouteData { get; private set; }


		#endregion Properties


		#region InternalHttpContext Class
		/// <summary>The InternalHttpContext class</summary>
		/// <created author="scott.schluer" date="Fri, 18 Nov 2011 14:25:56 GMT"/>
		private class InternalHttpContext
			: HttpContextBase
		{

			#region Fields


			#region Const

			private readonly HttpRequestBase _request;

			#endregion Const


			#endregion Fields


			#region Constructors

			/// <summary>Creates a new instance of InternalHttpContext</summary>
			/// <param name="uri"></param>
			/// <param name="applicationPath"></param>
			/// <created author="scott.schluer" date="Fri, 18 Nov 2011 14:25:56 GMT"/>
			public InternalHttpContext(Uri uri, string applicationPath)
				: base()
			{
				_request = new InternalRequestContext(uri, applicationPath);
			}


			#endregion Constructors


			#region Override Properties

			/// <summary>Gets the Request</summary>
			/// <created author="scott.schluer" date="Fri, 18 Nov 2011 14:25:56 GMT"/>
			public override HttpRequestBase Request { get { return _request; } }


			#endregion Override Properties

		}
		#endregion Class  InternalHttpContext

		#region InternalRequestContext Class
		/// <summary>The InternalRequestContext class</summary>
		/// <created author="scott.schluer" date="Fri, 18 Nov 2011 14:25:56 GMT"/>
		private class InternalRequestContext
						: HttpRequestBase
		{

			#region Fields


			#region Const

			private readonly string _appRelativePath;
			private readonly string _pathInfo;

			#endregion Const


			#endregion Fields


			#region Constructors

			/// <summary>Creates a new instance of InternalRequestContext</summary>
			/// <param name="uri"></param>
			/// <param name="applicationPath"></param>
			/// <created author="scott.schluer" date="Fri, 18 Nov 2011 14:25:56 GMT"/>
			public InternalRequestContext(Uri uri, string applicationPath)
				: base()
			{
				_pathInfo = uri.Query;

				if (String.IsNullOrEmpty(applicationPath) || !uri.AbsolutePath.StartsWith(applicationPath,
					StringComparison.OrdinalIgnoreCase))
					_appRelativePath = uri.AbsolutePath.Substring(applicationPath.Length);
				else
					_appRelativePath = uri.AbsolutePath;
			}


			#endregion Constructors


			#region Override Properties

			/// <summary>Gets the AppRelativeCurrentExecutionFilePath</summary>
			/// <created author="scott.schluer" date="Fri, 18 Nov 2011 14:25:56 GMT"/>
			public override string AppRelativeCurrentExecutionFilePath
			{
				get
				{
					return String.Concat("~",
						_appRelativePath);
				}
			}

			/// <summary>Gets the PathInfo</summary>
			/// <created author="scott.schluer" date="Fri, 18 Nov 2011 14:25:56 GMT"/>
			public override string PathInfo { get { return _pathInfo; } }


			#endregion Override Properties

		}
		#endregion Class  InternalRequestContext



	}
	/// <summary>The UriExtensions class</summary>
	/// <created author="scott.schluer" date="Fri, 18 Nov 2011 14:28:22 GMT"/>
	public static partial class UriExtensions
	{

		#region Business Methods

		/// <summary>
		/// </summary>
		/// <param name="uri"></param>
		/// <param name="controllerName"></param>
		/// <param name="actionName"></param>
		/// <returns></returns>
		/// <created author="scott.schluer" date="Fri, 18 Nov 2011 14:28:22 GMT"/>
		public static bool IsRouteMatch(this Uri uri, string controllerName, string actionName)
		{
			RouteInfo routeInfo = new RouteInfo(uri, HttpContext.Current.Request.ApplicationPath);
			return (routeInfo.RouteData.Values["controller"].ToString() == controllerName &&
				routeInfo.RouteData.Values["action"].ToString() == actionName);
		}

		/// <summary>
		/// </summary>
		/// <param name="uri"></param>
		/// <param name="paramaterName"></param>
		/// <returns></returns>
		/// <created author="scott.schluer" date="Fri, 18 Nov 2011 14:28:22 GMT"/>
		public static string GetRouteParameterValue(this Uri uri, string paramaterName)
		{
			RouteInfo routeInfo = new RouteInfo(uri, HttpContext.Current.Request.ApplicationPath);
			return routeInfo.RouteData.Values[paramaterName] != null
				? routeInfo.RouteData.Values[paramaterName].ToString()
				: null;
		}


		#endregion Business Methods


	}

}
