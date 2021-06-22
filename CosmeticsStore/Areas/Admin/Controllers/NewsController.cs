using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CosmeticsStore.Models;

namespace CosmeticsStore.Areas.Admin.Controllers
{
    public class NewsController : BaseController
    {
        private CosmeticsStoreDbContext db = new CosmeticsStoreDbContext();

        // GET: Admin/News
        public ActionResult Index()
        {
            return View(db.News.ToList());
        }

        // GET: Admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: Admin/News/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "NewID,Name,MetaTitle,DisplayOrder,UpdateDate,Content,Image,Description,Status")] News news)
        {
            if (ModelState.IsValid)
            {
                if (news.UpdateDate == null)
                {

                    news.UpdateDate = DateTime.Now;
                }
                if (news.Status == null)
                {
                    news.Status = true;
                }
                news.MetaTitle = Commons.ToUnsignString(news.Name);
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(news);
        }

        // GET: Admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "NewID,Name,MetaTitle,DisplayOrder,UpdateDate,Content,Image,Description,Status")] News news)
        {
            if (ModelState.IsValid)
            {
                if (news.UpdateDate == null)
                {

                    news.UpdateDate = DateTime.Now;
                }
                news.MetaTitle = Commons.ToUnsignString(news.Name);
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult xoa(int id)
        {
            try
            {
                News news = db.News.SingleOrDefault(x => x.NewID == id);
                db.News.Remove(news);
                db.SaveChanges();
                return Json(new { code = 200, msg = "Xóa thành công " }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { code = 500, msg = "Xóa thất bại " + ex.Message }, JsonRequestBehavior.AllowGet);

            }
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
