using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CosmeticsStore.Models;


namespace CosmeticsStore.Controllers
{
    public class CartController : Controller
    {
        CosmeticsStoreDbContext db = new CosmeticsStoreDbContext();
        // GET: Cart
        //Lấy giỏ hàng
        public List<CartItems> LayGioHang()
        {
            //Giỏ hàng đã tồn tại 
            List<CartItems> lstGioHang = Session["GioHang"] as List<CartItems>;
            if (lstGioHang == null)
            {
                //Nếu session giỏ hàng chưa tồn tại thì khởi tạo giỏ hàng
                lstGioHang = new List<CartItems>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //Thêm giỏ hàng thông thường (Load lại trang)
        public ActionResult ThemGioHang(int MaSP, string strURL)
        {
            //Kiểm tra sản phẩm có tồn tại trong CSDL hay không
            Product sp = db.Products.SingleOrDefault(n => n.ProductID == MaSP);
            if (sp == null)
            {
                //TRang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng
            List<CartItems> lstGioHang = LayGioHang();
            //Trường hợp 1 nếu sản phẩm đã tồn tại trong giỏ hàng 
            CartItems spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck != null)
            {
                //Kiểm tra số lượng tồn trước khi cho khách hàng mua hàng
                if (sp.InventoryNumber < spCheck.SoLuong)
                {
                    return View("ThongBao");
                }
                spCheck.SoLuong++;
                spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                return Redirect(strURL);
            }

            CartItems itemGH = new CartItems(MaSP);
            if (sp.InventoryNumber < itemGH.SoLuong)
            {
                return View("ThongBao");
            }

            lstGioHang.Add(itemGH);
            return Redirect(strURL);
        }
        //Tính tổng số lượng
        public double TinhTongSoLuong()
        {
            //Lấy giỏ hàng
            List<CartItems> lstGioHang = Session["GioHang"] as List<CartItems>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.SoLuong);
        }
        //Tính Tổng tiền 
        public decimal TinhTongTien()
        {
            //Lấy giỏ hàng
            List<CartItems> lstGioHang = Session["GioHang"] as List<CartItems>;
            if (lstGioHang == null)
            {
                return 0;
            }
            if (lstGioHang.Sum(n => n.ThanhTien) < 300000)
            {
                return lstGioHang.Sum(n => n.ThanhTien) + 30000;

            }
            return lstGioHang.Sum(n => n.ThanhTien);

        }

        public ActionResult GioHangPartial()
        {
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            return PartialView();
        }
        public ActionResult GioHangPartial2()
        {
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            return PartialView();
        }

        // GET: /GioHang/
        public ActionResult XemGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Lấy giỏ hàng 
            List<CartItems> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }

        //Chỉnh sửa giỏ hàng
        [HttpGet]
        public ActionResult SuaGioHang(int MaSP)
        {
            //Kiểm tra session giỏ hàng tồn tại chưa 
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Kiểm tra sản phẩm có tồn tại trong CSDL hay không
            Product sp = db.Products.SingleOrDefault(n => n.ProductID == MaSP);
            if (sp == null)
            {
                //TRang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            //Lấy list giỏ hàng từ session
            List<CartItems> lstGioHang = LayGioHang();
            //Kiểm tra xem sản phẩm đó có tồn tại trong giỏ hàng hay không
            CartItems spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Lấy list giỏ hàng tạo giao diện
            ViewBag.GioHang = lstGioHang;

            //Nếu tồn tại rồi
            return View(spCheck);
        }
        //Xử lý cập nhật giỏ hàng
        [HttpPost]
        public ActionResult CapNhatGioHang(CartItems itemGH)
        {
            //Kiểm tra số lượng tồn 
            Product spCheck = db.Products.SingleOrDefault(n => n.ProductID == itemGH.MaSP);
            ViewBag.SLTon = spCheck.InventoryNumber;
            if (spCheck.InventoryNumber < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            //Cập nhật số lượng trong session giỏ hàng 
            //Bước 1: Lấy List<GioHang> từ session["GioHang"]
            List<CartItems> lstGH = LayGioHang();
            //Bước 2: Lấy sản phẩm cần cập nhật từ trong list<GioHang> ra
            CartItems itemGHUpdate = lstGH.Find(n => n.MaSP == itemGH.MaSP);
            //Bước 3: Tiến hành cập nhật lại số lượng cũng thành tiền
            itemGHUpdate.SoLuong = itemGH.SoLuong;
            itemGHUpdate.ThanhTien = itemGHUpdate.SoLuong * itemGHUpdate.DonGia;
            return RedirectToAction("XemGioHang");
        }

        public ActionResult XoaGioHang(int MaSP)
        {
            //Kiểm tra session giỏ hàng tồn tại chưa 
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Kiểm tra sản phẩm có tồn tại trong CSDL hay không
            Product sp = db.Products.SingleOrDefault(n => n.ProductID == MaSP);
            if (sp == null)
            {
                //TRang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            //Lấy list giỏ hàng từ session
            List<CartItems> lstGioHang = LayGioHang();
            //Kiểm tra xem sản phẩm đó có tồn tại trong giỏ hàng hay không
            CartItems spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Xóa item trong giỏ hàng
            lstGioHang.Remove(spCheck);
            return RedirectToAction("XemGioHang");
        }
        //Xây dựng chức năng đặt hàng
        public ActionResult DatHang(Customer kh)
        {
            //Kiểm tra session giỏ hàng tồn tại chưa 
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (Session["user"] == null)
            {
                RedirectToAction("Login", "Authen");
            }
            Customer tv = Session["user"] as Customer;

            //Thêm đơn hàng 
            Order ddh = new Order();
            ddh.CustomerID = tv.CustomerID;
            ddh.OrderDate = DateTime.Now;
            ddh.Status = false;
            ddh.Paid = false;
            ddh.Discount = 0;
            ddh.Cancelled = false;
            ddh.Approved = false;
            db.Orders.Add(ddh);
            db.SaveChanges();
            decimal total = 0;
            //Thêm chi tiết đơn đặt hàng
            List<CartItems> lstGH = LayGioHang();
            foreach (var item in lstGH)
            {
                DetailOrder ctdh = new DetailOrder();
                ctdh.OrderID = ddh.OrderID;
                ctdh.ProductID = item.MaSP;
                ctdh.ProductName = item.TenSP;
                ctdh.Quantity = item.SoLuong;
                ctdh.Price = item.DonGia;
                total += item.SoLuong * item.DonGia;
                db.DetailOrders.Add(ctdh);
            }
            string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/Admin/neworderForAdmin.html"));
            content = content.Replace("{{CustomerName}}", tv.Name);
            content = content.Replace("{{Address}}", tv.Address);
            content = content.Replace("{{Email}}", tv.Email);
            content = content.Replace("{{Phone}}", tv.Phone);
            content = content.Replace("{{Total}}", total.ToString("N0") + "vnđ");
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
            Commons.SendMail(toEmail, "Đơn hàng mới từ Mỹ Phẩm Ngọc Ánh", content);

            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XemGioHang");
        }
        //Thêm giỏ hàng Ajax
        public ActionResult ThemGioHangAjax(int MaSP, string strURL)
        {
            //Kiểm tra sản phẩm có tồn tại trong CSDL hay không
            Product sp = db.Products.SingleOrDefault(n => n.ProductID == MaSP);
            if (sp == null)
            {
                //TRang đường dẫn không hợp lệ
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng
            List<CartItems> lstGioHang = LayGioHang();
            //Trường hợp 1 nếu sản phẩm đã tồn tại trong giỏ hàng 
            CartItems spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck != null)
            {
                //Kiểm tra số lượng tồn trước khi cho khách hàng mua hàng
                if (sp.InventoryNumber < spCheck.SoLuong)
                {
                    return Content("<script> alert(\"Sản phẩm đã hết hàng!\")</script>");
                }
                spCheck.SoLuong++;
                spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                ViewBag.TongSoLuong = TinhTongSoLuong();
                ViewBag.TongTien = TinhTongTien();
                return PartialView("GioHangPartial");
            }

            CartItems itemGH = new CartItems(MaSP);
            if (sp.InventoryNumber < itemGH.SoLuong)
            {
                return Content("<script> alert(\"Sản phẩm đã hết hàng!\")</script>");
            }

            lstGioHang.Add(itemGH);
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien();
            return PartialView("GioHangPartial");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }


}
