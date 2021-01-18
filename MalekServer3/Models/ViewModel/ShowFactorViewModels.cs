using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MalekServer3.Models.ViewModel
{
    public class ShowFactorViewModels
    {
        public int id { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public string Date { get; set; }
        public int Count { get; set; }
        public bool IsFinaly { get; set; }
        public string Name { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientTellNo { get; set; }
        public string ClientEmail { get; set; }
        public string CatagoryName { get; set; }
        public long Price { get; set; }
        public int FactorId { get; set; }
        public int Discount { get; set; }
        public long SumFactor { get; set; }
        public long Sum { get; set; }
        public int DiscountId { get; set; }
        public int DarsadDiscountId { get; set; }
    }
}