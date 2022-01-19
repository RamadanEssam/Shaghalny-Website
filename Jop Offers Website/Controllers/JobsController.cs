using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Jop_Offers_Website.Models;
using Microsoft.AspNet.Identity;

namespace Jop_Offers_Website.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Jobs
        public ActionResult Index()
        {
            var jobs = db.Jobs.Include(j => j.category);
            return View(jobs.ToList());
        }

        // GET: Jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            viewbag();
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        public void viewbag()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CatogryName");
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "gen");
            ViewBag.ExperienceId = new SelectList(db.ExperienceLevels, "Id", "ExpLevel");
            ViewBag.QualificationId = new SelectList(db.Qualifications, "Id", "Quali");
            ViewBag.GovernoratesId = new SelectList(db.Governorates, "Id", "Name");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Job job , HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload == null)
                {
                    ViewBag.imgerr = "من فضلك قم باختيار صوره";
                    viewbag();
                    return View();
                }
                else
                {
                    var r = new Regex(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$");
                    if (!r.IsMatch(upload.FileName))
                    {
                        ViewBag.imgerr = "من فضلك قم احتيار صوره من نوع (png - jpg - gif )";
                        viewbag();
                        return View();
                    }
                    else
                    {
                        string path = System.IO.Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                        upload.SaveAs(path);
                        job.JobImage = upload.FileName;
                        job.UserId = User.Identity.GetUserId();
                    }
                }
                job.jobDate = DateTime.Now;
                db.Jobs.Add(job);
                db.SaveChanges();

                viewbag();
                ViewBag.message = "تم رفع الوظيفه بنجاح";
                return View(job); ;
            }
            ViewBag.message = "لم يتم رفع الوظيفه الخاصه بك !";
            viewbag();
            return View(job);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            viewbag();
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Job job, HttpPostedFileBase upload )
        {
            if (ModelState.IsValid)
            {
                if (upload == null)
                {
                    //كده لو معدلش الصوره هيحط نفس الصوره القديمه
                    string oldpath = job.JobImage;
                    job.JobImage = oldpath;
                }
                else
                {
                    var r = new Regex(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$");
                    if (!r.IsMatch(upload.FileName))
                    {
                        ViewBag.imgerr = "من فضلك قم احتيار صوره من نوع (png - jpg - gif )";
                        viewbag();
                        return View();
                    }
                    else
                    {
                        string old = Server.MapPath("~/Uploads") +"\\"+ job.JobImage;
                        System.IO.File.Delete(old);
                        string path = System.IO.Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                        upload.SaveAs(path);
                        job.JobImage = upload.FileName;
                    }
                }
                job.jobDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            viewbag();
            return View(job);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            string old = Server.MapPath("~/Uploads") + "\\" + job.JobImage;
            System.IO.File.Delete(old);
            db.Jobs.Remove(job);
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
