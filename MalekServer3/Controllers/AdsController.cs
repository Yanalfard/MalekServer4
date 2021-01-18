using MalekServer3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MalekServer3.Controllers
{
    public class AdsController : Controller
    {
        Heart heart;
        public AdsController()
        {
            heart = new Heart();
        }
        // GET: Ads
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
            List<TblAd> selectAllAds = heart.TblAds.Where(i => i.PositionId == 2).OrderByDescending(i=>i.id).Take(1).ToList();
            return PartialView(selectAllAds);
        }
        public ActionResult Slider3()
        {
            List<TblAd> selectAllAds = heart.TblAds.Where(i => i.PositionId == 3).OrderByDescending(i => i.id).Take(1).ToList();
            return PartialView(selectAllAds);
        }
        public ActionResult AdsRow1()
        {
            try
            {
                TblAd selectAllAds = heart.TblAds.FirstOrDefault(i => i.PositionId == 4);
                return PartialView(selectAllAds);
            }
            catch
            {
                return HttpNotFound("SelectAllAds has not worked");
            }
        }
        public ActionResult AdsRow2()
        {
            try
            {
                TblAd selectAllAds = heart.TblAds.FirstOrDefault(i => i.PositionId == 5);
                return PartialView(selectAllAds);
            }
            catch
            {
                return HttpNotFound("SelectAllAds has not worked");
            }
        }
        public ActionResult AdsRow3()
        {
            try
            {
                TblAd selectAllAds = heart.TblAds.FirstOrDefault(i => i.PositionId == 6);
                return PartialView(selectAllAds);
            }
            catch
            {
                return HttpNotFound("SelectAllAds has not worked");
            }
        }
        public ActionResult AdsRow4()
        {
            try
            {
                TblAd selectAllAds = heart.TblAds.FirstOrDefault(i => i.PositionId == 7);
                return PartialView(selectAllAds);
            }
            catch
            {
                return HttpNotFound("SelectAllAds has not worked");
            }
        }
        public ActionResult AdsRow5()
        {
            try
            {
                TblAd selectAllAds = heart.TblAds.FirstOrDefault(i => i.PositionId == 8);
                return PartialView(selectAllAds);
            }
            catch
            {
                return HttpNotFound("SelectAllAds has not worked");
            }
        }
    }
}