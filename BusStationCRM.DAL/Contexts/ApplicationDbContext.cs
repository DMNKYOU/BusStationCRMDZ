using BusStationCRM.BLL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BusStationCRM.BLL.Enums;

namespace BusStationCRM.DAL.Contexts
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<BusStop> BusStops { get; set; }// = default!;
        public DbSet<Voyage> Voyages { get; set; }// = default!;
        public DbSet<Order> Orders { get; set; }// = default!;
        public DbSet<User> Users { get; set; }// = default!;
        public DbSet<Ticket> Tickets { get; set; }// = default!;

        public ApplicationDbContext(): base() 
        {
            //Database.EnsureDeleted();   
            //Database.EnsureCreated();
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)  { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Voyage>()
                .HasOne(b => b.BusStopArrival)
                .WithMany(a => a.ArrivalVoyages)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Voyage>()
                .HasOne(b => b.BusStopDeparture)
                .WithMany(a => a.DepartureVoyages)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Seed();
        }
    }
}
