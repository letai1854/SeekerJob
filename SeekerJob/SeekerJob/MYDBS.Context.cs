﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SeekerJob
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MYDBS : DbContext
    {
        public MYDBS()
            : base("name=MYDBS")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Advertise> Advertises { get; set; }
        public virtual DbSet<CV> CVs { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<ListCandidate> ListCandidates { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<SaveJob> SaveJobs { get; set; }
        public virtual DbSet<tableimagemenu> tableimagemenus { get; set; }
        public virtual DbSet<tablemenu> tablemenus { get; set; }
        public virtual DbSet<tablemenufunction> tablemenufunctions { get; set; }
        public virtual DbSet<tablemenupart> tablemenuparts { get; set; }
    }
}
