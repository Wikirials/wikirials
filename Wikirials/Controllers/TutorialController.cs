﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wikirials.Models;
using Wikirials.DAL;

namespace Wikirials.Controllers
{
    public class TutorialController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Tutorial/
        public ActionResult Index(string searchString)
        {
            var tutorial = from s in db.Tutorials
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                tutorial = tutorial.Where(s => s.Title.Contains(searchString));
            }
              
            return View(tutorial);
        }

        // GET: /Tutorial/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorial tutorial = db.Tutorials.Find(id);
            if (tutorial == null)
            {
                return HttpNotFound();
            }
            return View(tutorial);
        }

        // GET: /Tutorial/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Tutorial/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Body,Date,Classification,Type,ContentType")] Tutorial tutorial, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {
                    var pic = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Pic,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        pic.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    tutorial.Files = new List<File> { pic };
                }

                db.Tutorials.Add(tutorial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tutorial);
        }

        // GET: /Tutorial/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorial tutorial = db.Tutorials.Find(id);
            if (tutorial == null)
            {
                return HttpNotFound();
            }
            return View(tutorial);
        }

        // POST: /Tutorial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Body,Date,Classification,Type,ContentType")] Tutorial tutorial, HttpPostedFileBase upload)
        {

            if (ModelState.IsValid)
            {
                int tutorialid = tutorial.ID;
                var tutorialToUpdate = db.Tutorials.SingleOrDefault(u => u.ID == tutorialid);

                if (upload != null && upload.ContentLength > 0)
                {
                    if (tutorialToUpdate.Files.Any(f => f.FileType == FileType.Pic))
                    {
                        db.Files.Remove(tutorialToUpdate.Files.First(f => f.FileType == FileType.Pic));
                    }
                    var pic = new File
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Pic,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        pic.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    tutorialToUpdate.Files = new List<File> { pic };
                }

                db.Entry(tutorialToUpdate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tutorial);
        }

        // GET: /Tutorial/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorial tutorial = db.Tutorials.Find(id);
            if (tutorial == null)
            {
                return HttpNotFound();
            }
            return View(tutorial);
        }

        // POST: /Tutorial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tutorial tutorial = db.Tutorials.Find(id);
            db.Tutorials.Remove(tutorial);
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