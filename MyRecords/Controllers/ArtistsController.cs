using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyRecords.Models;
using System.Data.Entity.Infrastructure;
using PagedList;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace MyRecords.Controllers
{
    [Authorize]
    public class ArtistsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Artists
        public ActionResult Index(string searchString, string currentFilter, int? page)
        {

            string currentUserId = User.Identity.GetUserId();

            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var artists = from s in db.Artists where(s.UserId == currentUserId)
                          select s;
                
            if (!String.IsNullOrEmpty(searchString))
            {
                artists = artists.Where(s => s.ArtistName.Contains(searchString)).Include(a => a.Files);
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);

            return View(artists.OrderBy(a => a.ArtistName).ToPagedList(pageNumber, pageSize));
        }

        public class XMLReader
        {
            public List<Artist> ReturnListOfArtists()
            {
                string xmlData = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Artists.xml");//Path of the xml script  
                DataSet ds = new DataSet();//Using dataset to read xml file  
                ds.ReadXml(xmlData);
                var artists = new List<Artist>();
                artists = (from rows in ds.Tables[0].AsEnumerable()
                           select new Artist
                           {
                               ArtistId = Convert.ToInt32(rows[0].ToString()), //Convert row to int  
                               ArtistName = rows[1].ToString(),
                               Country = rows[2].ToString(),
                           }).ToList();
                return artists;
            }
        }

        public ActionResult ReturnListOfArtists()
        {
            XMLReader readXml = new XMLReader();
            var artists = readXml.ReturnListOfArtists();

            return View(artists.OrderBy(a => a.ArtistName).ToList());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Artist artist = db.Artists.Include(s => s.Files).SingleOrDefault(s => s.ArtistId == id);

            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        public ActionResult FindArtistByName(string name)
        {
            var artists = from a in db.Artists
                          select a;

            if (!String.IsNullOrEmpty(name))
            {
                artists = artists.Where(s => s.ArtistName.Contains(name));
            }

            return View(artists.ToList());
        }

        // GET: Artists/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtistId,ArtistName,Country")] Artist artist, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
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
                        artist.Files = new List<File> { image };
                    }
                    artist.DateCreated = DateTime.Now;
                    artist.UserId = User.Identity.GetUserId();
                    db.Artists.Add(artist);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(artist);
        }

        // GET: Artists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Include(a => a.Files).SingleOrDefault(s => s.ArtistId == id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, HttpPostedFileBase upload)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var artistToUpdate = db.Artists.Find(id);
            if (TryUpdateModel(artistToUpdate, "",
                new string[] { "ArtistName", "Country" }))
            {
                try
                {
                    if (upload != null && upload.ContentLength > 0)
                    {
                        if (artistToUpdate.Files.Any(f => f.FileType == FileType.Image))
                        {
                            db.Files.Remove(artistToUpdate.Files.First(f => f.FileType == FileType.Image));
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
                        artistToUpdate.Files = new List<File> { image };
                    }
                    db.Entry(artistToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Details/"+id);
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(artistToUpdate);
        }
        public ActionResult RemovePicture(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Artist artist = db.Artists.Include(a => a.Files).SingleOrDefault(s => s.ArtistId == id);
            if (artist.Files.Any(f => f.FileType == FileType.Image))
            {
                db.Files.Remove(artist.Files.First(f => f.FileType == FileType.Image));
                db.SaveChanges();
            }

            return RedirectToAction("Details/" + id);
        }

        // GET: Artists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }



        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artist artist = db.Artists.Find(id);
            db.Artists.Remove(artist);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
