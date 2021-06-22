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
    public class ProducersController : BaseController
    {
        private CosmeticsStoreDbContext db = new CosmeticsStoreDbContext();

        // GET: Admin/Producers
        public ActionResult Index()
        {
            return View(db.Producers.ToList());
        }

        // GET: Admin/Producers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producer producer = db.Producers.Find(id);
            if (producer == null)
            {
                return HttpNotFound();
            }
            return View(producer);
        }

        // GET: Admin/Producers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Producers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProducerID,Name,MetaTitle,DisplayOder,Info,Logo,Status")] Producer producer)
        {
            if (ModelState.IsValid)
            { 
                if(producer.Status == null)
                {
                    producer.Status = true;
                }
                producer.MetaTitle = Commons.ToUnsignString(producer.Name);
                db.Producers.Add(producer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producer);
        }

        // GET: Admin/Producers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producer producer = db.Producers.Find(id);
            if (producer == null)
            {
                return HttpNotFound();
            }
            return View(producer);
        }

        // POST: Admin/Producers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProducerID,Name,MetaTitle,DisplayOder,Info,Logo,Status")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                if (producer.Status == null)
                {
                    producer.Status = true;
                }
                producer.MetaTitle = Commons.ToUnsignString(producer.Name);
                db.Entry(producer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producer);
        }

        // GET: Admin/Producers/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Producer producer = db.Producers.Find(id);
        //    if (producer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(producer);
        //}

        // POST: Admin/Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producer producer = db.Producers.Find(id);
            db.Producers.Remove(producer);
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
