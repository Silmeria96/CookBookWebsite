using CookBookWebsite.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CookBookWebsite.Controllers
{
    // 菜谱模块

    public class CookBookController : Controller
    {
        private CookBookDbContext db = new CookBookDbContext();

        /// <summary>
        /// 菜谱首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 菜谱详情
        /// </summary>
        /// <param name="id">菜谱ID</param>
        /// <returns></returns>
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CookBook CookBook = db.CookBooks.Find(id);

            if (CookBook == null)
            {
                return HttpNotFound();
            }

            return View(CookBook);
        }

        /// <summary>
        /// 获取菜谱评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JObject GetCommentList(int id)
        {
            JObject rv = new JObject();

            var comments = (from s in db.CookBookComments
                            where s.CookBookID == id
                            orderby s.CreateDate descending
                            select new
                            {
                                CreateUser = s.CreateUser.Name,
                                CreateDate = s.CreateDate,
                                Comment = s.Content
                            }).ToList();


            if(comments != null && comments.Count > 0)
            {
                rv["flag"] = true;
                rv["comments"] = JsonConvert.SerializeObject(comments);
            }
            else
            {
                rv["flag"] = false;
            }

            return rv;
        }

        /// <summary>
        /// 发表评论
        /// </summary>
        /// <returns></returns>
        public JObject CreateComment()
        {
            JObject rv = new JObject();

            string account = Request.Form["account"];
            string content = Request.Form["content"];
            string cookbookid = Request.Form["cookbookid"];

            User createUser = db.Users.Where(a => a.Account == account).FirstOrDefault();
            CookBook cookBook = db.CookBooks.Find(Convert.ToInt32(cookbookid));

            CookBookComment comment = new CookBookComment();
            comment.CreateDate = DateTime.Now;
            comment.Content = content;
            comment.CreateUser = createUser;
            comment.CreateUserID = createUser.ID;
            comment.CookBook = cookBook;
            comment.CookBookID = cookBook.ID;

            db.CookBookComments.Add(comment);

            try
            {
                db.SaveChanges();
                rv["flag"] = true;
                rv["msg"] = "发表成功！";
            }
            catch
            {
                rv["flag"] = false;
                rv["msg"] = "发表评论发生了错误";
            }

            return rv;
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