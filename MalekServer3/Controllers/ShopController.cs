using MalekServer3.Models;
using MalekServer3.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MalekServer3.Controllers
{
    public class ShopController : ApiController
    {
        Heart heart = new Heart();
        // GET: api/Shop
        public int Get()
        {
            List<ShopCartItem> list = new List<ShopCartItem>();
            var sessions = HttpContext.Current.Session;
            if (sessions["ShopCart"] != null)
            {
                list = sessions["ShopCart"] as List<ShopCartItem>;
            }
            return list.Sum(l => l.Count);
        }

        // GET: api/Shop/5
        public int Get(int id)
        {
            List<ShopCartItem> list = new List<ShopCartItem>();
            var sessions = HttpContext.Current.Session;
            if (sessions["ShopCart"] != null)
            {
                list = sessions["ShopCart"] as List<ShopCartItem>;
            }
            if (list.Any(p => p.ProductID == id))
            {
                int index = list.FindIndex(p => p.ProductID == id);
                list[index].Count += 1;
                TblProduct tblProduct = heart.TblProducts.Find(id);
                if(tblProduct.Count< list[index].Count)
                {
                    list[index].Count = tblProduct.Count;
                }
            }
            else
            {
                list.Add(new ShopCartItem()
                {
                    ProductID = id,
                    Count = 1
                });
            }
            sessions["ShopCart"] = list;
            return Get();
        }
    }
}
