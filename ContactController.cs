using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class ContactController : Controller
    {
        Context db = new Context();
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Send(Tbl_Contact contact)
        {
            db.Tbl_Contact.Add(contact);
            db.SaveChanges();
            return Json(new { result = true, message = "با موفقیت ثبت شد" });
        }
    }
}