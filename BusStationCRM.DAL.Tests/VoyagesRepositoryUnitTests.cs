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
                    ArrivalInfo =  new DateTime().AddDays(+3).AddHours(19),
                    TravelTime = new DateTime().AddDays(1).AddHours(4),
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
                    Name = "Name 8",
                    DepartureInfo = new DateTime().AddDays(+2).AddHours(15),
                    ArrivalInfo =  new DateTime().AddDays(+3).AddHours(19),
                    TravelTime = new DateTime().AddDays(1).AddHours(4),
                    NumberSeats = 8,
                    TicketCost = 5,
                    BusStopArrivalId = 3,
                    BusStopDepartureId = 2,
                    Type = TypeTransport.All
                }
            };

            return defaultVoyages;
        }

        public IEnumerable<BusStop> GetTestBusStops()
        {
            List<BusStop> defaultBusStops = new List<BusStop>()
            {
                new BusStop() { Id = 1, Description = "A 1", Type = TypeTransport.Express },
                new BusStop() { Id = 2, Description = "BusStop 2", Type = TypeTransport.All },
                new BusStop() { Id = 3, Description = "BB 3", Type = TypeTransport.All },
                new BusStop() { Id = 4, Description = "BC 4", Type = TypeTransport.Express },
                new BusStop() { Id = 5, Description = "BusStop 5", Type = TypeTransport.All },
            };

            return defaultBusStops;
        }

        [SetUp]
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
        public async Task TestEmptyCost()
        {
            await using var context = new ApplicationDbContext(Opt);
            var repository = new VoyagesRepository(context);
            VoyagesService voyagesService = new VoyagesService(repository);

            var result = voyagesService.Filter(new VoyageFilter() { PriceFrom = 119 }).Result.Count();

            Assert.AreEqual(0, result);
        }

        //    [Test]
        //    public async Task TestSubTitle()
        //    {
        //        await using var context = new ApplicationDbContext(Opt);
        //        var repository = new UnitOfWork(context);
        //        OfferService offerService = new OfferService(repository);
        //        Assert.AreEqual(3, offerService.AdvancedSearch(subtitle: "Offer").Count());
        //    }

        //    [Test]
        //    public async Task TestCost()
        //    {
        //        await using var context = new ApplicationDbContext(Opt);
        //        var repository = new UnitOfWork(context);
        //        OfferService offerService = new OfferService(repository);
        //        Assert.AreEqual(2, offerService.AdvancedSearch(minCost: 40).Count());
        //    }

        //    [Test]
        //    public async Task TestCostAndSubTitle()
        //    {
        //        await using var context = new ApplicationDbContext(Opt);
        //        var repository = new UnitOfWork(context);
        //        OfferService offerService = new OfferService(repository);
        //        Assert.AreEqual(1, offerService.AdvancedSearch(subtitle: "Offer", minCost: 40).Count());
        //    }

        [Test]
        public async Task TestRangeCost()
        {
            await using var context = new ApplicationDbContext(Opt);
            var repository = new VoyagesRepository(context);
            VoyagesService voyagesService = new VoyagesService(repository);

            var result = voyagesService.Filter(new VoyageFilter() { PriceFrom = 1, PriceTo = 8 }).Result.Count();

            Assert.AreEqual(2, result);
        }

        //    [Test]
        //    public async Task TestRangeCostAll()
        //    {
        //        await using var context = new ApplicationDbContext(Opt);
        //        var repository = new UnitOfWork(context);
        //        OfferService offerService = new OfferService(repository);
        //        Assert.AreEqual(5, offerService.AdvancedSearch(minCost: 5, maxCost: 50).Count());
        //    }

        //    [Test]
        //    public async Task TestMinMoreMaxException()
        //    {
        //        await using var context = new ApplicationDbContext(Opt);
        //        var repository = new UnitOfWork(context);
        //        OfferService offerService = new OfferService(repository);
        //        Assert.Throws<ArgumentException>(() => offerService.AdvancedSearch(minCost: 15, maxCost: 3));
        //    }

        //    [Test]
        //    public async Task TestNegativeMinException()
        //    {
        //        await using var context = new ApplicationDbContext(Opt);
        //        var repository = new UnitOfWork(context);
        //        OfferService offerService = new OfferService(repository);
        //        Assert.Throws<ArgumentException>(() => offerService.AdvancedSearch(minCost: -15, maxCost: 3));
        //    }

        //    [Test]
        //    public async Task TestNegativeMaxException()
        //    {
        //        await using var context = new ApplicationDbContext(Opt);
        //        var repository = new UnitOfWork(context);
        //        OfferService offerService = new OfferService(repository);
        //        Assert.Throws<ArgumentException>(() => offerService.AdvancedSearch(maxCost: -3));
        //    }

        //    [Test]
        //    public async Task TestNegativeMinAndMaxException()
        //    {
        //        await using var context = new ApplicationDbContext(Opt);
        //        var repository = new UnitOfWork(context);
        //        OfferService offerService = new OfferService(repository);
        //        Assert.Throws<ArgumentException>(() => offerService.AdvancedSearch(minCost: -4, maxCost: -3));
        //    }
    }
}