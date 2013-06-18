using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Web;

namespace System.Globalization
{
	/// <summary>The Internationalization class</summary>
	/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 14:55:18 GMT"/>
	public static partial class Internationalization
	{
		/// <summary>The language that MsgIDs are given in (generally the language the project is being developed in).</summary>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 14:55:18 GMT"/>
		public static readonly string DefaultWorkingLanguage = ConfigurationManager.AppSettings["Internalization.DefaultMessagesLanguage"] ?? "en";
		/// <summary>The default culture LCID</summary>
		/// <created author="laurentiu.macovei" date="Tue, 29 Nov 2011 12:59:18 GMT"/>
		public static readonly int DefaultWorkingLanguageLCID = Internationalization.LCID(DefaultWorkingLanguage);

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

		/// <summary>Static constructor of Internationalization</summary>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 14:55:18 GMT"/>
		static Internationalization()
		{
			Localizations = new Dictionary<int, Localization>();
			string _basePathAbsolute = HttpContext.Current.Server.MapPath(Internationalization.BasePath);


			if (Directory.Exists(_basePathAbsolute))
			{
				Localization l;
				foreach (string filename in Directory.GetFiles(_basePathAbsolute, "*.po", SearchOption.AllDirectories))
				{
					var culture = Path.GetFileNameWithoutExtension(filename);
					culture = Path.GetExtension(culture);
					culture = culture.StartsWith(".") ? culture.Substring(1) : culture;
					var cultureHash = string.IsNullOrWhiteSpace(culture) ? Internationalization.DefaultWorkingLanguageLCID : LCID(culture);
					if (!Localizations.TryGetValue(cultureHash, out l))
					{
						l = new Localization();
						l.LoadFromFile(filename);
						Localizations.Add(cultureHash, l);
					}
					else l.LoadFromFile(filename);
				}
			}

			if (!Localizations.ContainsKey(Internationalization.DefaultWorkingLanguageLCID))
				Localizations.Add(Internationalization.DefaultWorkingLanguageLCID, new Localization());
		}

		#region public methods

		/// <summary>Gets a translated message version of the supplied text message.</summary>
		/// <param name="msgID">Text to be translated</param>
		/// <param name="languageCode">Language to translate into</param>
		/// <param name="lcid">Specify the Culture LCID for faster access if you know it</param>
		/// <created author="laurentiu.macovei" date="Fri, 25 Nov 2011 14:55:19 GMT"/>
		public static Message GetMessage(string msgID, string languageCode = null, int? lcid = null)
		{
			if (Internationalization.HideAllLocalizedText)
				return Message.Empty;
			int languageHash = lcid.HasValue ? lcid.Value : string.IsNullOrWhiteSpace(languageCode) ? CultureInfo.CurrentUICulture.LCID : LCID(languageCode);
			Localization localization;

			if (!Localizations.TryGetValue(languageHash, out localization))
			{
				if (CultureInfo.CurrentUICulture.IsNeutralCulture)
				{
					Localizations[languageHash] = localization = new Localization();
					languageHash = Internationalization.DefaultWorkingLanguageLCID;
				}
				else
				{
					var nativeCultureHash = CultureInfo.GetCultureInfo(languageHash).Parent.LCID;
					if (!Localizations.TryGetValue(nativeCultureHash, out localization))
					{
						languageHash = Internationalization.DefaultWorkingLanguageLCID;
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
		public static string GetText(string msgID, string languageCode = null, int? lcid = null, bool plural = false)
		{
			var msg = GetMessage(msgID, languageCode, lcid);
            return msg == null ? null : msg.GetText(plural);
			//var result = msg == null ? null : msg.GetText(plural);
            //if (GettingText != null)
            //{
            //    return GettingText(msg, msgID, result, languageCode, lcid, plural);
            //}
            //return result;
		}
        //public static event GettingTextEventHandler GettingText;
        //public delegate string GettingTextEventHandler(Message msg, string msgID, string translation, string languageCode, int? lcid, bool plural);

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
				return Internationalization.DefaultWorkingLanguage;
			}

			foreach (string lang in request.UserLanguages)
			{
				string language = string.IsNullOrWhiteSpace(lang) ? Internationalization.DefaultWorkingLanguage : lang;
				int languageHash = string.IsNullOrWhiteSpace(lang) ? Internationalization.DefaultWorkingLanguageLCID : LCID(language);

				if (Localizations.ContainsKey(languageHash))
					return lang;

				string fragment = language.Split('-')[0];
				if (Localizations.ContainsKey(fragment.GetHashCode()))
					return fragment;
			}

			return fallback ?? Internationalization.DefaultWorkingLanguage;
		}

		#endregion

	}
}
