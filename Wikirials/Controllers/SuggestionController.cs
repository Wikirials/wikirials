using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wikirials.DAL;
using Wikirials.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Wikirials.ViewModels;

namespace Wikirials.Controllers
{
    public class SuggestionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Suggestion
        public ActionResult Index()
        {
            return View(db.Suggestions.ToList());
        }

        // GET: Suggestion/Details/5
        public ActionResult Details(int? id)
        {
            id = (int)Session["sug"];

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suggestion suggestion = db.Suggestions.Find(id);
            if (suggestion == null)
            {
                return HttpNotFound();
            }
            return View(suggestion);
        }

        // GET: Suggestion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Suggestion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Body,Date")] Suggestion suggestion)
        {
            if (ModelState.IsValid)
            {
                GroupView groupview = new GroupView();
                string userid = User.Identity.GetUserId();
                var currentuser = db.Users.SingleOrDefault(u => u.Id == userid);
                int group = (int)Session["id"];
                var currentgroup = db.Groups.FirstOrDefault(s => s.ID == group);

                suggestion.User = currentuser;
                suggestion.Group = currentgroup;

                suggestion.Date = DateTime.Now;

                db.Suggestions.Add(suggestion);
                db.SaveChanges();
                return RedirectToAction("Index", "Group");
            }

            return View(suggestion);
        }

        // GET: Suggestion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suggestion suggestion = db.Suggestions.Find(id);
            if (suggestion == null)
            {
                return HttpNotFound();
            }
            return View(suggestion);
        }

        // POST: Suggestion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Body,Date")] Suggestion suggestion)
        {
            if (ModelState.IsValid)
            {
                suggestion.Date = DateTime.Now;

                db.Entry(suggestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(suggestion);
        }

        // GET: Suggestion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suggestion suggestion = db.Suggestions.Find(id);
            if (suggestion == null)
            {
                return HttpNotFound();
            }
            return View(suggestion);
        }

        // POST: Suggestion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Suggestion suggestion = db.Suggestions.Find(id);
            db.Suggestions.Remove(suggestion);
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
