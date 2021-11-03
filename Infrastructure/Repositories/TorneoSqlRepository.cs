using System.Collections.Generic;
using System.Linq;
using clubs_api.Domain.Entities;
using clubs_api.Infrastructure.Data;

namespace clubs_api.Infrastructure.Repositories
{
    public class TorneoSqlRepository
    {
        private readonly clubsdbContext _context;

        public TorneoSqlRepository()
        {
            _context = new clubsdbContext();
        }

        public IEnumerable<Torneo> GetTorneos()
        {
            return _context.Torneos.Select(torneo => torneo);
        }

        public Torneo GetTorneoById(int id)
        {
            return _context.Torneos.Single(torneo => torneo.Id == id);
        }
    }
}