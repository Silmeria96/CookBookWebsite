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
    public class MngCookBookStepController : Controller
    {
        private CookBookDbContext db = new CookBookDbContext();

        // GET: MngCookBookStep
        public ActionResult Index()
        {
            var cookBookSteps = db.CookBookSteps.Include(c => c.CookBook);
            return View(cookBookSteps.ToList());
        }

        // GET: MngCookBookStep/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CookBookStep cookBookStep = db.CookBookSteps.Find(id);
            if (cookBookStep == null)
            {
                return HttpNotFound();
            }
            return View(cookBookStep);
        }

        // GET: MngCookBookStep/Create
        public ActionResult Create()
        {
            ViewBag.CookBookID = new SelectList(db.CookBooks, "ID", "Title");
            return View();
        }

        // POST: MngCookBookStep/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StepID,OrderID,ImageUrl,Content,CookBookID")] CookBookStep cookBookStep)
        {
            if (ModelState.IsValid)
            {
                db.CookBookSteps.Add(cookBookStep);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CookBookID = new SelectList(db.CookBooks, "ID", "Title", cookBookStep.CookBookID);
            return View(cookBookStep);
        }

        // GET: MngCookBookStep/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CookBookStep cookBookStep = db.CookBookSteps.Find(id);
            if (cookBookStep == null)
            {
                return HttpNotFound();
            }
            ViewBag.CookBookID = new SelectList(db.CookBooks, "ID", "Title", cookBookStep.CookBookID);
            return View(cookBookStep);
        }

        // POST: MngCookBookStep/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StepID,OrderID,ImageUrl,Content,CookBookID")] CookBookStep cookBookStep)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cookBookStep).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CookBookID = new SelectList(db.CookBooks, "ID", "Title", cookBookStep.CookBookID);
            return View(cookBookStep);
        }

        // GET: MngCookBookStep/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CookBookStep cookBookStep = db.CookBookSteps.Find(id);
            if (cookBookStep == null)
            {
                return HttpNotFound();
            }
            return View(cookBookStep);
        }

        // POST: MngCookBookStep/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CookBookStep cookBookStep = db.CookBookSteps.Find(id);
            db.CookBookSteps.Remove(cookBookStep);
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
    }
}
