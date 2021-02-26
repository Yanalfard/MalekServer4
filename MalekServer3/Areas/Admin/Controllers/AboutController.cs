using MalekServer3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MalekServer3.Areas.Admin.Controllers
{
    [Authorize]
    public class AboutController : Controller
    {
        Heart heart;
        public AboutController()
        {
            heart = new Heart();
        }
        // GET: Admin/About
        public ActionResult AboutUs()
        {
            try
            {
                int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (idLogin != 3)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TblConfig selectAdById = heart.TblConfigs.SingleOrDefault();
                return View(selectAdById);
            }
            catch
            {
                return HttpNotFound();

            }
        }
        [HttpPost]
        public ActionResult AboutUs(TblConfig config)
        {
            try
            {
                int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (idLogin != 3)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                heart.Entry(config).State = EntityState.Modified;
                heart.SaveChanges();
                return View(config);
            }
            catch
            {
                return HttpNotFound();

            }
        }

        public ActionResult Rules()
        {
            try
            {
                int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (idLogin != 3)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TblConfig selectAdById = heart.TblConfigs.SingleOrDefault();
                return View(selectAdById);
            }
            catch
            {
                return HttpNotFound();

            }
        }
        [HttpPost]
        public ActionResult Rules(TblConfig config)
        {
            try
            {
                int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (idLogin != 3)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                heart.Entry(config).State = EntityState.Modified;
                heart.SaveChanges();
                return View(config);
            }
            catch
            {
                return HttpNotFound();

            }
        }
        public ActionResult SendProduct()
        {
            try
            {
                int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (idLogin != 3)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TblConfig selectAdById = heart.TblConfigs.SingleOrDefault();
                return View(selectAdById);
            }
            catch
            {
                return HttpNotFound();

            }
        }
        [HttpPost]
        public ActionResult SendProduct(TblConfig config)
        {
            try
            {
                int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (idLogin != 3)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                heart.Entry(config).State = EntityState.Modified;
                heart.SaveChanges();
                return View(config);
            }
            catch
            {
                return HttpNotFound();

            }
        }
        public ActionResult Contact()
        {
            try
            {
                int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (idLogin != 3)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                List<TblContact> list = heart.TblContacts.ToList();
                return View(list);
            }
            catch
            {
                return HttpNotFound();

            }
        }

        public ActionResult DeleteContact(int id)
        {
            heart.TblContacts.Remove(heart.TblContacts.SingleOrDefault(i => i.id == id));
            heart.SaveChanges();
            return RedirectToAction("Contact");
        }
    }
}