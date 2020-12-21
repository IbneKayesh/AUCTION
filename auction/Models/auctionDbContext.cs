using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace auction.Models
{
    public class auctionDbContext : DbContext
    {
        public auctionDbContext() : base("name=auctionconstr")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("KAYESH");
        }
        public virtual DbSet<T_ORDR> T_ORDR { get; set; }
        public virtual DbSet<T_ORTP> T_ORTP { get; set; }
        public virtual DbSet<T_ORMC> T_ORMC { get; set; }
        public virtual DbSet<T_MCTP> T_MCTP { get; set; }
        public virtual DbSet<T_AMSP> T_AMSP { get; set; }
        public virtual DbSet<T_AMIM> T_AMIM { get; set; }
        public virtual DbSet<T_MOLD> T_MOLD { get; set; }
        public virtual DbSet<T_AMSU> T_AMSU { get; set; }
        public virtual DbSet<T_ORAM> T_ORAM { get; set; }
        public virtual DbSet<T_MCLT> T_MCLT { get; set; }
        public virtual DbSet<T_USER> T_USER { get; set; }
        public virtual DbSet<T_USMC> T_USMC { get; set; }
        public virtual DbSet<T_WCRT> T_WCRT { get; set; }
        public virtual DbSet<T_SMRY> T_SMRY { get; set; }
        public virtual DbSet<T_MAIL> T_MAIL { get; set; }
        public virtual DbSet<T_EMAL> T_EMAL { get; set; }
        public virtual DbSet<T_CLSM> T_ORBL { get; set; }
        public virtual DbSet<T_PSMA> T_PSMA { get; set; }
        public virtual DbSet<T_CLST> T_CLST { get; set; }
        public virtual DbSet<T_CLSD> T_CLSD { get; set; }
        

    }
}