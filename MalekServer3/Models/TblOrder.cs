//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MalekServer3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblOrder
    {
        public int id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int ClientId { get; set; }
    
        public virtual TblClient TblClient { get; set; }
    }
}