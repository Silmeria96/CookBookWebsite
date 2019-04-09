using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CookBookWebsite.Models
{
    public class CookBookDbContext : DbContext
    {
        public CookBookDbContext() : base("CookBookDB") { }

        public DbSet<User> Users { get; set; }

        public DbSet<CookBook> CookBooks { get; set; }

        public DbSet<CookBookMaterial> CookBookMaterials { get; set; }

        public DbSet<CookBookStep> CookBookSteps { get; set; }

        public DbSet<CookBookOrder> CookBookOrders { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<CookBookComment> CookBookComments { get; set; }
    }
}