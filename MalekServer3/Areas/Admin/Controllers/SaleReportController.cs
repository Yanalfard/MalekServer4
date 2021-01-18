using MalekServer3.Models;
using MalekServer3.Models.ViewModel;
using MalekServer3.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MalekServer3.Areas.Admin.Controllers
{
    [Authorize]
    public class SaleReportController : Controller
    {
        Heart heart;
        public SaleReportController()
        {
            heart = new Heart();
        }

        // GET: Admin/SaleReport
        public ActionResult SaleReports()
        {
            try
            {
                int id = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (id != 3)
                {
                    throw new Exception("Admin access only");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                List<TblClientProductRel> rels = heart.TblClientProductRels.Where(i => i.ClientId != id).DistinctBy(i => i.FactorId).OrderByDescending(i => i.id).ToList();
                return View(rels);
            }
            catch (Exception e)
            {
                throw e;
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }


        public ActionResult NamyeshFactor(int? id)
        {
            try
            {
                int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (idLogin != 3)
                {
                    throw new Exception("Admin access only");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                int DarsadDiscountId = 0;
                List<TblClientProductRel> rels = heart.TblClientProductRels.Where(i => i.IsFinaly && i.ClientId != idLogin && i.FactorId == id).ToList();
                List<ShowFactorViewModels> list = new List<ShowFactorViewModels>();
                if (rels[0].DiscountId > 0)
                {
                    DarsadDiscountId = heart.TblDiscounts.Find(rels[0].DiscountId).Discount;
                }
                foreach (var item in rels)
                {
                    TblProduct product = heart.TblProducts.Find(item.ProductId);
                    ShowFactorViewModels AddTbl = new ShowFactorViewModels()
                    {
                        ProductId = item.ProductId,
                        Count = item.Count,
                        Price = item.Discount == 0 ? item.Price : (item.Price - (long)(Math.Floor((double)item.Price * Convert.ToInt32(item.Discount) / 100))),
                        Name = item.Name,
                        Date = item.Date,
                        IsFinaly = false,
                        FactorId = item.FactorId,
                        ClientId = item.ClientId,
                        Discount = Convert.ToInt32(item.Discount),
                        SumFactor = item.SumFactor,
                        Sum = item.Discount == 0 ? item.Count * item.Price : (item.Price - (long)(Math.Floor((double)item.Price * Convert.ToInt32(item.Discount) / 100))) * item.Count,
                        DiscountId = Convert.ToInt32(item.DiscountId),
                        DarsadDiscountId = DarsadDiscountId,
                        ClientName = item.TblClient.Name,
                        ClientAddress = item.TblClient.Address,
                        ClientTellNo = item.TblClient.TellNo,
                        ClientEmail = item.TblClient.Email,
                        CatagoryName = product.TblCatagory.Name
                    };
                    list.Add(AddTbl);
                }
                return View(list);
            }
            catch (Exception e)
            {
                throw e;
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }



    }
}