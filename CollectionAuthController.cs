using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class CollectionAuthController : Controller
    {
        Context db = new Context();
        ClsCollection collection = new ClsCollection();
        // GET: CollectionAuth
        public ActionResult Index()
        {
            return View();
        }

        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public JsonResult SetLogin(Tbl_Collection OP, bool Remember)
        {

            if (Session["TryLoginCol"] != null && Convert.ToInt32(Session["TryLoginCol"]) >= 5)
            {
                try
                {
                    DateTime trydate = Convert.ToDateTime(Session["TryLoginColDate"]);
                    TimeSpan span = DateTime.Now.Subtract(trydate);
                    if (span.TotalMinutes < 10)
                    {
                        return Json(new { result = false, message = "بیش از حد مجاز تلاش کرده اید، 10 دقیقه دیگر سعی کنید" });
                    }
                    else
                    {
                        Session.Add("TryLoginCol", 0);
                        SetLogin(OP, Remember);
                    }
                }
                catch (Exception)
                {
                    Session.Add("TryLoginColDate", DateTime.Now);
                    return Json(new { result = false, message = "بیش از حد مجاز تلاش کرده اید، 10 دقیقه دیگر سعی کنید" });
                }


            }
            Hash hh = new Hash();
            var pass = hh.Encrypt(OP.password);
            var check = db.Tbl_Collection.Where(c => c.username == OP.username && c.password == pass).FirstOrDefault();

            if (check != null)
            {
                Session.Add("TryLoginCol", 0);
                if (collection.AddSession(check))
                {
                    if (check.active == false)
                        return Json(new { result = false, message = "دسترسی شما غیرفعال شده است با مدیریت تماس بگیرید" });

                    return Json(new { result = true });
                }
                else
                {
                    return Json(new { result = false, message = "خطای سیستمی رخ داده است" });
                }
            }
            else
            {
                if (Session["TryLoginCol"] != null)
                    Session.Add("TryLoginCol", (int)Session["TryLoginCol"] + 1);
                else
                    Session.Add("TryLoginCol", 1);
                Session.Add("TryLoginColDate", DateTime.Now);
                return Json(new { result = false, message = "نام کاربری یا رمزعبور اشتباه است" });
            }
        }
    }
}