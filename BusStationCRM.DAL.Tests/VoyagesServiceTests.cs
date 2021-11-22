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

namespace BusStationCRM.DAL.EF.Tests {

    public class AdvancedSearchTests
    {
        public DbContextOptions<ApplicationDbContext> Opt;

        [SetUp] //как вариант можно еще замокать Moq 
        public void SetUp()
        {
            Opt = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            using var context = new ApplicationDbContext(Opt);
            // Remove seed data
            context.RemoveRange(context.Voyages);
            context.SaveChanges(); 
            
            context.RemoveRange(context.BusStops);
            context.SaveChanges();

            context.BusStops.AddRange(GetTestBusStops());
            context.Voyages.AddRange(GetTestVoyages());
            context.SaveChanges();
        }

        [Test]
        public async Task TestEmptyNameContains()
        {
            await using var context = new ApplicationDbContext(Opt);
            var repository = new VoyagesRepository(context);
            VoyagesService voyagesService = new VoyagesService(repository);

            var result = voyagesService.Filter(new VoyageFilter() { NameContains = "Offers" }).Result.Count();

            Assert.AreEqual(0, result);
        }

        [Test]
        public async Task TesNotEmptyNameContains()
        {
            await using var context = new ApplicationDbContext(Opt);
            var repository = new VoyagesRepository(context);
            VoyagesService voyagesService = new VoyagesService(repository);

            var result = voyagesService.Filter(new VoyageFilter() { NameContains = "Name" }).Result.Count();

            Assert.AreEqual(2, result);
        }

        [Test]
        public async Task TestEmptyCost()
        {
            await using var context = new ApplicationDbContext(Opt);
            var repository = new VoyagesRepository(context);
            VoyagesService voyagesService = new VoyagesService(repository);

            var result = voyagesService.Filter(new VoyageFilter() { PriceFrom = 119 }).Result.Count();

            Assert.AreEqual(0, result);
        }

        [Test]
        public async Task TestCostAndPrice()
        {
            await using var context = new ApplicationDbContext(Opt);
            var repository = new VoyagesRepository(context);
            VoyagesService voyagesService = new VoyagesService(repository);

            var result = voyagesService.Filter(new VoyageFilter() { PriceFrom = 10, ArrivalStopNameContains = "BusStop" }).Result.Count();

            Assert.AreEqual(1, result);
        }

        [Test]
        public async Task TestRangeCost()
        {
            await using var context = new ApplicationDbContext(Opt);
            var repository = new VoyagesRepository(context);
            VoyagesService voyagesService = new VoyagesService(repository);

            var result = voyagesService.Filter(new VoyageFilter() { PriceFrom = 1, PriceTo = 8 }).Result.Count();

            Assert.AreEqual(2, result);
        }

        [Test]
        public async Task TestRangeCostAll()
        {
            await using var context = new ApplicationDbContext(Opt);
            var repository = new VoyagesRepository(context);
            VoyagesService voyagesService = new VoyagesService(repository);

            var result = voyagesService.Filter(new VoyageFilter() { PriceFrom = 1, PriceTo = 20 }).Result.Count();

            Assert.AreEqual(4, result);
        }

        [Test]
        public async Task TestRangeDateWithPriceDeparture()
        {
            await using var context = new ApplicationDbContext(Opt);
            var repository = new VoyagesRepository(context);
            VoyagesService voyagesService = new VoyagesService(repository);

            var result = voyagesService.Filter(new VoyageFilter() { DepartureFrom = new DateTime().AddDays(+3), PriceFrom = 1, PriceTo = 8 }).Result.Count();

            Assert.AreEqual(0, result);
        }
        
        [Test]
        public async Task TestRangeDateWithPriceArrival()
        {
            await using var context = new ApplicationDbContext(Opt);
            var repository = new VoyagesRepository(context);
            VoyagesService voyagesService = new VoyagesService(repository);

            var result = voyagesService.Filter(new VoyageFilter() { ArrivalTo = new DateTime().AddDays(+3).AddHours(17), PriceFrom = 1}).Result.Count();

            Assert.AreEqual(2, result);
        }

        [Test]
        public async Task TestSearchEmptyResult()
        {
            await using var context = new ApplicationDbContext(Opt);
            var repository = new VoyagesRepository(context);
            VoyagesService voyagesService = new VoyagesService(repository);

            var result = voyagesService.Search("Namm").Result.Count();

            Assert.AreEqual(0, result);
        }

        [Test]
        public async Task TestSearchNotEmptyResult()
        {
            await using var context = new ApplicationDbContext(Opt);
            var repository = new VoyagesRepository(context);
            VoyagesService voyagesService = new VoyagesService(repository);

            var result = voyagesService.Search("Name").Result.Count();

            Assert.AreEqual(2, result);
        }

        [Test]
        public async Task TestSearchOnBusStopsNamesNotEmptyResult()
        {
            await using var context = new ApplicationDbContext(Opt);
            var repository = new VoyagesRepository(context);
            VoyagesService voyagesService = new VoyagesService(repository);

            var result = voyagesService.Search("BusStop").Result.Count();

            Assert.AreEqual(4, result);
        }
        
        [Test]
        public async Task TestSearchInNamesVoyagesNotEmptyResult()
        {
            await using var context = new ApplicationDbContext(Opt);
            var repository = new VoyagesRepository(context);
            VoyagesService voyagesService = new VoyagesService(repository);

            var result = voyagesService.Search("Number").Result.Count();

            Assert.AreEqual(2, result);
        }

        public IEnumerable<Voyage> GetTestVoyages()
        {
            List<Voyage> defaultVoyages = new List<Voyage>()
            {
                new Voyage()
                {
                    Id = 4,
                    Number = 16,
                    Name = "Name 4",
                    DepartureInfo = new DateTime().AddDays(+3).AddHours(15),
                    ArrivalInfo =  new DateTime().AddDays(+3).AddHours(19),
                    TravelTime = new DateTime().AddHours(4),
                    NumberSeats = 13,
                    TicketCost = 10,
                    BusStopArrivalId = 1,
                    BusStopDepartureId = 2,
                    Type = TypeTransport.All
                },
                new Voyage()
                {
                    Id = 5,
                    Number = 11,
                    Name = "Name 11",
                    DepartureInfo = new DateTime().AddDays(+2).AddHours(15),
                    ArrivalInfo =  new DateTime().AddDays(+3).AddHours(17),
                    TravelTime = new DateTime().AddDays(1).AddHours(2),
                    NumberSeats = 13,
                    TicketCost = 8,
                    BusStopArrivalId = 2,
                    BusStopDepartureId = 2,
                    Type = TypeTransport.All
                },
                new Voyage()
                {
                    Id = 7,
                    Number = 8,
                    Name = "Number 8",
                    DepartureInfo = new DateTime().AddDays(+2).AddHours(15),
                    ArrivalInfo =  new DateTime().AddDays(+3).AddHours(16),
                    TravelTime = new DateTime().AddDays(1).AddHours(1),
                    NumberSeats = 8,
                    TicketCost = 5,
                    BusStopArrivalId = 3,
                    BusStopDepartureId = 2,
                    Type = TypeTransport.All
                },
                new Voyage()
                {
                    Id = 8,
                    Number = 9,
                    Name = "9Number",
                    DepartureInfo = new DateTime().AddDays(+2).AddHours(15),
                    ArrivalInfo =  new DateTime().AddDays(+5).AddHours(19),
                    TravelTime = new DateTime().AddDays(3).AddHours(4),
                    NumberSeats = 80,
                    TicketCost = 15,
                    BusStopArrivalId = 4,
                    BusStopDepartureId = 5,
                    Type = TypeTransport.Express
                }
            };

            return defaultVoyages;
        }

        public IEnumerable<BusStop> GetTestBusStops()
        {
            List<BusStop> defaultBusStops = new List<BusStop>()
            {
                new BusStop() { Id = 1, Name = "A 1", Description = "A 1", Type = TypeTransport.Express },
                new BusStop() { Id = 2, Name = "BusStop 2", Description = "BusStop 2", Type = TypeTransport.All },
                new BusStop() { Id = 3, Name = "BB 3", Description = "BB 3", Type = TypeTransport.All },
                new BusStop() { Id = 4, Name = "BusStop 4", Description = "BusStop 4", Type = TypeTransport.Express },
                new BusStop() { Id = 5, Name = "BusStop 5", Description = "BusStop 5", Type = TypeTransport.All },
            };

            return defaultBusStops;
        }

    }
}