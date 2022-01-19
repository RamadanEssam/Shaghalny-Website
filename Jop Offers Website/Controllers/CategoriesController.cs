using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Jop_Offers_Website.Models;

namespace Jop_Offers_Website.Controllers
{
    [Authorize(Roles = "Admins")]
    public class CategoriesController : Controller
    {
        private AdminController Admin = new AdminController();
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories
        public ActionResult Index( string cat)
        {
            if (cat != null)
            {
                ViewBag.catId = cat;
                ViewBag.message = "هذا القسم يحتوى على وظائف عند حذف القسم سيتم حذف جميع الوظائف به";
            }
            return View(db.Categories.ToList());
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CatogryName,CatogryDescription")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }
        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                var Cat = db.Categories.Find(id);
                db.Categories.Remove(Cat);
                db.SaveChanges();
                return RedirectToAction("Index");
                
            }
            catch
            {
                return RedirectToAction("Index", new { cat = id });
            }
        }
        public ActionResult DeleteConfirmed(int id)
        {
            var jobs = db.Jobs.Where(c => c.CategoryId == id).ToList();
            try
            {
                if(jobs.Count() > 0)
                {
                    foreach (var item in jobs)
                    {
                        JobDelete(item.Id);
                    }
                }
                return RedirectToAction("Delete" , new { id = id });
            }
            catch
            {
                if (jobs.Count() > 0)
                {
                    foreach (var item in jobs)
                    {
                        Admin.ConfirmDeleteJob(item.Id);
                    }
                }
                return RedirectToAction("DeleteConfirmed", new { id = id });
            }  
        }
        public void JobDelete(int id)
        {
            Job job = db.Jobs.Find(id);
            string img = job.JobImage;

            db.Jobs.Remove(job);
            db.SaveChanges();
            string serverrpath = Server.MapPath("~/Uploads");
            string imgpath = serverrpath + "\\" + img;
            System.IO.File.Delete(imgpath);
        }
        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CatogryName,CatogryDescription")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }


        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
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
