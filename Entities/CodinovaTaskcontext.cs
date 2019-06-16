using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodinovaTask.Model;
using CodinovaTask.Repository.Model;
using Microsoft.EntityFrameworkCore;

namespace CodinovaTask.Entities
{
    public class CodinovaTaskcontext : DbContext
    {
        public CodinovaTaskcontext(DbContextOptions options)
         : base(options)
        {

        }

        public DbSet<EmployeeTesting> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; } 
        public DbSet<SaveOrder> SaveOrders { get; set; }
    }
}
