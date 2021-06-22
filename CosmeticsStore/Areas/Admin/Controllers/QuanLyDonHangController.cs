using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using CosmeticsStore.Models;

namespace CosmeticsStore.Areas.Admin.Controllers
{
    public class QuanLyDonHangController : BaseController
    {
        CosmeticsStoreDbContext db = new CosmeticsStoreDbContext();
        // GET: Admin/QuanLyDonHang
        public ActionResult ChuaDuyet()
        {
            //Lấy danh sách các đơn hàng Chưa duyệt
            var lstChuaduyet = db.Orders.Where(n => n.Paid == false &&  n.Approved == false).OrderByDescending(n => n.OrderDate);
            return View(lstChuaduyet); 
        }
        public ActionResult DangGiao()
        {
            //Lấy danh sách đơn hàng chưa giao 
            var lstDanggiao = db.Orders.Where(n => n.Status == false && n.Approved == true).OrderBy(n => n.DeliveryDate);
            return View(lstDanggiao);
        }
        public ActionResult Complete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Complete(int id)
        {
            Order order = db.Orders.Find(id);
            order.Status = true;
            order.Paid = true;
            db.SaveChanges();
            return RedirectToAction("HoanThanh","QuanLyDonHang","Admin");
        }
        public ActionResult HoanThanh()
        {
            //Lấy danh sách đơn hàng đã giao thành công
            var lstHoanthanh = db.Orders.Where(n => n.Status == true && n.Approved == true && n.Paid == true);
            return View(lstHoanthanh);
        }
        public ActionResult ThatBai()
        {
            //Lấy danh sách đơn hàng đã giao thành công
            var lstThatbai = db.Orders.Where(n => n.Status == false && n.Approved == true && n.Paid == false);
            return View(lstThatbai);
        }
        [HttpGet]
        public ActionResult DuyetDonHang(int? id)
        {
            //Kiểm tra xem id hợp lệ không
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order model = db.Orders.SingleOrDefault(n => n.OrderID == id);
            //Kiểm tra đơn hàng có tồn tại không
            if (model == null)
            {
                return HttpNotFound();
            }
            //Lấy danh sách chi tiết đơn hàng để hiển thị cho người dùng thấy
            var lstChiTietDH = db.DetailOrders.Where(n => n.OrderID == id);
            ViewBag.ListChiTietDH = lstChiTietDH;
            return View(model);
        }
        [HttpPost]
        public ActionResult DuyetDonHang(Order ddh)
        {
            //Truy vấn lấy ra dữ liệu của đơn hàn đó 
            Order ddhUpdate = db.Orders.FirstOrDefault(n => n.OrderID == ddh.OrderID);
            if (ModelState.IsValid)
            {
                ddhUpdate.Paid = ddh.Paid;
                ddhUpdate.Status = ddh.Status;
                ddhUpdate.DeliveryDate = ddh.DeliveryDate;
                ddhUpdate.Approved = true;
                db.SaveChanges();
                Customer kh = db.Customers.FirstOrDefault(n => n.CustomerID == ddhUpdate.CustomerID);
                var toEmail = kh.Email;
                //Lấy danh sách chi tiết đơn hàng để hiển thị cho người dùng thấy
                var lstChiTietDH = db.DetailOrders.Where(n => n.OrderID == ddh.OrderID);
                ViewBag.ListChiTietDH = lstChiTietDH;
                decimal? total = 0;
                string nameProduct = "";
               
                foreach (var item in lstChiTietDH)
                {
                    //Cập nhật số lượng tồn
                    item.Product.InventoryNumber -= item.Quantity;
                    item.Product.SellCounts += item.Quantity;
                    nameProduct += item.Quantity + " Sản phẩm :" + item.Product.Name + " Giá : " + item.Price;
                    if(item.Product.Price.GetValueOrDefault(0) * item.Quantity < 300000)
                    {
                        total = 30000;
                    }
                    total += item.Product.Price.GetValueOrDefault(0) * item.Quantity;
                }
                //Gửi khách hàng 1 mail để xác nhận việc thanh toán 
                string content = System.IO.File.ReadAllText(Server.MapPath("~/Content/Site/neworderForCustomer.html"));
                content = content.Replace("{{listProduct}}", nameProduct);
                content = content.Replace("{{Total}}", total.ToString() + "vnđ");
                Commons.SendMail(toEmail, "Bạn đã đặt thành công đơn hàng từ Mỹ Phẩm Ngọc Ánh", content);
                db.SaveChanges();

                return RedirectToAction("Index","Dashboard","Admin");
            }
            return View(ddhUpdate);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Abouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var lstChiTietDH = db.DetailOrders.Where(n => n.OrderID == id);

            foreach (var item in lstChiTietDH)
            {
                //Cập nhật số lượng tồn
                item.Product.InventoryNumber += item.Quantity;
                item.Product.SellCounts -= item.Quantity;
            }
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
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