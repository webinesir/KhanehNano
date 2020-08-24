using PagedList;
using CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class CourseController : Controller
    {
        Context db = new Context();
        // GET: Course
        public ActionResult Index(string sort, string search, int category = 0, int page = 1, int collection = 0)
        {
            var list = db.Tbl_Course.OrderByDescending(c => c.date_reg).ToList();

            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "visit")
                    list = list.OrderByDescending(c => c.visit).ToList();
                if (sort == "date")
                    list = list.OrderByDescending(c => c.date_reg).ToList();
                if (sort == "price-asc")
                    list = list.OrderBy(c => c.price).ToList();
                if (sort == "price-desc")
                    list = list.OrderByDescending(c => c.price).ToList();
            }

            if (category > 0)
                list = list.Where(c => c.coursecategory_id == category).ToList();

            if (!string.IsNullOrEmpty(search))
                list = list.Where(c => c.title.Contains(search)).ToList();

            if (collection > 0)
            {
                list = list.Where(c => c.collection_id == collection).ToList();
            }

            int pageSize = Setting.get().course_page_count;
            int pageNumber = (page);
            int count = list.ToPagedList(pageNumber, pageSize).PageCount;
            string html = "";
            for (int i = 1; i <= count; i++)
            {
                if (page == i)
                {
                    html += "<b>" + i + "</b>";
                }
                else
                {
                    html += "<a href='/course?page=" + i + "'>" + i + "</a>";
                }
            }
            ViewBag.page = "<div class='paging'>" + html + "</div>";
            return View(list.ToPagedList(pageNumber, pageSize));
            //return View(list);
        }

        [Route("course/detail/{id}/{title}")]
        public ActionResult Detail(int id, string title)
        {

            var get = db.Tbl_Course.Find(id);
            if (get == null)
            {
                return View(Config.SitePageNotFound);
            }
            else
            {
                if (get.active == false)
                {
                    return View(Config.SitePageNotFound);
                }
                else
                {
                    get.visit = get.visit + 1;
                    db.Entry(get).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return View(get);
                }
            }
        }


        [Route("course/detail/{id}")]
        public ActionResult Detail2(int id)
        {
            var get = db.Tbl_Course.Find(id);
            if (get == null)
            {
                return View(Config.SitePageNotFound);
            }
            else
            {
                Response.Redirect("/course/detail/" + id + "/" + get.title.Replace(" ", "-"));

            }
            return View();
        }

        [HttpPost]
        public JsonResult SendComment(Tbl_Comment comment)
        {

            db.Tbl_Comment.Add(comment);
            db.SaveChanges();
            return Json(new { result = true, message = "دیدگاه با موفقیت ثبت شد" });
        }
    }
}