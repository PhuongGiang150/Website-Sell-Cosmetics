using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CosmeticsStore.Models;
using PagedList;

namespace CosmeticsStore.Controllers
{
    public class TimKiemController : Controller
    {
        CosmeticsStoreDbContext db = new CosmeticsStoreDbContext();
        [HttpGet]
        // GET: TimKiem
        public ActionResult KQTimKiem(string sTuKhoa, int? page)
        {
            if(Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int pageSize = 12;
            int pageNumber = (page ?? 1);

            var listSP = db.Products.Where(x => x.Name.Contains(sTuKhoa) || x.Producer.Name.Contains(sTuKhoa) || x.ProductType.Name.Contains(sTuKhoa) && x.Status !=false && x.InventoryNumber > 0 );
            ViewBag.TuKhoa = sTuKhoa;
            return View(listSP.OrderBy(x=>x.Name).ToPagedList(pageNumber,pageSize));
        }

        [HttpPost]
        // Post: TimKiem
        public ActionResult KQTimKiem(string sTuKhoa, int? page, FormCollection f)
        {
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int pageSize = 12;
            int pageNumber = (page ?? 1);

            var listSP = db.Products.Where(x => x.Name.Contains(sTuKhoa) || x.Producer.Name.Contains(sTuKhoa) || x.ProductType.Name.Contains(sTuKhoa) && x.Status != false && x.InventoryNumber > 0);
            ViewBag.TuKhoa = sTuKhoa;
            return View(listSP.OrderBy(x => x.Name).ToPagedList(pageNumber, pageSize));
        }
    }
}