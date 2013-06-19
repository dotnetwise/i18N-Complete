using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace $rootnamespace$.Controllers
{
    public class LocalizationController
        : LocalizableController
    {
        public ActionResult Help(int? id)
        {
            return View();
        }

        private static void SwitchCulture(string cultureName)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultureName);
        }

        public ContentResult Index(string lang = "en", string include = ".js:")
        {
            SwitchCulture(lang);
            var culture = CultureInfo.CurrentCulture;
            var lcid = culture.LCID;
            var sb = new StringBuilder();
            LocalizationAppConfig.LocalizationLoadComments = true;
            sb.AppendLine("var appConfig = appConfig || {};");
            sb.AppendLine("appConfig.messages = {");
            foreach (var msg in Internationalization.Localizations[lcid].Messages.Values)
                if (string.IsNullOrEmpty(include) || msg.Contexts.Any(c => c.Contains(include)))
                {
                    sb.Append("\t").Append(msg.MsgID.js()).Append(": {");
                    _("xyz paul {0}", 123);
                    if (msg.Translated || msg.HasPlural)
                    {
                        if (msg.HasPlural)
                            sb.AppendLine()
                                .Append("\t\tt: ")
                                .Append(msg.MsgStr.js())
                                .AppendLine(",")
                                .Append("\t\tp: ")
                                .Append(msg.MsgID_Plural.js())
                                .AppendLine(",")
                                .Append("\t\tpt:")
                                .Append(msg.MsgStr_Plural.js())
                                .AppendLine(",")
                                .AppendLine("\t");
                        else sb.Append("t:").Append(msg.MsgStr.js());
                    }
                    sb.AppendLine("},");
                }
            sb.AppendLine("}");

            return Content(sb.ToString(), "text/javascript");
        }

    }
}
