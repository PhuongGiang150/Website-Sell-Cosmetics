using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CosmeticsStore.Models;

namespace CosmeticsStore.Areas.Admin.Controllers
{
    public class AuthenAdminsController : Controller
    {
        CosmeticsStoreDbContext db = new CosmeticsStoreDbContext();
        // GET: Admin/Login
        public ActionResult Login()
        {
            return View();
       
        }
        public ActionResult Dotask()
        {
            string account = Request["account"];
            string password = Request["password"];
            var f_password = Commons.GetMD5(password);
            var data = db.Customers.Where(s => s.UserName.Equals(account) && s.Password.Equals(f_password) && s.Status != false && s.Role == "Chủ cửa hàng"|| s.Role == "Nhân viên");
            if (data.Count() > 0)
            {
                Session["admin"] = data.FirstOrDefault();
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
               ModelState.AddModelError("ErrorAdmin", "Tài khoản hoặc mật khẩu không chính sác!");
                return View("Login");
            }

        }
        public ActionResult Logout()
        {
            Session["admin"] = null;
            return RedirectToAction("Login", "AuthenAdmins");
        }
    }
}