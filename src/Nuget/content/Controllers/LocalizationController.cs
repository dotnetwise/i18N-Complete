using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Localization.Controllers
{
    public class LocalizationController
        : LocalizableController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Exemple(int? id)
        {
            return View();
        }

        private static void SwitchCulture(string cultureName)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultureName);
        }

        private void ExportTranslation(string language)
        {
            SwitchCulture(language);
            var exportPath = Path.Combine(AppConfig.Export.GeneratedFolderPath, language, "messages.js");
            var culture = CultureInfo.CurrentCulture;
            var lcid = culture.LCID;
            var sb = new StringBuilder();
            sb.AppendLine("var appConfig = appConfig || {};");
            sb.AppendLine("appConfig.messages = {");
            foreach (var msg in Internationalization.Localizations[lcid].Messages.Values)
            {
                sb.Append("\t").Append(msg.MsgID.js()).Append(": {");
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
            System.IO.File.WriteAllText(exportPath, sb.ToString(), Encoding.UTF8);
        }

    }
}
