using MalekServer3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MalekServer3.Controllers
{
    public class CommentController : Controller
    {
        Heart heart;

        public CommentController()
        {
            heart = new Heart();
        }
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult AddComment(string id, string raiting, string comment)
        {
            try
            {
                string userId = User.Identity.Name.Split('|')[1];
                int ConverRating = Convert.ToInt32(raiting);
                ConverRating *= 20;
                int id2 = Convert.ToInt32(id);
                if (id == "" || raiting == "" || comment == "" || userId == "")
                {
                    return Json(new { success = false, responseText = "خطا در ثبت پیام لطفا دقت فرمایید" }, JsonRequestBehavior.AllowGet);
                }
                TblComment addcomment = new TblComment()
                {
                    Body = comment,
                    Date = DateTime.Now.ToShortDateString(),
                    FromId = Convert.ToInt32(userId),
                    IsValid = false,
                };
                var commentId = heart.TblComments.Add(addcomment);
                TblProductCommentRel addCommentId = new TblProductCommentRel
                {
                    CommentId = commentId.id,
                    ProductId = Convert.ToInt32(id)
                };
                TblProduct product = heart.TblProducts.Find(id2);
                int count = heart.TblProductCommentRels.Where(j => j.ProductId == id2).ToList().Count;
                if (count == 0)
                {
                    product.Raiting = ConverRating;
                }
                else
                {
                    product.Raiting = Convert.ToInt32((product.Raiting * (count - 1) + ConverRating) / count);
                }
                TblProductCommentRel productCommentRel = heart.TblProductCommentRels.Add(addCommentId);
                heart.SaveChanges();
                return Redirect("/ViewProduct/"+id2+"/"+ product.Name);

            }
            catch
            {
                return Json(new { success = false, responseText = "خطا در ثبت پیام لطفا دقت فرمایید" }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}