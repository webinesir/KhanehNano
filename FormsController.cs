using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class FormsController : Controller
    {
        Context db = new Context();
        // GET: Forms
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Idea()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SubmitIdea(string email,string name,string codemeli,string tell,string mobile,string description,string resume,string subject,string age)
        {
            string text = "";
            text += "درخواست جدید در بخش ایده پردازی جوانان <br><br> ";
            text += "نام <br>: " + name;
            text += "<br><br>";
            text += "ایمیل <br>: " + email;
            text += "<br><br>";
            text += "شماره تماس <br>: " + tell;
            text += "<br><br>";
            text += "شماره موبایل <br>: " + mobile;
            text += "<br><br>";
            text += "کدملی <br>: " + codemeli;
            text += "<br><br>";
            text += "سن <br>: " + age;
            text += "<br><br>";
            text += "عنوان <br>: " + subject;
            text += "<br><br>";
            text += "توضیحات <br>: " + description;
            text += "<br><br>";
            text += "رزومه : <br>"+resume;

            Tbl_Contact contact = new Tbl_Contact();
            contact.Name = name;
            contact.Email = email;
            contact.Date = DateTime.Now;
            contact.IP = Request.UserHostAddress;
            contact.Phone = mobile;
            contact.Read = false;
            contact.Message = tell;
            contact.Subject = "درخواست جدید در بخش ایده پردازی جوانان";
            db.Tbl_Contact.Add(contact);
            db.SaveChanges();

            return Json(new { result = true, message = "درخواست شما با موفقیت ثبت شد و پس از بررسی با شما تماس گرفته میشود" });

        }
    }
}