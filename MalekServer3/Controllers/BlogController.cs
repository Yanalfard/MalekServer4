using MalekServer3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MalekServer3.Controllers
{
    public class BlogController : Controller
    {
        Heart heart;

        public BlogController()
        {
            heart = new Heart();
        }
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Slider()
        {
            List<TblAd> selectAllAds = heart.TblAds.Where(i => i.PositionId == 1).Take(4).ToList();
            return PartialView(selectAllAds);
        }
        public ActionResult Slider2()
        {
            List<TblAd> selectAllAds = heart.TblAds.Where(i => i.PositionId == 1).Take(4).ToList();
            return PartialView();
        }
        public ActionResult Slider3()
        {
            List<TblAd> selectAllAds = heart.TblAds.Where(i => i.PositionId == 1).Take(4).ToList();
            return PartialView();
        }
        public ActionResult BlogHome()
        {
            return PartialView(heart.TblBlogs.OrderByDescending(i=>i.id).Take(4).ToList());
        }
        [Route("Blog/{id}/{name}")]
        public ActionResult Blog(int id,string name)
        {
            ViewBag.Name = name;
            return View(heart.TblBlogs.Find(id));
        }
        [Route("Blogs")]
        public ActionResult Blogs()
        {
            return View(heart.TblBlogs.ToList());
        }
    }
}