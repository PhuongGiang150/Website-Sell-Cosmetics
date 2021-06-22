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
    public class PostController : Controller
    {
        CosmeticsStoreDbContext db = new CosmeticsStoreDbContext();

        // GET: Post
        public ActionResult NewList(int? page)
        {
            int PageSize = 5;
            int PageNumber = (page ?? 1);
            var lstKH = db.News.OrderBy(x=>x.DisplayOrder).ToPagedList(PageNumber, PageSize);
            return View(lstKH);
        }
        public ActionResult NewDetail(int? id, string tenNews)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News post = db.News.SingleOrDefault(x => x.NewID == id && x.Status != false);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
        public ActionResult AboutPost()
        {
            var post = db.Abouts.SingleOrDefault(x => x.Status != false);
            return View(post);

        }
        public ActionResult ContactPost()
        {
            var post = db.Contacts.SingleOrDefault(x => x.Status != false);
            return View(post);
        }
    }
}