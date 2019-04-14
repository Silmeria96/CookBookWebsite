using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CookBookWebsite.Models;

namespace CookBookWebsite.Controllers
{
    public class MngCookBookController : Controller
    {
        private CookBookDbContext db = new CookBookDbContext();

        #region 脚手架代码

        // GET: MngCookBook
        public ActionResult Index()
        {
            return View(db.CookBooks.ToList());
        }

        // GET: MngCookBook/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CookBook cookBook = db.CookBooks.Find(id);
            if (cookBook == null)
            {
                return HttpNotFound();
            }
            return View(cookBook);
        }

        // GET: MngCookBook/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MngCookBook/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CreateDate,Title,SubTitle,Image,Content,NeedPayment,LikeNum")] CookBook cookBook)
        {
            if (ModelState.IsValid)
            {
                db.CookBooks.Add(cookBook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cookBook);
        }

        // GET: MngCookBook/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CookBook cookBook = db.CookBooks.Find(id);
            if (cookBook == null)
            {
                return HttpNotFound();
            }
            return View(cookBook);
        }

        // POST: MngCookBook/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CreateDate,Title,SubTitle,Image,Content,NeedPayment,LikeNum")] CookBook cookBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cookBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cookBook);
        }

        // GET: MngCookBook/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CookBook cookBook = db.CookBooks.Find(id);
            if (cookBook == null)
            {
                return HttpNotFound();
            }
            return View(cookBook);
        }

        // POST: MngCookBook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CookBook cookBook = db.CookBooks.Find(id);
            db.CookBooks.Remove(cookBook);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region 扩展代码

        /// <summary>
        /// 创建菜谱
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateCookBook(CreateCookBookModel model)
        {
            CookBook cookBook = new CookBook();
            cookBook.Title = model.Title;
            cookBook.SubTitle = model.SubTitle;
            cookBook.Content = model.Content;
            cookBook.Image = model.Image;

            db.CookBooks.Add(cookBook);
            db.SaveChanges();

            foreach(var step in model.Steps)
            {
                CookBookStep cookBookStep = new CookBookStep();
                cookBookStep.OrderID = model.Steps.IndexOf(step);
                cookBookStep.ImageUrl = step.ImageUrl;
                cookBookStep.Content = step.Content;
                cookBookStep.CookBookID = cookBook.ID;
                cookBookStep.CookBook = cookBook;

                db.CookBookSteps.Add(cookBookStep);
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        #endregion

    }
}
