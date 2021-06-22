using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CosmeticsStore.Models;
using PagedList;

namespace CosmeticsStore.Controllers
{
    public class ProductController : Controller
    {
        private CosmeticsStoreDbContext db = new CosmeticsStoreDbContext();
        // GET: ProductDetail

        public ActionResult ProductList(int? ProductTypeID, int? ProducerID, int? page)
        {
            if (ProductTypeID == null && ProducerID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listSP = db.Products.Where(n => n.ProductTypeID == ProductTypeID && n.ProducerID == ProducerID && n.InventoryNumber > 0 && n.Status != false);

            if (listSP.Count() == 0)
            {
                return HttpNotFound();
            }
            int PageSize = 12;
            int PageNumber = (page ?? 1);
            ViewBag.MaLoaiSP = ProductTypeID;
            ViewBag.MaNSX = ProducerID;
            return View(listSP.OrderBy(x => x.Discount).ToPagedList(PageNumber, PageSize));
        }
        public ActionResult ProductDetail(int? id)
        {
            ViewBag.Comment = db.Reviews.Where(x => x.ProductID == id).Take(6).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.SingleOrDefault(x => x.ProductID == id && x.Status != false);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult ProductLikePartial(string tukhoa)
        {
            //string productType = Request["productType"];
            if (tukhoa == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Product> model = db.Products.Where(x => x.Status != false && x.ProductType.Name.Contains(tukhoa)).OrderBy(x => x.Discount).Take(12).ToList();
            return PartialView(model);
        }
        public ActionResult ProductTypeList(int? ProducerID, int? page)
        {
            if (ProducerID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listSP = db.Products.Where(n => n.ProducerID == ProducerID && n.InventoryNumber > 0 && n.Status != false);

            if (listSP.Count() == 0)
            {
                return HttpNotFound();
            }
            int PageSize = 12;
            int PageNumber = (page ?? 1);
            return View(listSP.OrderBy(n => n.ProductID).ToPagedList(PageNumber, PageSize));
        }
        public ActionResult ListRate(int? id)
        {
            var model = db.Reviews.Where(x => x.ProductID == id).Take(6).ToList();
            return PartialView(model);
        }
        public ActionResult CreateReview(int? productID)
        {
            ViewBag.ProductID = productID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateReview([Bind(Include = "ReviewID,Content,Star,CusomerID,ProductID")] Review review , int? productID)
        {
            Product product = db.Products.FirstOrDefault(n => n.ProductID == productID);
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                product.ReviewCounts += 1; 
                db.SaveChanges();
                return View();
            }

            ViewBag.CusomerID = new SelectList(db.Customers, "CustomerID", "UserName", review.CusomerID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", review.ProductID);
            return View(review);
        }
    }
}