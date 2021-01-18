using MalekServer3.Models;
using MalekServer3.Models.ObjectClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MalekServer3.Controllers
{
    public class SearchController : Controller
    {
        Heart heart;

        public SearchController()
        {
            heart = new Heart();
        }
        // GET: Search
        public ActionResult Index(string q=" ")
        {
            try
            {
                ViewBag.Search = q;
                List<TblProduct> products = heart.TblProducts.ToList();
                List<OcProduct> newProducts = new List<OcProduct>();
                foreach (TblProduct i in products)
                {
                    newProducts.Add(new OcProduct(i));
                }
                return View(newProducts.Where(p => p.Name.Contains(q) || p.Catagory.Name.Contains(q)).Distinct());
            }
            catch
            {
                return HttpNotFound("SelectAllProducts has not worked");
            }
        }
    }
}