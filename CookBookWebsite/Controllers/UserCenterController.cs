using CookBookWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CookBookWebsite.Controllers
{
    /// <summary>
    /// 个人中心控制器
    /// </summary>
    [Authorize]
    public class UserCenterController : Controller
    {
        private CookBookDbContext db = new CookBookDbContext();

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 个人信息模块
        /// </summary>
        /// <returns></returns>
        public ActionResult UserInfoPartial()
        {
            if (Session["UserAccount"] != null)
            {
                string account = Session["UserAccount"].ToString();
                User user = db.Users.Where(a => a.Account == account).FirstOrDefault();
                return PartialView(user);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

        /// <summary>
        /// 我的收藏
        /// </summary>
        /// <returns></returns>
        public ActionResult UserCollectionPartial()
        {
            string account = Convert.ToString(Session["UserAccount"]);

            var user = db.Users.Where(a => a.Account == account).FirstOrDefault();

            if(user != null)
            {
                var collections = user.CookBookCollected.ToList();
                return PartialView(collections);
            }

            return RedirectToAction("Index","Login");
        }

        /// <summary>
        /// 订单模块
        /// </summary>
        /// <returns></returns>
        public ActionResult UserOrderPartial()
        {
            return PartialView();
        }

        /// <summary>
        /// 文章模块
        /// </summary>
        /// <returns></returns>
        public ActionResult UserArticlePartial()
        {
            if(Session["UserAccount"] != null)
            {
                string account = Session["UserAccount"].ToString();
                var user_articles = db.Articles.Where(a => a.CreateUser.Account == account).ToList();
                return PartialView(user_articles);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

        /// <summary>
        /// 创建文章模块
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateArticlePartial()
        {
            if (Session["UserAccount"] != null)
            {
                string account = Session["UserAccount"].ToString();
                User user = db.Users.Where(a => a.Account == account).FirstOrDefault();
                ViewBag.UserID = user.ID;
                return PartialView();
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }


        /// <summary>
        /// 保存个人信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveUserInfo(User user)
        {
            User origin_user = db.Users.Where(a => a.Account == user.Account).FirstOrDefault();
            db.Entry(origin_user).CurrentValues.SetValues(user);
            db.SaveChanges();
            return PartialView("UserInfoPartial", user);
        }

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateArtial(Article article)
        {
            db.Articles.Add(article);
            db.SaveChanges();

            if (Session["UserAccount"] != null)
            {
                string account = Session["UserAccount"].ToString();
                var user_articles = db.Articles.Where(a => a.CreateUser.Account == account).ToList();
                return PartialView("UserArticlePartial",user_articles);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

    }
}