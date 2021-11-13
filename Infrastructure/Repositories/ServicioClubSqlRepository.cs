using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clubs_api.Domain.Entities;
using clubs_api.Domain.Interfaces;
using clubs_api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace clubs_api.Infrastructure.Repositories
{
    public class ServicioClubSqlRepository : IServicioClubSqlRepository
    {
        private readonly clubsdbContext _context;
        
        public ServicioClubSqlRepository()
        {
            _context = new clubsdbContext();
        }

        public async Task<IEnumerable<ServicioClub>> GetServicios()
        {
            var query = _context.ServicioClubs.Select(ServicioClub => ServicioClub);
            return await query.ToListAsync();
        }

        public async Task<ServicioClub> GetServicioById(int id)
        {
            var query = _context.ServicioClubs.FindAsync(id);
            return await query;
        }

    }
}