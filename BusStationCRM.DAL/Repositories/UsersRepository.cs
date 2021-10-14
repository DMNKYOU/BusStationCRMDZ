using System;
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
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<User> GetAsync(string id)
        {
            return _context.Users.AsNoTracking()
                .Include(s => s.Tickets)
                .Include(c => c.Orders)
                .SingleOrDefaultAsync(sr => sr.Id == id);
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _context.Users.FindAsync(id);

            if (entity == null)
            {
                throw new ArgumentException($"Error! User not deleted, because not found!");
            }

            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public Task<List<User>> GetAllAsync()
        {
            return _context.Users
                .Include(s => s.Tickets)
                .Include(c => c.Orders)
                .ToListAsync();
        }
        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return _context.Users.AsEnumerable().Where(predicate).ToList();
        }

        public async Task CreateAsync(User item)
        {
            await _context.Users.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}