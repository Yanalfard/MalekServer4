﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Heart : DbContext
    {
        public Heart()
            : base("name=Heart")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TblAd> TblAds { get; set; }
        public virtual DbSet<TblBlog> TblBlogs { get; set; }
        public virtual DbSet<TblBlogCommentRel> TblBlogCommentRels { get; set; }
        public virtual DbSet<TblBlogKeywordRel> TblBlogKeywordRels { get; set; }
        public virtual DbSet<TblCatagory> TblCatagories { get; set; }
        public virtual DbSet<TblClient> TblClients { get; set; }
        public virtual DbSet<TblClientProductRel> TblClientProductRels { get; set; }
        public virtual DbSet<TblComment> TblComments { get; set; }
        public virtual DbSet<TblDiscount> TblDiscounts { get; set; }
        public virtual DbSet<TblImage> TblImages { get; set; }
        public virtual DbSet<TblKeyword> TblKeywords { get; set; }
        public virtual DbSet<TblOrder> TblOrders { get; set; }
        public virtual DbSet<TblProduct> TblProducts { get; set; }
        public virtual DbSet<TblProductCommentRel> TblProductCommentRels { get; set; }
        public virtual DbSet<TblProductImageRel> TblProductImageRels { get; set; }
        public virtual DbSet<TblProductKeywordRel> TblProductKeywordRels { get; set; }
        public virtual DbSet<TblProductPropertyRel> TblProductPropertyRels { get; set; }
        public virtual DbSet<TblProperty> TblProperties { get; set; }
    }
}
