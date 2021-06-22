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
    public class ProductsController : BaseController
    {
        private CosmeticsStoreDbContext db = new CosmeticsStoreDbContext();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Producer).Include(p => p.ProductType).Include(p => p.Supplier);
            return View(products.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "Name");
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Name");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "Name");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "ProductID,Name,Price,Discount,About,Description,MetaTitle,UpdateDate,InventoryNumber,ReviewCounts,SellCounts,Hot,New,SupplierID,ProducerID,ProductTypeID,Image1,Image2,Image3,Image4,Status")] Product product)
        {
            if (ModelState.IsValid)
            {
                    product.UpdateDate = DateTime.Now;
                    product.Status = true;
                    product.SellCounts = 0;
                    product.ReviewCounts = 0;
                if (product.Discount == null)
                {
                    product.Discount = 0;
                }

                product.MetaTitle = Commons.ToUnsignString(product.Name);
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "Name", product.ProducerID);
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Name", product.ProductTypeID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "Name", product.SupplierID);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "Name", product.ProducerID);
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Name", product.ProductTypeID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "Name", product.SupplierID);
            return View(product);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "ProductID,Name,Price,Discount,About,Description,MetaTitle,UpdateDate,InventoryNumber,Hot,New,SupplierID,ProducerID,ProductTypeID,Image1,Image2,Image3,Image4,Status")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.UpdateDate = DateTime.Now;
                if (product.Discount == null)
                {
                    product.Discount = 0;
                }
                product.MetaTitle = Commons.ToUnsignString(product.Name);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProducerID = new SelectList(db.Producers, "ProducerID", "Name", product.ProducerID);
            ViewBag.ProductTypeID = new SelectList(db.ProductTypes, "ProductTypeID", "Name", product.ProductTypeID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "Name", product.SupplierID);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
