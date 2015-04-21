using MyRecords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using System.Net;
using System.Data.Entity;
using System.Security.Claims;
using PagedList;

namespace MyRecords.Controllers
{
    [Authorize]
    public class ProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult AllProfiles(int? page)
        {
            string currentUserId = User.Identity.GetUserId();

            var AllProfiles = from p in db.Users
                            select p;

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(AllProfiles.OrderByDescending(p => p.LatestUpdate).ToPagedList(pageNumber, pageSize));
        }



        // GET: Profile/1
        public ActionResult MyProfile()
        {
            string currentUserId = User.Identity.GetUserId();

            var MyProfile = from p in db.Users
                            where(p.Id == currentUserId)
                           select p;

            var MyPosts = from p in db.Posts
                          where (p.PostReceiverId == currentUserId)
                          select p;

            var MyArtists = from a in db.Artists
                            where (a.UserId == currentUserId)
                            select a;

            ViewBag.MyPosts = MyPosts.OrderByDescending(p => p.DatePosted);
            ViewBag.MyArtists = MyArtists.OrderByDescending(a => a.DateCreated).Take(4);
            //ViewBag.MyProfile = MyProfile;

            return View(MyProfile);
        }

        public ActionResult ProfileDetails(string id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }

            if(id == User.Identity.GetUserId())
            {
                return RedirectToAction("MyProfile");
            }

            var ProfileDetails = from p in db.Users
                                 where (p.Id == id)
                                 select p;

            var DetailsPost = from p in db.Posts
                          where (p.PostReceiverId == id)
                          select p;

            var DetailsArtist = from a in db.Artists
                            where (a.UserId == id)
                            select a;

            ViewBag.DetailsArtist = DetailsArtist.OrderByDescending(d => d.DateCreated).Take(4);
            ViewBag.DetailsPost = DetailsPost.OrderByDescending(p => p.DatePosted); ;

            return View(ProfileDetails);
        }

        // GET: UpdateStatus/5
        public ActionResult UpdateStatus(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            } 
            
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStatus(string id, string StatusUpdate)
        {

            var user = db.Users.Find(id);

            if (ModelState.IsValid)
            {
                user.StatusUpdate = StatusUpdate;
                user.LatestUpdate = System.DateTime.Now;
                db.SaveChanges();
            }
            return RedirectToAction("MyProfile");
        }

        // GET: EditProfile/5
        public ActionResult EditProfile(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Include(a => a.Files).SingleOrDefault(a => a.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(string id, string FirstName, string LastName, int Age, string Country, string City, string Presentation, HttpPostedFileBase upload)
        {
            var user = db.Users.Find(id);

            if(user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                    {
                        if (user.Files.Any(f => f.FileType == FileType.Image))
                        {
                            db.Files.Remove(user.Files.First(f => f.FileType == FileType.Image));
                        }
                        var image = new File
                        {
                            FileName = System.IO.Path.GetFileName(upload.FileName),
                            FileType = FileType.Image,
                            ContentType = upload.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            image.Content = reader.ReadBytes(upload.ContentLength);
                        }
                        user.Files = new List<File> { image };
                    }

                user.FirstName = FirstName;
                user.LastName = LastName;
                user.Age = Age;
                user.Country = Country;
                user.City = City;
                user.Presentation = Presentation;
                db.SaveChanges();
            }
            return RedirectToAction("MyProfile");
        }
    }
}