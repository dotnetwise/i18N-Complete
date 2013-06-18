using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
