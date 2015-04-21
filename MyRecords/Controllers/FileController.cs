﻿using MyRecords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyRecords.Controllers
{
    [Authorize]
    public class FileController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
 
        // GET: File
        public ActionResult Index(int id)
        {
            var fileToRetrieve = db.Files.Find(id);
            return File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }
    }
}