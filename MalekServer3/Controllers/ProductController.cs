using MalekServer3.Models;
using MalekServer3.Models.ObjectClass;
using MalekServer3.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MalekServer3.Controllers
{
    public class ProductController : Controller
    {
        Heart heart;
        public ProductController()
        {
            heart = new Heart();
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return PartialView();
        }
        private List<(int productId, int count)> SortProductsBySoldCount()
        {
            List<(int productId, int count)> result = new List<(int, int)>();
            List<TblClientProductRel> rels = heart.TblClientProductRels.ToList();
            int count = 0;
            for (int i = 0; i < rels.Count; i++)
            {
                TblClientProductRel rel = rels[i];

                for (int j = 1; j < rels.Count; j++)
                    if (rel.ProductId == rels[j].ProductId)
                    {
                        count++;
                    }
                result.Add((rel.ProductId, count));
                count = 0;
            }
            result = result.OrderBy(i => i.count).ToList();
            return result;
        }
        public ActionResult ListProduct(int? id)
        {
            List<TblProduct> products = new List<TblProduct>();
            if (id == null)
            {
                products = AllProducts().OrderByDescending(i => i.IsSuggested).Take(4).ToList();
            }
            else if (id == 1)
            {
                products = AllProducts().OrderByDescending(i => i.id).Take(4).ToList();
            }
            else if (id == 2)
            {
                List<TblClientProductRel> tblClientProduct = heart.TblClientProductRels.ToList();
                //products = heart.TblProducts.Where(i => i.id==).Take(4).ToList();
                foreach (var item in tblClientProduct)
                {
                    List<TblProduct> tblProduct = AllProducts().Where(i => i.id == item.ProductId).ToList();
                    products.AddRange(AllProducts().Where(i => i.id == item.ProductId));
                }
                //List<TblClientProductRel> clientProductRels = heart.TblClientProductRels.OrderByDescending(i=>i.Count).ToList();
                // SortProductsBySoldCount().ForEach(i => products.Add(heart.TblProducts.FirstOrDefault(j => j.id == i.productId)));
                products = products.DistinctBy(i => i.id).Take(4).ToList();
            }
            List<OcProduct> newProducts = new List<OcProduct>();
            foreach (TblProduct i in products)
            {
                newProducts.Add(new OcProduct(i));
            }
            return PartialView(newProducts);
        }
        public ActionResult NewProduct()
        {
            List<TblProduct> products = heart.TblProducts.Take(10).ToList();
            List<OcProduct> newProducts = new List<OcProduct>();
            foreach (TblProduct i in products)
            {
                newProducts.Add(new OcProduct(i));
            }
            return PartialView(newProducts);
        }
        [Route("ViewProduct/{id}/{name}")]
        public ActionResult ViewProduct(int id, string name)
        {
            try
            {
                ViewBag.name = name;
                var Product = new OcProduct(heart.TblProducts.Find(id));
                if (Product == null)
                {
                    return HttpNotFound("SelectProductById  has not worked");
                }
                //return View("/Product?Name=" + Product.Name);
                return View(Product);

            }
            catch
            {
                return HttpNotFound("SelectProductById has not worked");
            }
        }
        public ActionResult RelatedProduct(int id)
        {
            TblProduct selectProduct = heart.TblProducts.Find(id);
            List<TblProduct> products = new List<TblProduct>();
            products.AddRange(AllProducts().Where(i => i.Name.Contains(selectProduct.Name) || i.DescriptionHtml.Contains(selectProduct.Name)).ToList());
            products.AddRange(AllProducts().Where(i => i.CatagoryId == selectProduct.CatagoryId).ToList());
            //List<TblKeyword> sele = heart.TblKeywords.Where(i => i.Name.Contains(selectProduct.Name)).ToList();
            //foreach (TblKeyword i in sele)
            //{
            //    List<TblProductKeywordRel> a = heart.TblProductKeywordRels.Where(j => j.KeywordId == i.id).ToList();
            //    a.ForEach(j => products.Add(heart.TblProducts.FirstOrDefault(k => k.id == j.ProductId)));
            //}
            List<OcProduct> newProducts = new List<OcProduct>();
            foreach (TblProduct i in products)
            {
                newProducts.Add(new OcProduct(i));
            }
            return PartialView(newProducts);
        }
        List<TblProduct> AllProducts()
        {
            List<TblProduct> list = new List<TblProduct>();
            list.AddRange(heart.TblProducts.ToList());
            return list;
        }
        [Route("Products/{id?}/{name?}")]
        public ActionResult Products(int? id, string name)
        {
            List<TblProduct> list = new List<TblProduct>();
            if (id != null && id > 0)
            {
                ViewBag.Name = name;
                list.AddRange(AllProducts().Where(i => i.CatagoryId == id));
            }
            else if (id != null && id == -1)
            {
                ViewBag.Name = "محصولات جدید";
                list.AddRange(AllProducts().OrderByDescending(i=>i.id).ToList());
            }
            else if (id != null && id == -2)
            {
                ViewBag.Name = "محصولات پیشنهادی";
                list.AddRange(AllProducts().Where(i=>i.IsSuggested).ToList());
            }
            else if (id != null && id == -3)
            {
                ViewBag.Name = " بیشترین فروش ها";
                list.AddRange(AllProducts().Where(i=>i.IsSuggested).ToList());
                List<TblClientProductRel> tblClientProduct = heart.TblClientProductRels.ToList();
                foreach (var item in tblClientProduct)
                {
                    List<TblProduct> tblProduct = AllProducts().Where(i => i.id == item.ProductId).ToList();
                    list.AddRange(AllProducts().Where(i => i.id == item.ProductId));
                }
                list = list.DistinctBy(i => i.id).ToList();
            }
            else if (id != null && id == -4)
            {
                ViewBag.Name = " محبوب ترین لوازم جانبی";
                list.AddRange(AllProducts().OrderByDescending(i => i.Raiting).ToList());
            }
            else if (id != null && id == -5)
            {
                ViewBag.Name = "محبوب ترین جدیدها ";
                list.AddRange(AllProducts().OrderByDescending(i => i.id).OrderByDescending(i => i.Raiting).ToList());
            }
            else
            {
                ViewBag.Name = "همه محصولات ملک سرور";
                list.AddRange(AllProducts());
            }
            List<OcProduct> newProducts = new List<OcProduct>();
            foreach (TblProduct i in list)
            {
                newProducts.Add(new OcProduct(i));
            }
            return View(newProducts.OrderByDescending(i => i.DateSubmited));
        }


        public ActionResult IsOnFirstPage()
        {
            try
            {
                List<OcCatagory> cats = new List<OcCatagory>();
                List<TblCatagory> AllCatagory = heart.TblCatagories.Where(i => i.IsOnFirstPage).ToList();
                foreach (TblCatagory i in AllCatagory)
                {
                    cats.Add(new OcCatagory(i.id, i.Name, i.CatagoryId, new List<OcProduct>()));
                    for (int k = 0; k < cats.Count; k++)
                    {
                        int ctId = cats[k].id;
                        List<TblProduct> prs = AllProducts().Where(j => j.CatagoryId == ctId).ToList();
                        foreach (TblProduct item in prs)
                        {
                            cats[k].Products.Add(new OcProduct(item));
                        }
                    }
                }

                return PartialView(cats);
            }
            catch (Exception e)
            {
                return HttpNotFound("SelectButtonProducts has not worked");
            }
        }


        public ActionResult MostPriceProducts()
        {
            List<TblProduct> products = new List<TblProduct>();
            List<TblClientProductRel> tblClientProduct = heart.TblClientProductRels.ToList();
            foreach (var item in tblClientProduct)
            {
                List<TblProduct> tblProduct = AllProducts().Where(i => i.id == item.ProductId).ToList();
                products.AddRange(AllProducts().Where(i => i.id == item.ProductId));
            }
            products = products.DistinctBy(i => i.id).Take(4).ToList();
            List<OcProduct> newProducts = new List<OcProduct>();
            foreach (TblProduct i in products)
            {
                newProducts.Add(new OcProduct(i));
            }
            return PartialView(newProducts);
        }

        public ActionResult LikeProducts()
        {
            List<TblProduct> products = new List<TblProduct>();
            products = AllProducts().OrderByDescending(i => i.Raiting).Take(4).ToList();
            List<OcProduct> newProducts = new List<OcProduct>();
            foreach (TblProduct i in products)
            {
                newProducts.Add(new OcProduct(i));
            }
            return PartialView(newProducts);
        }
        public ActionResult NewsProducts()
        {
            List<TblProduct> products = new List<TblProduct>();
            products = AllProducts().OrderByDescending(i => i.id).OrderByDescending(i => i.Raiting).Take(4).ToList();
            List<OcProduct> newProducts = new List<OcProduct>();
            foreach (TblProduct i in products)
            {
                newProducts.Add(new OcProduct(i));
            }
            return PartialView(newProducts);
        }
        
    }
}