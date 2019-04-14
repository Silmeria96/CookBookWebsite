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
            // 菜谱列表
            ViewBag.CookBookList = db.CookBooks.ToList();

            // 菜谱点赞排行榜
            ViewBag.CookBookRankList = db.CookBooks.OrderByDescending(c => c.LikeNum).Take(5);

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

        /// <summary>
        /// 点赞
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JObject DianZan()
        {
            JObject rv = new JObject();

            int id = Convert.ToInt32(Request.Form["id"]);

            CookBook target = db.CookBooks.Find(id);
            
            if(target != null)
            {
                if (target.LikeNum == null)
                    target.LikeNum = 1;
                else
                    target.LikeNum += 1;

                rv["flag"] = true;
                rv["msg"] = "操作成功！";
            }
            else
            {
                rv["flag"] = false;
                rv["msg"] = "操作失败！";
            }

            db.SaveChanges();

            return rv;
        }

        /// <summary>
        /// 收藏
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JObject Collect()
        {
            JObject rv = new JObject();

            int id = Convert.ToInt32(Request.Form["id"]);
            string account = Convert.ToString(Session["UserAccount"]);

            CookBook target = db.CookBooks.Find(id);
            User user = db.Users.Where(a => a.Account == account).FirstOrDefault();

            if (target != null && !string.IsNullOrEmpty(account))
            {
                user.CookBookCollected.Add(target);
                rv["flag"] = true;
                rv["msg"] = "操作成功！";
            }
            else
            {
                rv["flag"] = false;
                rv["msg"] = "操作失败！";
            }

            db.SaveChanges();

            return rv;
        }

        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JObject AddToCart()
        {
            JObject rv = new JObject();

            int id = Convert.ToInt32(Request.Form["id"]);
            string account = Convert.ToString(Session["UserAccount"]);

            CookBook target = db.CookBooks.Find(id);
            User user = db.Users.Where(a => a.Account == account).FirstOrDefault();

            if (target != null && string.IsNullOrEmpty(account))
            {
                user.CookBookShopCarted.Add(target);
                rv["flag"] = true;
                rv["msg"] = "操作成功！";
            }
            else
            {
                rv["flag"] = false;
                rv["msg"] = "操作失败！";
            }

            db.SaveChanges();

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