using CookBookWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CookBookWebsite.Controllers
{
    /// <summary>
    /// 帐号登录注册控制器
    /// </summary>
    public class AccountController : Controller
    {
        private CookBookDbContext db = new CookBookDbContext();

        /// <summary>
        /// 登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 尝试登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TryLogin()
        {
            string account = Request.Form["account"];
            string password = Request.Form["password"];

            User user = db.Users.Where(a => a.Account == account).FirstOrDefault();

            if (user == null)
            {
                ViewBag.Message = "该用户不存在";
                return View("Index", "Account");
            }
            else if (user.Password != password)
            {
                ViewBag.Message = "用户名或密码错误";
                return View("Index", "Account");
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.Account, false);
                Session["UserAccount"] = user.Account;
                Session["UserDisplayName"] = user.Name;
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove("UserAccount");
            Session.Remove("UserDisplayName");
            return RedirectToAction("Index", "Account");
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 尝试注册
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult TryRegister()
        {
            string account = Request.Form["account"];
            string password = Request.Form["password"];
            string displayname = Request.Form["displayname"];

            User user = new User();
            user.Account = account;
            user.Password = password;
            user.Name = displayname;
            user.Sex = 1;

            db.Users.Add(user);

            try
            {
                db.SaveChanges();

                FormsAuthentication.SetAuthCookie(user.Account, false);
                Session["UserAccount"] = user.Account;
                Session["UserDisplayName"] = user.Name;
                return RedirectToAction("Index", "UserCenter");
            }
            catch
            {
                ViewBag.Message = "注册失败，请联系管理员";
                return View();
            }
        }

    }
}