using MalekServer3.Models;
using MalekServer3.Models.ViewModel;
using MalekServer3.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MalekServer3.Controllers
{
    public class HomeController : Controller
    {
        Heart heart;
        public HomeController()
        {
            heart = new Heart();
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [Route("ContactUs")]
        public ActionResult ContactUs()
        {
            return View();
        }
        [Route("AboutUs")]
        public ActionResult AboutUs()
        {
            try
            {
                TblConfig selectAdById = heart.TblConfigs.SingleOrDefault();
                return View(selectAdById);
            }
            catch
            {
                return HttpNotFound();
            }
        }
        [Route("Rules")]
        public ActionResult Rules()
        {
            try
            {
                TblConfig selectAdById = heart.TblConfigs.SingleOrDefault();
                return View(selectAdById);
            }
            catch
            {
                return HttpNotFound();
            }
        }
        [Route("SendProduct")]
        public ActionResult SendProduct()
        {
            try
            {
                TblConfig selectAdById = heart.TblConfigs.SingleOrDefault();
                return View(selectAdById);
            }
            catch
            {
                return HttpNotFound();
            }
        }

        public ActionResult Contact()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Contact(ContactViewModel contact)
        {
            if (ModelState.IsValid)
            {
                TblContact addContact = new TblContact();
                addContact.Body = contact.Body;
                addContact.Title = contact.Title;
                addContact.TellNo = contact.TellNo;
                heart.TblContacts.Add(addContact);
                heart.SaveChanges();
                return JavaScript("doneSendContact()");
            }
            return PartialView("Contact", contact);
        }
    }
}