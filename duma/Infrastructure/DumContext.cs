using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using duma.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using duma.Models;

namespace duma.Infrastructure
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class DumContext : IdentityDbContext<ApplicationUser>//DbContext//IdentityDbContext<ApplicationUser>// >>> IdentityUser
    {
        //контекста данных
        public DumContext() : base("DumContext") { }
        public DbSet<Duma> Dumas { get; set;}
        public DbSet<TotalPrice> TotalPrices { get; set; }
        public DbSet<Parameter> Parameters { get; set; }
        public static DumContext Create()
        {
            return new DumContext();
        }
        //--------------------------------------------
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new EntityTypeConfiguration<Duma>());
            //modelBuilder.Entity<Parameter>().Ignore(x => x.codparameter);
            base.OnModelCreating(modelBuilder);
        }
        //--------------------------------------------
    }
}