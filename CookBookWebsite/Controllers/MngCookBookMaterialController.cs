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
    public class MngCookBookMaterialController : Controller
    {
        private CookBookDbContext db = new CookBookDbContext();

        // GET: MngCookBookMaterial
        public ActionResult Index()
        {
            var cookBookMaterials = db.CookBookMaterials.Include(c => c.CookBook);
            return View(cookBookMaterials.ToList());
        }

        // GET: MngCookBookMaterial/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CookBookMaterial cookBookMaterial = db.CookBookMaterials.Find(id);
            if (cookBookMaterial == null)
            {
                return HttpNotFound();
            }
            return View(cookBookMaterial);
        }

        // GET: MngCookBookMaterial/Create
        public ActionResult Create()
        {
            ViewBag.CookBookID = new SelectList(db.CookBooks, "ID", "Title");
            return View();
        }

        // POST: MngCookBookMaterial/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Level,Name,Amount,CookBookID")] CookBookMaterial cookBookMaterial)
        {
            if (ModelState.IsValid)
            {
                db.CookBookMaterials.Add(cookBookMaterial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CookBookID = new SelectList(db.CookBooks, "ID", "Title", cookBookMaterial.CookBookID);
            return View(cookBookMaterial);
        }

        // GET: MngCookBookMaterial/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CookBookMaterial cookBookMaterial = db.CookBookMaterials.Find(id);
            if (cookBookMaterial == null)
            {
                return HttpNotFound();
            }
            ViewBag.CookBookID = new SelectList(db.CookBooks, "ID", "Title", cookBookMaterial.CookBookID);
            return View(cookBookMaterial);
        }

        // POST: MngCookBookMaterial/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Level,Name,Amount,CookBookID")] CookBookMaterial cookBookMaterial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cookBookMaterial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CookBookID = new SelectList(db.CookBooks, "ID", "Title", cookBookMaterial.CookBookID);
            return View(cookBookMaterial);
        }

        // GET: MngCookBookMaterial/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CookBookMaterial cookBookMaterial = db.CookBookMaterials.Find(id);
            if (cookBookMaterial == null)
            {
                return HttpNotFound();
            }
            return View(cookBookMaterial);
        }

        // POST: MngCookBookMaterial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CookBookMaterial cookBookMaterial = db.CookBookMaterials.Find(id);
            db.CookBookMaterials.Remove(cookBookMaterial);
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
