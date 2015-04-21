using MyRecords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyRecords.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var artists = (from a in db.Artists
                             select a).OrderByDescending(a => a.ArtistId).Take(4);

            ViewBag.albums = (from alb in db.Albums
                              select alb).OrderByDescending(alb => alb.AlbumId).Take(3).ToList();

            ViewBag.reviews = (from r in db.Reviews
                               select r).OrderByDescending(r => r.ReviewId).Take(3).ToList();

            ViewBag.users = (from u in db.Users
                             select u).OrderByDescending(r => r.LatestUpdate).Take(3).ToList();

            return View(artists.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}