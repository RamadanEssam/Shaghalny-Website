using Jop_Offers_Website.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jop_Offers_Website.Controllers
{
    public class MessageController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Message
        public ActionResult Index()
        {
            var M = db.MessageModels.Include(a=>a.user).ToList();
            return View(M);
        }

        // GET: Message/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Message/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Message/Create
        [HttpPost]
        [Authorize]
        public ActionResult Create( MessageModel me)
        {
            try
            {
                // TODO: Add insert logic here

                var userid = User.Identity.GetUserId();
                me.userId = userid;
                db.MessageModels.Add(me);
                db.SaveChanges();

                ViewBag.me = "تم ارسال رسالتك بنجاج شكرا على تواصلك معنا";
                ViewBag.st = "sended";
                return View(); ;
            }
            catch
            {
                ViewBag.me = "لم يتم ارسال رسالتك الى الادمن ! ";
                ViewBag.st = "error";
                return View();
            }
        }

        // GET: Message/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Message/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Message/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Message/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
