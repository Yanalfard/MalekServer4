using MalekServer3.Models;
using MalekServer3.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MalekServer3.Controllers
{
    public class ShopCartController : Controller
    {
        Heart heart;
        public ShopCartController()
        {
            heart = new Heart();
        }
        // GET: ShopCart
        public ActionResult Index()
        {
            return View();
        }
        [Route("FinalShopCart")]
        public ActionResult FinalShopCart()
        {
            return View();
        }
        public ActionResult Order()
        {
            try
            {
                return PartialView(getListOrder());
            }
            catch
            {
                return RedirectToAction("/ErrorPage/NotFound");
            }
        }
        public ActionResult Order2()
        {
            try
            {
                return PartialView(getListOrder());
            }
            catch
            {
                return RedirectToAction("/ErrorPage/NotFound");
            }
        }
        public ActionResult ShowCart()
        {
            try
            {
                List<ShopCartViewModel> list = new List<ShopCartViewModel>();
                if (Session["ShopCart"] != null)
                {
                    List<ShopCartItem> listShop = (List<ShopCartItem>)Session["ShopCart"];

                    foreach (var item in listShop)
                    {
                        var product = heart.TblProducts.ToList().Where(p => p.id == item.ProductID).Select(p => new
                        {
                            p.Name,
                            p.TblProductImageRels.FirstOrDefault().TblImage.Image,
                            p.Price,
                            p.Discount,
                        }).Single();
                        list.Add(new ShopCartViewModel()
                        {
                            Count = item.Count,
                            ProductID = item.ProductID,
                            Title = product.Name,
                            Discount = product.Discount,
                            DiscountId = 0,
                            ImageName = product.Image,
                            // Price = product.Discount == 0
                            Price = product.Discount == 0 ? product.Price * item.Count : (product.Price - (long)(Math.Floor((double)product.Price * product.Discount / 100))) * item.Count,

                        });
                    }
                }
                return PartialView(list);
            }
            catch
            {
                return RedirectToAction("/ErrorPage/NotFound");
            }
        }

        List<ShopCartViewModel> getListOrder()
        {
            List<ShopCartViewModel> list = new List<ShopCartViewModel>();

            if (Session["ShopCart"] != null)
            {
                List<ShopCartItem> listShop = Session["ShopCart"] as List<ShopCartItem>;

                foreach (var item in listShop)
                {
                    var product = heart.TblProducts.ToList().Where(p => p.id == item.ProductID).Select(p => new
                    {
                        p.Name,
                        p.TblProductImageRels.FirstOrDefault().TblImage.Image,
                        p.Price,
                        p.Discount
                    }).Single();

                    list.Add(new ShopCartViewModel()
                    {
                        Count = item.Count,
                        ProductID = item.ProductID,
                        Price = product.Discount == 0 ? product.Price : (product.Price - (long)(Math.Floor((double)product.Price * product.Discount / 100))),
                        ImageName = product.Image,
                        Title = product.Name,
                        Discount = product.Discount,
                        DiscountId = 0,
                        Sum = product.Discount == 0 ? product.Price * item.Count : (product.Price - (long)(Math.Floor((double)product.Price * product.Discount / 100))) * item.Count,
                    });
                    long sum = list.Sum(i => i.Sum);
                    list.ForEach(i => i.SumFinal = sum);
                }
            }
            return list;
        }
        public ActionResult CommandOrder(int id, int count)
        {
            try
            {
                List<ShopCartItem> listShop = Session["ShopCart"] as List<ShopCartItem>;
                int index = listShop.FindIndex(p => p.ProductID == id);
                if (count == 0)
                {
                    listShop.RemoveAt(index);
                }
                else
                {
                    listShop[index].Count = count;
                    TblProduct tblProduct = heart.TblProducts.Find(id);
                    if (tblProduct.Count < listShop[index].Count)
                    {
                        listShop[index].Count = tblProduct.Count;
                    }
                }
                Session["ShopCart"] = listShop;

                return PartialView("Order", getListOrder());
            }
            catch
            {
                return RedirectToAction("/ErrorPage/NotFound");
            }
        }
        public ActionResult CommandOrder2(int id, int count)
        {
            try
            {
                List<ShopCartItem> listShop = Session["ShopCart"] as List<ShopCartItem>;
                int index = listShop.FindIndex(p => p.ProductID == id);
                if (count == 0)
                {
                    listShop.RemoveAt(index);
                }
                else
                {
                    listShop[index].Count = count;
                    TblProduct tblProduct = heart.TblProducts.Find(id);
                    if (tblProduct.Count < listShop[index].Count)
                    {
                        listShop[index].Count = tblProduct.Count;
                    }
                }
                Session["ShopCart"] = listShop;

                return PartialView("Order2", getListOrder());
            }
            catch
            {
                return RedirectToAction("/ErrorPage/NotFound");
            }
        }
        [HttpPost]
        public ActionResult CheckDiscount(string name)
        {
            try
            {

                TblDiscount discout = heart.TblDiscounts.FirstOrDefault(i => i.Name == name && i.Count != 0);
                if (discout != null && discout.Count > 0 && discout.Discount > 0)
                {
                    List<ShopCartViewModel> list = new List<ShopCartViewModel>();

                    if (Session["ShopCart"] != null)
                    {
                        List<ShopCartItem> listShop = Session["ShopCart"] as List<ShopCartItem>;

                        foreach (var item in listShop)
                        {
                            var product = heart.TblProducts.ToList().Where(p => p.id == item.ProductID).Select(p => new
                            {
                                p.Name,
                                p.TblProductImageRels.FirstOrDefault().TblImage.Image,
                                p.Price,
                                p.Discount
                            }).Single();

                            list.Add(new ShopCartViewModel()
                            {
                                Count = item.Count,
                                ProductID = item.ProductID,
                                Price = product.Discount == 0 ? product.Price : (product.Price - (long)(Math.Floor((double)product.Price * product.Discount / 100))),
                                ImageName = product.Image,
                                Title = product.Name,
                                Discount = product.Discount,
                                DiscountId = discout.id,
                                Sum = product.Discount == 0 ? product.Price * item.Count : (product.Price - (long)(Math.Floor((double)product.Price * product.Discount / 100))) * item.Count,
                            });
                            long sum = list.Sum(i => i.Sum);
                            sum = sum - (int)(Math.Floor((double)sum * discout.Discount / 100));
                            list.ForEach(i => i.SumFinal = sum);
                        }
                    }
                    return PartialView("Order", list);

                }
                return Json(new { success = true, responseText = "کد تخفیف موجود نیست" }, JsonRequestBehavior.AllowGet);

                //List<TblDiscount> discout = heart.TblDiscounts.Where(i => i.Name == name && i.Count != 0).ToList();
                //if (discout.Count > 0)
                //{
                //    return Json(new { success = true, responseText = discout[0].Discount, responseId = discout[0].id }, JsonRequestBehavior.AllowGet);
                //}
                //return Json(new { success = false, responseText = "کد تخفیف موجود نیست" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return RedirectToAction("/ErrorPage/NotFound");
            }
        }
        [Authorize]
        public ActionResult Payment(TblClientProductRel tblClientProductRel)
        {
            try
            {
                int idLogin = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (idLogin == 3)
                {
                    return Redirect("/Login");
                }
                List<TblClientProductRel> selectFatorId = heart.TblClientProductRels.ToList();
                int numFactor;
                if (selectFatorId.Count == 0)
                {
                    numFactor = 1000;
                }
                else
                {
                    numFactor = selectFatorId[selectFatorId.Count - 1].FactorId + 1;
                }
                var ClientId = User.Identity.Name.Split('|')[1];
                if (Session["ShopCart"] != null)
                {
                    List<ShopCartItem> cart = Session["ShopCart"] as List<ShopCartItem>;
                    foreach (var ShopCartItem in cart)
                    {
                        var product = heart.TblProducts.SingleOrDefault(i => i.id == ShopCartItem.ProductID);
                        TblClientProductRel AddTbl = new TblClientProductRel()
                        {
                            ProductId = ShopCartItem.ProductID,
                            Count = ShopCartItem.Count,
                            Price = product.Price,
                            Name = product.Name,
                            Date = DateTime.Now.ToShortDateString(),
                            IsFinaly = false,
                            FactorId = numFactor,
                            ClientId = Convert.ToInt32(ClientId),
                            Discount = product.Discount,
                            SumFactor = tblClientProductRel.SumFactor,
                            DiscountId = tblClientProductRel.DiscountId
                        };
                        heart.TblClientProductRels.Add(AddTbl);
                    }

                    heart.SaveChanges();
                }

                System.Net.ServicePointManager.Expect100Continue = false;
                ZarinPal.PaymentGatewayImplementationServicePortTypeClient zp = new ZarinPal.PaymentGatewayImplementationServicePortTypeClient();
                string Authority;

                int Status = zp.PaymentRequest("a282a431-19d8-43ee-ae50-e3d056519667", tblClientProductRel.SumFactor / 10, "ملک سرور", "noreplaymalekserver@gmail.com", "09190995384", @ConfigurationManager.AppSettings["MyDomain"] + "/ShopCart/Verify/" + numFactor, out Authority);
                if (Status == 100)
                {
                    Response.Redirect("https://www.zarinpal.com/pg/StartPay/" + Authority);

                    //Test
                    //return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + Authority);
                }
                else
                {
                    ViewBag.Error = "Error : " + Status;
                }
                return View();
            }
            catch (Exception)
            {
                return Redirect("/Login");
            }
        }

        public ActionResult Verify(int id)
        {
            List<TblClientProductRel> order = heart.TblClientProductRels.Where(i => i.FactorId == id).ToList();

            if (Request.QueryString["Status"] != "" && Request.QueryString["Status"] != null && Request.QueryString["Authority"] != "" && Request.QueryString["Authority"] != null)
            {
                if (Request.QueryString["Status"].ToString().Equals("OK"))
                {
                    int Amount = order[0].SumFactor / 10;
                    long RefID;
                    System.Net.ServicePointManager.Expect100Continue = false;
                    ZarinPal.PaymentGatewayImplementationServicePortTypeClient zp = new ZarinPal.PaymentGatewayImplementationServicePortTypeClient();

                    int Status = zp.PaymentVerification("a282a431-19d8-43ee-ae50-e3d056519667", Request.QueryString["Authority"].ToString(), Amount, out RefID);
                    if (Status == 100)
                    {
                        foreach (var item in order)
                        {
                            TblProduct tblProduct= heart.TblProducts.Find(item.ProductId);
                            tblProduct.Count -= item.Count;
                            item.IsFinaly = true;
                        }
                        if (order[0].DiscountId > 0)
                        {
                            int numberdis = Convert.ToInt32(order.FirstOrDefault().DiscountId);
                            TblDiscount discountToUpdate = heart.TblDiscounts.FirstOrDefault(i => i.id == numberdis);
                            discountToUpdate.Count--;
                        }
                        heart.SaveChanges();
                        List<ShopCartItem> cart = Session["ShopCart"] as List<ShopCartItem>;
                        cart.Clear();
                        ViewBag.IsSuccess = true;
                        ViewBag.RefId = RefID;
                        // Response.Write("Success!! RefId: " + RefID);
                        return Redirect("/UserPanel/Home/NamyeshFactor/" + id);
                    }
                    else
                    {
                        ViewBag.Status = Status;
                    }
                }
                else
                {
                    ViewBag.Status = Request.QueryString["Authority"].ToString() ;
                }
            }
            else
            {
                Response.Write("Invalid Input");
            }
            return View();
        }
    }
}