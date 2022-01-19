using Jop_Offers_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Jop_Offers_Website.Controllers
{
    [Authorize (Roles ="Admins")]
    public class AdminController : Controller
    {
        ApplicationDbContext Db = new ApplicationDbContext();
        
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.countuser = Db.Users.Where(u => u.UserType != null).ToList().Count();
            ViewBag.jobs = Db.Jobs.ToList().Count();
            ViewBag.publisher = Db.Users.Where(u => u.UserType == "الناشرون").ToList().Count();
            ViewBag.Searcher = Db.Users.Where(u => u.UserType == "الباحثون").ToList().Count();
            ViewBag.Message = Db.MessageModels.ToList().Count();
            ViewBag.Apply = Db.ApplyForJobs.ToList().Count();
            return View();
        }

        //جلب كل المستخدمين

        public ActionResult GetAllUser(string userID)
        {
            var users = Db.Users.Where(r=>r.UserType != null).ToList();
            if(userID != null)
            {
                ViewBag.r = false;
                ViewBag.Con = "ملحوظه عند حذف هذا المستخدم سيتم حذف  كل ما يخصه من  وظائف او رسائل او طلبات تقدم";
                ViewBag.id = userID;
            }
            return View(users);
        }

        //حذف مستخدم

        public ActionResult DeleteUser(string id , string Act)
        {
            try
            {
                UserDelete(id);
                if(Act == "P")
                {
                    return RedirectToAction("GetUserByType" , new { type = "الناشرون" });
                }
                if(Act == "S")
                {
                    return RedirectToAction("GetUserByType", new { type = "الباحثون" });
                }
                else
                {
                    return RedirectToAction("GetAllUser");
                }
                
            }
            catch
            {
                if (Act == "P")
                {
                    return RedirectToAction("GetUserByType", new { type = "الناشرون", userID = id, test = "p" });
                }
                else if (Act == "S")
                {
                    return RedirectToAction("GetUserByType", new { type = "الباحثون", userID = id, test = "S" });
                }
                else
                {
                    return RedirectToAction("GetAllUser", new { userID = id });
                }   
            }
        }
        public ActionResult DeleteUserConfirmed(string id , string test)
        {
            ConfirmDeleteUser(id);
            return RedirectToAction("DeleteUser", new { id = id , Act = test});
        }
        public void UserDelete(string id)
        {
            var user = Db.Users.Find(id);
            var img = user.image;
            Db.Users.Remove(user);
            Db.SaveChanges();
            if (img != null)
            {
                string imgpath = Server.MapPath("~/Uploads/users") + "\\" + img;
                System.IO.File.Delete(imgpath);
            }
        }
        public void ConfirmDeleteUser(string id)
        {
            var jobs = Db.Jobs.Where(a => a.UserId == id).ToList();
            var Messages = Db.MessageModels.Where(a => a.userId == id).ToList();
            var Apply = Db.ApplyForJobs.Where(a => a.UserId == id).ToList();

            if (jobs.Count > 0)
            {
                foreach (var item in jobs)
                {
                    string img = Server.MapPath("~/Uploads") + "\\" + item.JobImage;
                    System.IO.File.Delete(img);
                    var ApplyFojob = Db.ApplyForJobs.Where(a => a.JobId == item.Id).ToList();
                    if (ApplyFojob.Count() > 0)
                    {
                        foreach (var item2 in ApplyFojob)
                        {
                            if (item2.cv != null)
                            {
                                string cvpath = Server.MapPath("~/Uploads/cv") + "\\" + item2.cv;
                                System.IO.File.Delete(cvpath);
                            }
                        }
                    }
                    Db.Jobs.Remove(item);
                    Db.SaveChanges();
                }
            }

            if (Messages.Count > 0)
            {
                foreach (var item in Messages)
                {
                    Db.MessageModels.Remove(item);
                    Db.SaveChanges();
                }
            }

            if(Apply.Count >0 )
            {
                foreach (var item in Apply)
                {
                    if (item.cv != null)
                    {
                        string path = Server.MapPath("~/Uploads/cv") + "\\" + item.cv;
                        System.IO.File.Delete(path);
                    }
                    Db.ApplyForJobs.Remove(item);
                    Db.SaveChanges();
                }
            }
        }
      
        // جلب الناشرين او الباحثين

        public ActionResult GetUserByType(string type, string userID, string test)
        {
            var users = Db.Users.Where(a => a.UserType == type).ToList();
            if (userID != null)
            {
                ViewBag.r = false;
                ViewBag.Con = "ملحوظه عند حذف هذا المستخدم سيتم حذف  كل ما يخصه من  وظائف او رسائل او طلبات تقدم";
                ViewBag.id = userID;
            }
            ViewBag.test = test;
            return View(users);
        }

        //رسائل المستخدمين

        public ActionResult GetAllMessage()
        {
            var M = Db.MessageModels.Include(a => a.user).ToList();
            return View(M);
        }

        //حذف الرسايل 

        public ActionResult DeletMessaga(int id)
        {
            var mesage = Db.MessageModels.Find(id);
            Db.MessageModels.Remove(mesage);
            Db.SaveChanges();
            return RedirectToAction("GetAllMessage");
        }
        
        //جلب جميع الوظائف

        public ActionResult GetAllJobs(string jobid )
        {
            if(jobid != null)
            {
                ViewBag.jobid = jobid;
                ViewBag.message = "هذه الوظيف التى تريد حذفها مقدم عليها  من قبل الباحثون هل انت متأكد من حذفها";
            }
            var jobs = Db.Jobs.ToList();
            return View(jobs);
        }

        public ActionResult DeleteJob(int id)
        {
            try
            {
                JobDelete(id);
                return RedirectToAction("GetAllJobs");
            }
            catch
            {
                return RedirectToAction("GetAllJobs" , new { jobid = id });
            }
            
        }

        public ActionResult ConfirmDeleteJob(int id)
        {
            confiremdeletjon(id);
            return RedirectToAction("DeleteJob" , new { id = id });
        }

        public void JobDelete(int id)
        {

            Job job = Db.Jobs.Find(id);
            string img = job.JobImage;
            var Apply = Db.ApplyForJobs.Where(a => a.JobId == job.Id).ToList();
            if (Apply.Count() > 0)
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
            string serverrpath = Server.MapPath("~/Uploads");
            string imgpath =serverrpath + "\\" + img;
           
            System.IO.File.Delete(imgpath);
        }

        public void confiremdeletjon(int id)
        {
            var Apply = Db.ApplyForJobs.Where(a => a.JobId == id).ToList();
            if(Apply.Count() > 0)
            {
                Db.ApplyForJobs.RemoveRange(Apply);
                Db.SaveChanges();
            }
        }
       
        public ActionResult DetialsJob(int id)
        {
            var job = Db.Jobs.Find(id);
            return View(job);
        }
        //جلب جميع الطلبات

        public ActionResult GetAllApplyForJobs()
        {
            var AllApply = Db.ApplyForJobs.ToList();
            return View(AllApply);
        }

        public ActionResult DeleteApply(int id)
        {
            var Apply = Db.ApplyForJobs.Find(id);
            if (Apply.cv != null)
            {
                string path = Server.MapPath("~/Uploads/cv") + "\\" + Apply.cv;
                System.IO.File.Delete(path);
            }
            Db.ApplyForJobs.Remove(Apply);
            Db.SaveChanges();
            return RedirectToAction("GetAllApplyForJobs");
        }

    }
}