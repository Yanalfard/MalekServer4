using MalekServer3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MalekServer3.Controllers
{
    public class CategoryController : Controller
    {
        Heart heart;
        public CategoryController()
        {
            heart = new Heart();
        }
        // GET: Category
        public ActionResult Categorys()
        {
            return PartialView(heart.TblCatagories.ToList());
        }
    }
}