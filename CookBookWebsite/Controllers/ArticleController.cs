using CookBookWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CookBookWebsite.Controllers
{
    /// <summary>
    /// 文章模块
    /// </summary>
    public class ArticleController : Controller
    {
        private CookBookDbContext db = new CookBookDbContext();

        /// <summary>
        /// 文章首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var articles = db.Articles.ToList();

            ViewBag.ArticleRankList = db.Articles.OrderByDescending(a => a.LikeNum).Take(5);

            return View(articles);
        }

        /// <summary>
        /// 文章详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article article = db.Articles.Find(id);

            if (article == null)
            {
                return HttpNotFound();
            }

            return View(article);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}