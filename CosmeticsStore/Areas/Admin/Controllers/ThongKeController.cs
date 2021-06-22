using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CosmeticsStore.Models;


namespace CosmeticsStore.Areas.Admin.Controllers
{
    public class ThongKeController : BaseController
    {
        CosmeticsStoreDbContext db = new CosmeticsStoreDbContext();
        // GET: Admin/ThongKe
        [HttpGet]
        public ActionResult Index()
        {
            var thang = DateTime.Now.Month;
            var nam = DateTime.Now.Year;
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString(); //Lấy số lượng người truy cập từ application đã được tạo
            ViewBag.SoLuongNguoiOnLine = HttpContext.Application["SoNguoiDangOnline"].ToString(); //Lấy số lượng đang truy cập
            ViewBag.TongDoanhThu = TongDoanhThu(); //Thống kê tổng doanh thu
            ViewBag.DoanhThuTheoThang = DoanhThuTheoThang(3, nam); //Thống kê tổng doanh thu
            ViewBag.TongDonHang = TongDonHang();//Thống kê đơn hàng
            ViewBag.SLDonHangMoi =SLDonHangMoi();//Thống kê đơn hàng mới
            ViewBag.TongKhachHang = SLKhachHang(); //Thống kê thành viên
            return PartialView();
        }
        public decimal TongDoanhThu()
        {
            //Thống kê theo tất cả doanh thu từ khi website thành lập
            decimal TongDoanhThu = db.DetailOrders.Sum(n => n.Quantity * n.Price).Value;

            return TongDoanhThu;

        }
        public double TongDonHang()
        {
            //Đếm đơn đặt hàng  
            double slDDH = db.Orders.Count();
            return slDDH;
        }
        public double SLDonHangMoi()
        {
            //Đếm đơn đặt hàng  
            double sl = db.Orders.Where(n => n.Paid == false && n.Approved == false).Count();
            return sl;
        }
        public double SLKhachHang()
        {
            //Đếm sl khách hàng
            double slTV = db.Customers.Where(n => n.Role == "Khách hàng").Count();
            return slTV;

        }
        public decimal DoanhThuTheoThang(int Thang, int Nam)
        {
            //Thống kê theo tất cả doanh thu từ khi website thành lập
            //List ra những đơn hàng nào có tháng, năm tương ứng
            var lstDDH = db.Orders.Where(n => n.OrderDate.Value.Month == Thang && n.OrderDate.Value.Year == Nam);
            decimal TongTien = 0;
            //Duyệt chi tiết của đơn đặt hàng đó và lấy tổng tiền của tất cả các sản phẩm thuộc đơn hàng đó
            foreach (var item in lstDDH)
            {
                TongTien += decimal.Parse(item.DetailOrders.Sum(n => n.Quantity * n.Price).Value.ToString());
            }
            return TongTien;
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