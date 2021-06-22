using System.Web;
using System.Web.Optimization;

namespace CosmeticsStore
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //  Tạo bundles link script trang admin
            bundles.Add(new ScriptBundle("~/JsAdmin").Include(
                    "~/Content/Admin/vendor/jquery/jquery.min.js",
                     "~/Content/Admin/vendor/bootstrap/js/bootstrap.bundle.min.js",
                     "~/Content/Admin/vendor/jquery-easing/jquery.easing.min.js" ,
                     "~/Content/Admin/js/sb-admin-2.min.js"
                        ));

            //  Tạo bundles link script table trang admin
            bundles.Add(new ScriptBundle("~/JsAdminTable").Include(
                    "~/Content/Admin/vendor/datatables/jquery.dataTables.min.js",
                    "~/Content/Admin/vendor/datatables/dataTables.bootstrap4.min.js",
                    "~/Content/Admin/js/demo/datatables-demo.js"
                        ));

            //  Tạo bundles link script ck fimder && ck editor trang admin
            bundles.Add(new ScriptBundle("~/JsAdminCK_").Include(
                     "~/Plugin/ckeditor/ckeditor.js",
                       "~/Plugin/ckfinder/ckfinder.js" 
                        ));

            // Tạo bundles link css trang người dùng
            bundles.Add(new StyleBundle("~/CssSite").Include(
                       "~/Content/Site/assets/css/library.css",
                        "~/Content/Site/assets/owlCarousel/assets/owl.carousel.min.css",
                       "~/Content/Site/assets/owlCarousel/assets/owl.theme.default.min.css",
                       "~/Content/Site/assets/css/common.css"
                       ));

            // Tạo bundles link css trang quản trị
            bundles.Add(new StyleBundle("~/CssAdmin").Include(
                    "~/Content/Admin/css/sb-admin-2.min.css",
                    "~/Content/Admin/vendor/datatables/dataTables.bootstrap4.css",
                    "~/Content/Admin/css/adminForm.css"
                       ));
        }
    }
}
