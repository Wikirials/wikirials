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
using Wikirials.ViewModels;

namespace Wikirials.Controllers
{
    public class GroupController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Group
        public ActionResult Index()
        {
            return View(db.Groups.ToList());
        }

        // GET: Group/Details/5
        public ActionResult Details(int? id)
        {
            GroupView groupview = new GroupView();
            //var groupview = new GroupView();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);

            groupview.Group = group;

            if (groupview.Group == null)
            {
                return HttpNotFound();
            }

            groupview.Suggestions = db.Suggestions.ToList();
            //groupview.Tutorials = db.Tutorials.ToList();

            //if (id != null)
            //{
            //    ViewBag.ID = id.Value;
            //    groupview.SubCategories = viewModel.Categories.FirstOrDefault(c => c.ID == id).SubCategory;
            //}

            //if (subcatID != null)
            //{
            //    ViewBag.SubCategoryID = subcatID.Value;
            //    viewModel.Posts = db.Posts.Include(p => p.FilesPost).Where(x => x.SubCategoryID == subcatID);
            //}
            Session["id"] = id;

            return View(groupview);
        }

        // GET: Group/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Group/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Password")] Group group)
        {
            if (ModelState.IsValid)
            {
                string userid = User.Identity.GetUserId();
                var currentuser = db.Users.SingleOrDefault(u => u.Id == userid);

                group.User = currentuser;
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: Group/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Group/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Password")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: Group/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
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
