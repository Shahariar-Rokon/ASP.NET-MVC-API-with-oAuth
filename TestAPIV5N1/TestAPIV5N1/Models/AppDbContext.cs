using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestAPIV5N1.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext():base("AppDbContext")
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Service>Services { get; set; }
        public DbSet<ReservedItem> ReservedItems { get; set; }
    }
}