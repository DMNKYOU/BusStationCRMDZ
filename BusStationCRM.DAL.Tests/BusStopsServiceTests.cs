using System;
using System.Threading.Tasks;
using BusStationCRM.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using BusStationCRM.BLL.Enums;
using BusStationCRM.BLL.Models;
using BusStationCRM.BLL.Models.Search;
using BusStationCRM.BLL.Services;
using BusStationCRM.DAL.Repositories;

namespace BusStationCRM.BLL.Tests {

    public class BusStopsServiceTests
    {
        public DbContextOptions<ApplicationDbContext> Opt;

        [SetUp] //как вариант можно еще замокать Moq 
        public void SetUp()
        {
            Opt = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var context = new ApplicationDbContext(Opt);

            // Remove seed data
            context.RemoveRange(context.BusStops);
            context.SaveChanges();

            context.BusStops.AddRange(GetTestBusStops());
            context.SaveChanges();
        }

        [Test]
        public async Task TestEmptyNameContains()
        {
            await using var context = new ApplicationDbContext(Opt);
            BusStopsService busStopsService = new BusStopsService(new BusStopsRepository(context));

            var result = busStopsService.Search("OLP").Result.Count();

            Assert.AreEqual(0, result);
        }

        [Test]
        public async Task TesNotEmptyNameContains()
        {
            await using var context = new ApplicationDbContext(Opt);
            BusStopsService busStopsService = new BusStopsService(new BusStopsRepository(context));

            var result = busStopsService.Search("BusStop").Result.Count();

            Assert.AreEqual(3, result);
        }

        public IEnumerable<BusStop> GetTestBusStops()
        {
            List<BusStop> defaultBusStops = new List<BusStop>()
            {
                new BusStop() { Id = 1, Name = "A 1", Description = "A 1", Type = TypeTransport.Express },
                new BusStop() { Id = 2, Name = "BusStop 2", Description = "BusStop 2", Type = TypeTransport.All },
                new BusStop() { Id = 3, Name = "AB 1", Description = "BB 3", Type = TypeTransport.All },
                new BusStop() { Id = 4, Name = "BusStop 4", Description = "BusStop 4", Type = TypeTransport.Express },
                new BusStop() { Id = 5, Name = "BusStop 5", Description = "BusStop 5", Type = TypeTransport.All },
            };

            return defaultBusStops;
        }

    }
}