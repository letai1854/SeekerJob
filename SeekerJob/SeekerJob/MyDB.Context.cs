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
    
    public partial class MYSQL : DbContext
    {
        public MYSQL()
            : base("name=MYSQL")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Advertise> Advertises { get; set; }
        public virtual DbSet<CV> CVs { get; set; }
        public virtual DbSet<InforCandidate> InforCandidates { get; set; }
        public virtual DbSet<InforEmployer> InforEmployers { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<ListCandidate> ListCandidates { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<PageTitle> PageTitles { get; set; }
        public virtual DbSet<SaveJob> SaveJobs { get; set; }
        public virtual DbSet<tablebanner> tablebanners { get; set; }
        public virtual DbSet<tablebannerpart> tablebannerparts { get; set; }
        public virtual DbSet<tablefooter> tablefooters { get; set; }
        public virtual DbSet<tablefooterpart> tablefooterparts { get; set; }
        public virtual DbSet<tableimagemenu> tableimagemenus { get; set; }
        public virtual DbSet<tablemenu> tablemenus { get; set; }
        public virtual DbSet<tablemenufunction> tablemenufunctions { get; set; }
        public virtual DbSet<tablemenupart> tablemenuparts { get; set; }
        public virtual DbSet<TitlePage> TitlePages { get; set; }
    }
}
