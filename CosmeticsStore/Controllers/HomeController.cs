using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CosmeticsStore.Models;

namespace CosmeticsStore.Controllers
{
    public class HomeController : Controller
    {
        CosmeticsStoreDbContext db = new CosmeticsStoreDbContext();
        public ActionResult Index()
        {
            ViewBag.Trendings = db.Products.Where(x => x.Status != false && x.InventoryNumber > 0).OrderBy(x => x.SellCounts).Take(12).ToList();
            ViewBag.Hots = db.Products.Where(x => x.Status != false && x.InventoryNumber > 0).OrderByDescending(x => x.Hot).Take(12).ToList();
            ViewBag.News = db.Products.Where(x => x.Status != false && x.InventoryNumber > 0).OrderByDescending(x => x.New).Take(12).ToList();
            ViewBag.Prices = db.Products.Where(x => x.Status != false && x.InventoryNumber > 0).OrderByDescending(x => x.Discount).Take(12).ToList();
            return View();
        }
        public ActionResult SliderPartial()
        {
            var model = db.Sliders.OrderBy(x => x.DisplayOrder).Take(3).ToList();
            return PartialView(model);
        }
        public ActionResult BandPartial()
        {
            List<Producer> model = db.Producers.Where(x => x.Status != false).OrderBy(x => x.DisplayOder).Take(12).ToList();
            return PartialView(model);
        }
        public ActionResult MenuPartial()
        {
            System.Data.Entity.DbSet<Product> listSp = db.Products;
            return PartialView(listSp);
        }
        public ActionResult FooterPartial()
        {
            Contact model = db.Contacts.SingleOrDefault(x => x.Status != false);
            return PartialView(model);

        }
        public ActionResult NewPartial()
        {
            var model = db.News.Where(x => x.Status != false).OrderBy(x => x.DisplayOrder).Take(6).ToList();
            return PartialView(model);
        }


    }

}

