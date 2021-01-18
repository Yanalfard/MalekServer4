﻿using MalekServer3.Models.ObjectClass;
using MalekServer3.Models;
using MalekServer3.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.Security;
using MalekServer3.Utilities;

namespace MalekServer3.Areas.UserPanel.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        Heart heart;
        public HomeController()
        {
            heart = new Heart();
        }
        // GET: UserPanel/Home
        public ActionResult Index()
        {
            try
            {

                if (User.Identity.Name == null)
                {
                    throw new NullReferenceException();
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //int userId = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name.Split('|')[1]);
                int id = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                string userName = User.Identity.Name.Split('|')[0];
                if (id == 3)
                {
                    throw new Exception("Admin access only");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                TblClient client = heart.TblClients.SingleOrDefault(i => i.id == id);
                UpdateRegisterViewModle selectRegisterById = new UpdateRegisterViewModle
                {
                    id = client.id,
                    Name = client.Name,
                    IdentificationNo = client.IdentificationNo,
                    Username = client.Username,
                    TellNo = client.TellNo,

                };
                if (client == null)
                {
                    throw new NullReferenceException();
                    //return HttpNotFound();
                }
                return View(selectRegisterById);
            }
            catch (Exception e)
            {
                throw e;
                //return HttpNotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UpdateRegisterViewModle register)
        {
            try
            {
                int id = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (id == 3)
                {
                    throw new Exception("Admin access only");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                register.Address = register.Email;
                register.Auth = register.Email;
                string hashPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(register.OldPassword, "MD5");
                TblClient client = heart.TblClients.SingleOrDefault(i => i.id == register.id);
                if (client.Password != hashPassword)
                {
                    ModelState.AddModelError("OldPassword", "رمز قدیمی اشتباه است");
                    return View(register);
                }
                // go ahead and write code to validate username password against database
                var TestEmailAndUser = heart.TblClients.Where(i => i.id != register.id);
                foreach (var item in TestEmailAndUser)
                {
                    if (item.Username == register.Username)
                    {
                        ModelState.AddModelError("Username", "نام کاربری تکراریست");
                        return View(register);
                    }
                    if (item.Email == register.Email)
                    {
                        ModelState.AddModelError("Username", "ایمیل تکراریست");
                        return View(register);
                    }
                    if (item.IdentificationNo == register.IdentificationNo)
                    {
                        ModelState.AddModelError("IdentificationNo", "کد ملی تکراریست");
                        return View(register);
                    }
                    if (item.TellNo == register.TellNo)
                    {
                        ModelState.AddModelError("TellNo", "شماره تلفن تکراریست");
                        return View(register);
                    }
                }
                var tblClient = heart.TblClients.SingleOrDefault(i => i.id == register.id);
                tblClient.IdentificationNo = register.IdentificationNo.Trim().ToLower();
                tblClient.Name = register.Name.Trim().ToLower();
                tblClient.TellNo = register.TellNo.Trim().ToLower();
                tblClient.Username = register.Username.Trim().ToLower();
                tblClient.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5");
                tblClient.Address = register.Address;
                tblClient.Email = register.Email;
                heart.SaveChanges();
                return Redirect("Index?recovery=true");
            }
            catch
            {
                return View(register);
            }

        }


        public ActionResult Cart()
        {
            try
            {
                int id = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (id == 3)
                {
                    throw new Exception("Admin access only");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                List<OcProduct> list = new List<OcProduct>();
                if (Session["ShopCart"] != null)
                {
                    List<ShopCartItem> cart = Session["ShopCart"] as List<ShopCartItem>;
                    foreach (var ShopCartItem in cart)
                    {
                        var product = heart.TblProducts.SingleOrDefault(i => i.id == ShopCartItem.ProductID);
                        TblCatagory c = heart.TblCatagories.SingleOrDefault(i => i.id == product.CatagoryId);
                        List<TblImage> imagesForServeice = new List<TblImage>();
                        List<TblProductImageRel> relsForImages = heart.TblProductImageRels.Where(i => i.ProductId == product.id).ToList();
                        foreach (var j in relsForImages)
                            imagesForServeice.Add(heart.TblImages.SingleOrDefault(i => i.id == j.ImageId));

                        list.Add(new OcProduct()
                        {
                            id = ShopCartItem.ProductID,
                            Count = ShopCartItem.Count,
                            Price = product.Price,
                            Name = product.Name,
                            Sum = ShopCartItem.Count * product.Price,
                            Catagory = c,
                            images = imagesForServeice
                        });
                    }
                }
                return View(list);
            }
            catch (Exception e)
            {
                throw e;
                //return HttpNotFound();
            }

        }
        public ActionResult Checkout()
        {

            try
            {
                int id = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (id == 3)
                {
                    throw new Exception("Admin access only");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                List<OcProduct> list = new List<OcProduct>();
                if (Session["ShopCart"] != null)
                {
                    List<ShopCartItem> cart = Session["ShopCart"] as List<ShopCartItem>;
                    foreach (var ShopCartItem in cart)
                    {
                        var product = heart.TblProducts.SingleOrDefault(i => i.id == ShopCartItem.ProductID);
                        TblCatagory c = heart.TblCatagories.SingleOrDefault(i => i.id == product.CatagoryId);
                        List<TblImage> imagesForServeice = new List<TblImage>();
                        List<TblProductImageRel> relsForImages = heart.TblProductImageRels.Where(i => i.ProductId == product.id).ToList();
                        foreach (var j in relsForImages)
                            imagesForServeice.Add(heart.TblImages.SingleOrDefault(i => i.id == j.ImageId));

                        list.Add(new OcProduct()
                        {
                            id = ShopCartItem.ProductID,
                            Count = ShopCartItem.Count,
                            Price = product.Price,
                            Name = product.Name,
                            Sum = ShopCartItem.Count * product.Price,
                            Catagory = c,
                            images = imagesForServeice
                        });
                    }
                }
                return View(list);
            }
            catch (Exception e)
            {
                throw e;
                //return HttpNotFound();

            }
        }

        public ActionResult History()
        {
            try
            {
                int id = Convert.ToInt32(User.Identity.Name.Split('|')[1]);
                if (id == 3)
                {
                    throw new Exception("Admin access only");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                List<TblClientProductRel> rels = heart.TblClientProductRels.Where(i => i.ClientId == id).DistinctBy(i => i.FactorId).OrderByDescending(i => i.id).ToList();
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
                if (idLogin == 3)
                {
                    throw new Exception("Admin access only");
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                int DarsadDiscountId = 0;
                List<TblClientProductRel> rels = heart.TblClientProductRels.Where(i => i.IsFinaly && i.ClientId == idLogin && i.FactorId == id).ToList();
                List<ShowFactorViewModels> list = new List<ShowFactorViewModels>();
                if (rels[0].DiscountId > 0 )
                {
                    TblDiscount discount= heart.TblDiscounts.Find(rels[0].DiscountId);
                    if (discount != null)
                    {
                        DarsadDiscountId = discount.Discount;
                    }
                    else
                    {
                        DarsadDiscountId = -1;
                    }
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
                return View("Index");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult Order()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Order(TblOrder order)
        {
            try
            {
                TblOrder addOrder = heart.TblOrders.Add(order);
                heart.SaveChanges();
                return Json(new { success = true, responseText = "سفارش ثبت شد به زودی با شما تماس حاصل میشود" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new { success = false, responseText = "خطا در ثبت سفارش لطفا دقت فرمایید" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CheckDiscount(string name)
        {
            List<TblDiscount> discout = heart.TblDiscounts.Where(i => i.Name == name && i.Count != 0).ToList();
            if (discout.Count > 0)
            {
                return Json(new { success = true, responseText = discout[0].Discount, responseId = discout[0].id }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, responseText = "کد تخفیف موجود نیست" }, JsonRequestBehavior.AllowGet);
        }
    }
}