using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class EmailTemplateController : Controller
    {
        // GET: EmailTemplate
        public ActionResult MessageAnswer()
        {
            return View();
        }


        public ActionResult Template(string text)
        {
            ViewBag.text = text;
            return View();
        }
    }
}