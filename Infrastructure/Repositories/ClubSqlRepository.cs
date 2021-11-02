using System.Collections.Generic;
using System.Linq;
using clubs_api.Domain.Entities;
using clubs_api.Infrastructure.Data;

namespace clubs_api.Infrastructure.Repositories
{
    public class ClubSqlRepository
    {
        private readonly clubsdbContext _context;

        public ClubSqlRepository()
        {
            _context = new clubsdbContext();
        }
        public IEnumerable<Club> GetClubs()
        {        
            return _context.Clubs.Select(club => club);
        }
    }
}