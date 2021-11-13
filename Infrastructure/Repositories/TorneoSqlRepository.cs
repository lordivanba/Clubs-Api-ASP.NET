using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clubs_api.Domain.Entities;
using clubs_api.Domain.Interfaces;
using clubs_api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace clubs_api.Infrastructure.Repositories
{
    public class TorneoSqlRepository : ITorneoSqlRepository
    {
        private readonly clubsdbContext _context;

        public TorneoSqlRepository()
        {
            _context = new clubsdbContext();
        }

        public async Task<IEnumerable<Torneo>> GetTorneos()
        {
            var query = _context.Torneos.Select(torneo => torneo);
            return await query.ToListAsync();
        }

        public async Task<Torneo> GetTorneoById(int id)
        {
            var query = _context.Torneos.FindAsync(id);
            return await query;
        }
    }
}