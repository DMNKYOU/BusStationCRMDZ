
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace BusStationCRM.DAL.Contexts
{
    public class ApplicationDbContext : IdentityDbContext
    {
        

        public ApplicationDbContext(): base() 
        {
            //Database.EnsureDeleted();   
            //Database.EnsureCreated();
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)  { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Seed();
        }
    }
}
