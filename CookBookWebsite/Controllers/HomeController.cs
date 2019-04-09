using CookBookWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CookBookWebsite.Controllers
{
    /// <summary>
    /// 首页控制器
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            using(var db = new CookBookDbContext())
            {
                ViewBag.CookBooks = db.CookBooks.ToList();
            }

            return View();
        }
    }
}