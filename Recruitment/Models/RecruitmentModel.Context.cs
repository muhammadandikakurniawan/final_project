﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Recruitment.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RecruitmentEntities : DbContext
    {
        public RecruitmentEntities()
            : base("name=RecruitmentEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CANDIDATE> CANDIDATEs { get; set; }
        public virtual DbSet<EDUCATION> EDUCATIONs { get; set; }
        public virtual DbSet<EXPERIENCE> EXPERIENCEs { get; set; }
        public virtual DbSet<MENU> MENUs { get; set; }
        public virtual DbSet<POSITION> POSITIONs { get; set; }
        public virtual DbSet<ROLE> ROLEs { get; set; }
        public virtual DbSet<SELECTION_HISTORY> SELECTION_HISTORY { get; set; }
        public virtual DbSet<SKILL> SKILLs { get; set; }
        public virtual DbSet<SOURCE> SOURCEs { get; set; }
        public virtual DbSet<STATE> STATEs { get; set; }
        public virtual DbSet<USER> USERs { get; set; }
    }
}
