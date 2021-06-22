using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CosmeticsStore
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Cấu hình đường dẫn trang danh mục sản phẩm
            routes.MapRoute(
               name: "DanhMucSanPham",
               url: "danh-muc/{MetaTitle}-{ProducerID}",
               defaults: new { controller = "Product", action = "ProductTypeList", id = UrlParameter.Optional }
           );
            // Cấu hình đường dẫn trang danh mục sản phẩm
            routes.MapRoute(
               name: "DanhMucSanPham2",
               url: "danh-muc-san-pham/{ProductTypeID}/{ProducerID}",
               defaults: new { controller = "Product", action = "ProductList", id = UrlParameter.Optional }
           );
            // Cấu hình đường dẫn trang chi tiết sản phẩm
            routes.MapRoute(
               name: "ChiTietSanPham",
               url: "chi-tiet/{MetaTitle}-{id}",
               defaults: new { controller = "Product", action = "ProductDetail", id = UrlParameter.Optional }
           );
            // Cấu hình đường dẫn trang chủ
            routes.MapRoute(
               name: "TrangChu",
               url: "trang-chu",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );
            // Cấu hình đường dẫn trang giới thiệu
            routes.MapRoute(
               name: "GioiThieu",
               url: "gioi-thieu",
               defaults: new { controller = "Post", action = "AboutPost", id = UrlParameter.Optional }
           ); 
            // Cấu hình đường dẫn trang danh sách tin tức
            routes.MapRoute(
               name: "DanhSachTinTuc",
               url: "danh-sach-tin-tuc",
               defaults: new { controller = "Post", action = "NewList", id = UrlParameter.Optional }
           );
            // Cấu hình đường dẫn trang chi tiết sản phẩm
            routes.MapRoute(
               name: "ChiTietTinTuc",
               url: "tin-tuc/{MetaTitle}-{id}",
               defaults: new { controller = "Post", action = "NewDetail", id = UrlParameter.Optional }
           );
            // Cấu hình đường dẫn trang chi tiết sản phẩm
            routes.MapRoute(
               name: "ThayDoiTaiKhoan",
               url: "thay-doi-tai-khoan/{id}",
               defaults: new { controller = "AuthenSites", action = "EditAccount", id = UrlParameter.Optional }
           );
            // Cấu hình đường dẫn trang giới thiệu
            routes.MapRoute(
               name: "LienHe",
               url: "lien-he",
               defaults: new { controller = "Post", action = "ContactPost", id = UrlParameter.Optional }
           );
            // Cấu hình đường dẫn trang đăng kí
            routes.MapRoute(
               name: "DangKi",
               url: "dang-ki",
               defaults: new { controller = "AuthenSites", action = "Register", id = UrlParameter.Optional }
           );
            // Cấu hình đường dẫn trang đăng nhập
            routes.MapRoute(
               name: "DangNhap",
               url: "dang-nhap",
               defaults: new { controller = "AuthenSites", action = "Login", id = UrlParameter.Optional }
           );
            // Cấu hình đường dẫn trang giỏ hàng
            routes.MapRoute(
             name: "GioHang",
             url: "gio-hang",
             defaults: new { controller = "Cart", action = "SuaGioHang", id = UrlParameter.Optional }
         );
            // Cấu hình đường dẫn trang tìm kiếm
            routes.MapRoute(
             name: "TimKiem",
             url: "tim-kiem/{sTuKhoa}",
             defaults: new { controller = "TimKiem", action = "KQTimKiem", id = UrlParameter.Optional }
         );
            // Cấu hình đường dẫn trang sửa giỏ hàng
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
