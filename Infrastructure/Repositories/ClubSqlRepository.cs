using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clubs_api.Domain.Entities;
using clubs_api.Domain.Interfaces;
using clubs_api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace clubs_api.Infrastructure.Repositories
{
    public class ClubSqlRepository : IClubSqlRepository
    {
        private readonly clubsdbContext _context;

        public ClubSqlRepository()
        {
            _context = new clubsdbContext();
        }
        public async Task<IEnumerable<Club>> GetClubs()
        {
            var query = _context.Clubs.Select(club => club);
            return await query.ToListAsync();
        }

        public async Task<Club> GetClubById(int id)
        {
            var query = _context.Clubs.FindAsync(id);
            return await query;
        }
    }
}