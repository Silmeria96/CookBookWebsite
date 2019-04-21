using CookBookWebsite.Models;
using Newtonsoft.Json.Linq;
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
        /// 账户充值
        /// </summary>
        /// <returns></returns>
        public ActionResult UserCharge()
        {
            string current_account = Session["UserAccount"].ToString();
            var current_user = db.Users.Where(a => a.Account == current_account).FirstOrDefault();

            ViewBag.CurrentMoney = current_user.Money;

            return PartialView();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult UserChangePasswordPartial()
        {
            return PartialView();
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
        /// 我的购物车
        /// </summary>
        /// <returns></returns>
        public ActionResult UserShopCart()
        {
            return PartialView();
        }

        /// <summary>
        /// 我购买的菜谱
        /// </summary>
        /// <returns></returns>
        public ActionResult UserCookBookShopCarted()
        {
            string current_account = Session["UserAccount"].ToString();
            var current_user = db.Users.Where(a => a.Account == current_account).FirstOrDefault();

            ViewBag.CookBookShopCarted = current_user.CookBookShopCarted.ToList();

            return PartialView();
        }

        /// <summary>
        /// 我的文章
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
        /// 创建文章表单
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
        /// 尝试充值
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JObject TryCharge(double money)
        {
            JObject rv = new JObject();

            string current_account = Session["UserAccount"].ToString();
            var current_user = db.Users.Where(a => a.Account == current_account).FirstOrDefault();

            current_user.Money += money;
            int result = db.SaveChanges();

            if(result > 0)
            {
                rv["flag"] = true;
                rv["msg"] = "充值成功！";
                rv["newMoney"] = current_user.Money;
            }
            else
            {
                rv["flag"] = false;
                rv["msg"] = "充值失败！";
            }

            return rv;
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