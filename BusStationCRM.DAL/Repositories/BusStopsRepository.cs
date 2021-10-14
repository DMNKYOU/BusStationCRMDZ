﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusStationCRM.BLL.Models;
using BusStationCRM.DAL.Contexts;
using BusStationCRM.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusStationCRM.DAL.Repositories
{
    public class BusStopsRepository : BaseRepository<BusStop>
    {

        public BusStopsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public new Task<BusStop> GetAsync(int id) 
        {
            return _context.BusStops.AsNoTracking()
                .Include(s => s.Voyages)
                .SingleOrDefaultAsync(sr => sr.Id == id);
        }

        public new Task<List<BusStop>> GetAllAsync()
        {
            return _context.BusStops
                .Include(s => s.Voyages)
                .ToListAsync();
        }
    }
}
