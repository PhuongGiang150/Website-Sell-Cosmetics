using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CosmeticsStore.Models;

namespace CosmeticsStore.Areas.Admin.Controllers
{
    public class QuanLyPhieuNhapController : BaseController
    {
        CosmeticsStoreDbContext db = new CosmeticsStoreDbContext();

        [HttpGet]
        public ActionResult NhapHang()
        {
            ViewBag.MaNCC = db.Suppliers;
            ViewBag.ListSanPham = db.Products;
            return View();
        }
        [HttpPost]
        public ActionResult NhapHang(Coupon model, IEnumerable<DetailCoupon> lstModel)
        {
            ViewBag.MaNCC = db.Suppliers;
            ViewBag.ListSanPham = db.Products;
            //Sau khi các bạn đã kiểm tra tất cả dữ liệu đầu vào
                db.Coupons.Add(model);
                db.SaveChanges();
            //SaveChanges để lấy được mã phiếu nhập gán cho lstChiTietPhieuNhap
            Product sp;
            foreach (var item in lstModel)
            {
                //Cập nhật số lượng tồn
                sp = db.Products.FirstOrDefault(n => n.ProductID == item.ProductID);
                sp.InventoryNumber += item.Quantity;
                //Gán mã phiếu nhập cho tất cả chi tiết phiếu nhập
                item.CouponID = model.CouponID;
            }
            db.DetailCoupons.AddRange(lstModel);
            db.SaveChanges();
            return View();
        }
        [HttpGet]
        public ActionResult DSSPHetHang()
        {
            //Danh sách sản phẩm gần hết hàng với số lượng tồn bé hơn hoặc bằng 5
            var lstSP = db.Products.Where(n => n.Status != false && n.InventoryNumber <= 5);
            return View(lstSP);

        }
        //Tạo 1 view phục vụ cho việc nhập từng sản phẩm
        [HttpGet]
        public ActionResult NhapHangDon(int? id)
        {
            ViewBag.MaNCC = new SelectList(db.Suppliers.OrderBy(n => n.Name), "SupplierID", "Name");
            //Tương tự như trang chỉnh sửa sản phẩm nhưng ta không cần phải show hết các thuộc tính 
            //Chỉ thuộc tính nào cần thiết mà thôi đó là số lượng tồn hình ảnh... thông tin hiển thị cần thiết
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Product sp = db.Products.SingleOrDefault(n => n.ProductID == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);

        }
        // Xử lý nhập hàng từng sản phẩm
        [HttpPost]
        public ActionResult NhapHangDon(Coupon model, DetailCoupon ctpn)
        {
            ViewBag.MaNCC = new SelectList(db.Suppliers.OrderBy(n => n.Name), "SupplierID", "Name", model.SupplierID);
            //Sau khi các bạn đã kiểm tra tất cả dữ liệu đầu vào
            //Gán đã xóa: False
            model.DateAdd = DateTime.Now;
            db.Coupons.Add(model);
            db.SaveChanges();
            //SaveChanges để lấy được mã phiếu nhập gán cho lstChiTietPhieuNhap
            ctpn.CouponID = model.CouponID;
            //Cập nhật tồn 
            Product sp = db.Products.FirstOrDefault(n => n.ProductID == ctpn.ProductID);
            sp.InventoryNumber += ctpn.Quantity;
            db.DetailCoupons.Add(ctpn);
            db.SaveChanges();
            return View(sp);

        }
        //Giải phóng biến cho vùng nhớ
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