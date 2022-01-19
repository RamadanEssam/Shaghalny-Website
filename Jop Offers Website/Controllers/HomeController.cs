using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Jop_Offers_Website.Models;
using Microsoft.AspNet.Identity;

namespace Jop_Offers_Website.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext Db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.cat = Db.Categories.ToArray();
            return View(Db.Categories.ToList());
        }

        //لعرض تفاصيل الوظيفه
        public ActionResult Details(int jobId)
        {
            var job = Db.Jobs.Find(jobId);
            if (job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = jobId;
            return View(job);
        }

        //التقديم على الوظيفه 

        [Authorize(Roles = "الباحثون")]
        public ActionResult Apply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Apply(string Message, HttpPostedFileBase cv)
        {
            //هتحيب ال اى دى بتاع اليوزر الى عامل لوجن
            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];

            //كده مينفعش نفس اليوزر يقدم على نفس الوظيفه مرتين

            var check = Db.ApplyForJobs.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();

            if (check.Count < 1)
            {
                var job = new ApplyForJob();
                if (cv != null)
                {
                    cv.SaveAs(Server.MapPath($"~/Uploads/cv/{cv.FileName}"));
                    job.cv = cv.FileName;
                }

                job.JobId = JobId;
                job.UserId = UserId;
                job.ApplyDate = DateTime.Now;
                job.Message = Message;

                Db.ApplyForJobs.Add(job);
                Db.SaveChanges();
                ViewBag.Result = "تم التقدم الى تلك الوظيفه بنجاح";
            }
            else
            {
                ViewBag.Result = "لقد قمت بتقدم الى هذه الوظيفه من قبل !!!!";

            }
            return View();
        }

        //عشان اجيب الوظائف الى اليوز قدم عليها 
        [Authorize(Roles = "الباحثون")]
        public ActionResult GetJobsByUser()
        {
            var userid = User.Identity.GetUserId();
            var jobs = Db.ApplyForJobs.Where(a => a.UserId == userid).ToList();
            return View(jobs);
        }


        [Authorize(Roles = "الباحثون")]
        //التعديل على الوظائف التى قدم عليها اليوزر
        public ActionResult Edit(int id )
        {
            var job = Db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            
            return View(job);
        }

        [HttpPost]
        public ActionResult Edit(ApplyForJob job, HttpPostedFileBase cv)
        {
            if (ModelState.IsValid)
            {
                var oldcv = job.cv;
                job.ApplyDate = DateTime.Now;
                if(job.cv !=null && cv == null)
                {
                    job.cv = oldcv;
                }
                if(job.cv != null && cv != null)
                {
                    string path = Server.MapPath("~/Uploads/cv") + "\\" + oldcv;
                    System.IO.File.Delete(path);
                    cv.SaveAs(Server.MapPath($"~/Uploads/cv/{cv.FileName}"));
                    job.cv = cv.FileName;
                }
                if(job.cv == null && cv != null)
                {
                    cv.SaveAs(Server.MapPath($"~/Uploads/cv/{cv.FileName}"));
                    job.cv = cv.FileName;
                }
                Db.Entry(job).State = System.Data.Entity.EntityState.Modified;
                Db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(job);
        }

        [Authorize(Roles = "الباحثون")]
        public ActionResult Delete(int id)
        {
            var job = Db.ApplyForJobs.Find(id);
            if(job.cv != null)
            {
                string path = Server.MapPath("~/Uploads/cv") + "\\" + job.cv;
                System.IO.File.Delete(path);
            }
            Db.ApplyForJobs.Remove(job);
            Db.SaveChanges();
            return RedirectToAction("GetJobsByUser");
        }

        //الوظائف التى تقدم اليها اليوزر  
        [Authorize(Roles = "الناشرون")]
        public ActionResult GetJobsByPublisher()
        {
            var usreId = User.Identity.GetUserId();
            var jobs = from app in Db.ApplyForJobs
                       join Job in Db.Jobs
                       on app.JobId equals Job.Id
                       where Job.UserId == usreId
                       select app;
            //هنعمل كلاس جديد عشان نجمع بين عنوان الوظيفه والناس الى تقدموا اليها وعشان نخزن فيه ناتج عمليه الجروبين
            var grouped = from j in jobs
                          group j by j.job.JobTitle
                          into gr
                          select new JobsViewModel()
                          {
                              JobTitel = gr.Key,
                              Items = gr
                          };
            return View(grouped.ToList());
        }

        public ActionResult DownloadCv(string cv)
        {
            return File($"~/Uploads/cv/{cv}", "application/pdf");
;        }
        
        //هيجيب الوظائف التى تم رفعها من قبل الناشر 
        [Authorize (Roles ="الناشرون")]
        public ActionResult GetAllPublisherJobs()
        {
            var userId = User.Identity.GetUserId();
            var jobs = Db.Jobs.Where(u => u.UserId == userId).ToList();
            return View(jobs);
        }

        //حذف الوظيفه من قبل الناشر
        [Authorize(Roles = "الناشرون")]
        public ActionResult Deletejob(int id)
        {
            Job job = Db.Jobs.Find(id);
            string imgpath = Server.MapPath("~/Uploads") + "\\" + job.JobImage;
            System.IO.File.Delete(imgpath);
            var Apply = Db.ApplyForJobs.Where(a => a.JobId == job.Id).ToList();
            if(Apply.Count() > 0)
            {
                foreach (var item in Apply)
                {
                    if (item.cv != null)
                    {
                        string cvpath = Server.MapPath("~/Uploads/cv") + "\\" + item.cv;
                        System.IO.File.Delete(cvpath);
                    }
                }
            }
            Db.Jobs.Remove(job);
            Db.SaveChanges();
            return RedirectToAction("GetAllPublisherJobs");
        }

        //تعديل الوظيفه بتاع الناشر
        public void viewbag()
        {
            ViewBag.CategoryId = new SelectList(Db.Categories, "Id", "CatogryName");
            ViewBag.GenderId = new SelectList(Db.Genders, "Id", "gen");
            ViewBag.ExperienceId = new SelectList(Db.ExperienceLevels, "Id", "ExpLevel");
            ViewBag.QualificationId = new SelectList(Db.Qualifications, "Id", "Quali");
            ViewBag.GovernoratesId = new SelectList(Db.Governorates, "Id", "Name");
        }

        [Authorize(Roles = "الناشرون")]
        public ActionResult Editjob(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = Db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            viewbag();
            return View(job);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editjob(Job job, HttpPostedFileBase upload)
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
                        string old = Server.MapPath("~/Uploads") + "\\" + job.JobImage;
                        System.IO.File.Delete(old);
                        string path = System.IO.Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                        upload.SaveAs(path);
                        job.JobImage = upload.FileName;
                    }
                }
                job.jobDate = DateTime.Now;
                job.UserId = User.Identity.GetUserId();

                job.GenderId = job.Gender.Id;
                job.ExperienceId = job.Experience.Id;
                job.QualificationId = job.Qualification.Id;
                job.GovernoratesId = job.Governorates.Id;
                job.CategoryId = job.category.Id;

                Db.Entry(job).State = EntityState.Modified;

                Db.SaveChanges();
                return RedirectToAction("GetAllPublisherJobs");
            }
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            viewbag();
            return View(job);
        }
        
        // الاكشن الخاص بعمليه البحث فى الرئيسيه

        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search( string searchName)
        {
            var resalt = Db.Jobs.Where(a => a.JobTitle.Contains(searchName) 
            || a.JobContent.Contains(searchName)
            || a.category.CatogryName.Contains(searchName) 
            || a.category.CatogryDescription.Contains(searchName)
            || a.Governorates.Name.Contains(searchName)).ToList();

            return View(resalt);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult GetJobsByCat( string cat)
        {
            var jobs = Db.Jobs.Where(a => a.category.CatogryName == cat).ToList();
            return View(jobs);
        }
        
        
        [Authorize]
        public ActionResult profile (string id)
        {
            string Id = User.Identity.GetUserId();
            var user = Db.Users.Where(u => u.Id == Id).SingleOrDefault();

            return View(user);
        }


        [Authorize (Roles ="الباحثون")]
        public ActionResult SavedJobs()
        {
            var userid = User.Identity.GetUserId();
            var savedjobs = Db.SavedJobs.Where(u => u.userId == userid).Include(j=>j.job).ToList();
            return View(savedjobs);
        }

        public ActionResult DeleteSaveJob(int id)
        {
            var sjob = Db.SavedJobs.Where(j=>j.id == id).SingleOrDefault();
            Db.SavedJobs.Remove(sjob);
            Db.SaveChanges();
            return RedirectToAction("SavedJobs");
        }
        public string SaveJob(int id)
        {
            var userid = User.Identity.GetUserId();
            var savedjobs = Db.SavedJobs.ToList();
            if(savedjobs.Count > 0)
            {
                var sjob = Db.SavedJobs.Where(a => a.jobId == id && a.userId == userid).SingleOrDefault();
                if(sjob != null)
                {
                    return "تم حفظ هذه الوظيفه من قبل";
                }
                else
                {
                    var sjobs = new SavedJobs() { jobId = id, userId = userid };
                    Db.SavedJobs.Add(sjobs);
                    Db.SaveChanges();
                    return "تم حفظ الوظيفه";
                }
            }
            else
            {
                var savejob = new SavedJobs() { jobId = id, userId = userid };
                Db.SavedJobs.Add(savejob);
                Db.SaveChanges();
                return "تم حفظ الوظيفه";
            }
        }
    }
}