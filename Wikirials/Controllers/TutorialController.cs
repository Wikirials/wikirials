using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wikirials.Models;
using Wikirials.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using Wikirials.ViewModels;

namespace Wikirials.Controllers
{
    public class TutorialController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Tutorial/
        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            var tutorial = from s in db.Tutorials.Include(p => p.FileMains).Include(u => u.User)
                           select s;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                tutorial = tutorial.Where(s => s.Title.Contains(searchString));
            }
              
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(tutorial.OrderByDescending(d => d.Date).ToPagedList(pageNumber, pageSize));
        }

        // GET: /Tutorial/Details/5
        public ActionResult Details(int? id)
        {
            var comment = from s in db.Comments.Include(t => t.Tutorial).Include(f => f.User.Files)
                           select s;

            TutorialComment tutorialcomment = new TutorialComment();

            tutorialcomment.Comments = comment;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tutorial tutorial = db.Tutorials.Find(id);

            tutorialcomment.Tutorial = tutorial;

            if (tutorialcomment == null)
            {
                return HttpNotFound();
            }

            return View(tutorialcomment);
        }

        // POST: Comment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details([Bind(Include = "ID,Text,DateTime")] Comment comment, int? id)
        {
            TutorialComment tutorialcomment = new TutorialComment();

            tutorialcomment.Comment = comment;

            if (ModelState.IsValid)
            {
                string userid = User.Identity.GetUserId();
                var currentuser = db.Users.SingleOrDefault(u => u.Id == userid);

                var currentutorial = db.Tutorials.SingleOrDefault(v => v.ID == id);


                tutorialcomment.Comment.User = currentuser;
                tutorialcomment.Comment.Tutorial = currentutorial;
                tutorialcomment.Comment.DateTime = DateTime.Now;

                db.Comments.Add(tutorialcomment.Comment);
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            return View(tutorialcomment);
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
            string userid = User.Identity.GetUserId();
            var currentuser = db.Users.SingleOrDefault(u => u.Id == userid);

            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {
                    var pic = new FileMain
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Pic,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        pic.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    tutorial.FileMains = new List<FileMain> { pic };
                }

                tutorial.User = currentuser;
                //tutorial.Date = DateTime.Now;

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
                //int tutorialid = tutorial.ID;
                //var tutorialToUpdate = db.Tutorials.SingleOrDefault(u => u.ID == tutorialid);

                if (upload != null && upload.ContentLength > 0)
                {
                    if (tutorial.FileMains.Any(f => f.FileType == FileType.Pic))
                    {
                        db.FileMains.Remove(tutorial.FileMains.First(f => f.FileType == FileType.Pic));
                    }
                    var pic = new FileMain
                    {
                        FileName = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Pic,
                        ContentType = upload.ContentType
                    };
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        pic.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    tutorial.FileMains = new List<FileMain> { pic };
                }
                
                db.Entry(tutorial).State = EntityState.Modified;
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
            //Tutorial tutorial = db.Tutorials.Include(c => c.Comments)
            //    .Where(i => i.ID == id)
            //    .Single();
            var comment = tutorial.Comments;

            foreach (var item in comment.ToList())
            {
                db.Comments.Remove(item);
            }

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
