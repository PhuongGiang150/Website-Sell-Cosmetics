using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CosmeticsStore.Models;

namespace CosmeticsStore.Controllers
{
    public class AuthenSitesController : Controller
    {
        CosmeticsStoreDbContext db = new CosmeticsStoreDbContext();

        // GET: Authen
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Dotask()
        {
            string account = Request["account"];
            string password = Request["password"];
            var f_password = Commons.GetMD5(password);
            var data = db.Customers.Where(s => s.UserName.Equals(account) && s.Password.Equals(f_password) && s.Status != false).ToList();

            if (data.Count() > 0)
            {
                Session["user"] = data.FirstOrDefault();
                if (Session["GioHang"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("XemGioHang", "Cart");
            }
            else
            {
                ModelState.AddModelError("ErrorLogin", "Tài khoản hoặc mật khẩu không chính sác!");
                return View("Login");

            }

        }
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index", "Home");
        }
        // GET: Admin/Customers/Create
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "CustomerID,UserName,Password,Name,Image,Address,Email,Phone,Role,Status,Question,Answer")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var MaHMD5 = Commons.GetMD5(customer.Password);
                customer.Password = MaHMD5;
                if (db.Customers.Any(x => x.UserName == customer.UserName))
                {
                    ModelState.AddModelError("ErrorUserName", "Tài khoản đã tồn tại!");
                }
                else if (db.Customers.Any(x => x.Email == customer.Email))
                {
                    ModelState.AddModelError("ErrorEmail", "Email đã tồn tại!");
                }
                else
                {
                    customer.Status = true;
                    customer.Role = "Khách Hàng";
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            return View(customer);
        }
        public ActionResult EditAccount(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccount([Bind(Include = "CustomerID,UserName,Password,Name,Image,Address,Email,Phone,AgentID,Status,Question,Answer")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var MaHMD5 = Commons.GetMD5(customer.Password);
                customer.Password = MaHMD5;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                Session["user"] = null;
                return RedirectToAction("Index","Home");
            }
            return View(customer);
        }
    }
}