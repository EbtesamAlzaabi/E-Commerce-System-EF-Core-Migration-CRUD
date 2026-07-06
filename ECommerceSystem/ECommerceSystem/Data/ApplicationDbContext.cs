using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using ECommerceSystem.Models;

namespace ECommerceSystem.Data
{
    public class ApplicationDbContext : DbContext 
        //DbContext، وهو حلقة الوصل بين مشروع C# وقاعدة البيانات في SQL Server.
    {
        public DbSet<Category> Categories { get; set; } //جدول Categories في قاعدة البيانات.
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //هذه الدالة مسؤولة عن إعداد الاتصال بقاعدة البيانات.
        {

            optionsBuilder.UseSqlServer(
               @"Server=EBTESAM;Database=ECommerceDB;Trusted_Connection=True;TrustServerCertificate=True;");

        }
    }
}