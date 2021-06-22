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
    public class CustomersController : BaseController
    {
        private CosmeticsStoreDbContext db = new CosmeticsStoreDbContext();

        // GET: Admin/Customers
        public ActionResult Customer()
        {
            return View(db.Customers.Where(x=>x.Role == "Khách hàng").ToList());
        }
        public ActionResult Manager()
        {
            return View(db.Customers.Where(x => x.Role != "Khách hàng").ToList());

        }
        // GET: Admin/Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Admin/Customers/Create
        public ActionResult Create()
        {
            var list = new List<string> { "Nhân viên", "Chủ cửa hàng" };
            ViewBag.Role = list;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,UserName,Password,Name,Image,Address,Email,Phone,Role,Status,Question,Answer")] Customer customer)
        {
          
            if (ModelState.IsValid)
            {
                var MaHMD5 = Commons.GetMD5(customer.Password);
                customer.Password = MaHMD5;
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Manager");
            }
            var list = new List<string> { "Nhân viên", "Chủ cửa hàng" };
            ViewBag.Role = list;
            return View(customer);
        }

        // GET: Admin/Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
               
            }
            var list = new List<string> { "Nhân viên", "Chủ cửa hàng" };
            ViewBag.Role = list;
            ViewBag.Pass = customer.Password;
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,UserName,Password,Name,Image,Address,Email,Phone,Role,Status,Question,Answer")] Customer customer)
        {
           
            if (ModelState.IsValid)
            {
               
                var MaHMD5 = Commons.GetMD5(customer.Password);
                customer.Password = MaHMD5;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Manager");
            }
            var list = new List<string> { "Nhân viên", "Chủ cửa hàng" };
            ViewBag.Role = list;
            return View(customer);
        }

        // GET: Admin/Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Customer");
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
