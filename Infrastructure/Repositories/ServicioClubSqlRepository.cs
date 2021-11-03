using System.Collections.Generic;
using System.Linq;
using clubs_api.Domain.Entities;
using clubs_api.Infrastructure.Data;

namespace clubs_api.Infrastructure.Repositories
{
    public class ServicioClubSqlRepository
    {
        private readonly clubsdbContext _context;
        
        public ServicioClubSqlRepository()
        {
            _context = new clubsdbContext();
        }

        public IEnumerable<ServicioClub> GetServicios()
        {
            return _context.ServicioClubs.Select(ServicioClub => ServicioClub);
        }

        public ServicioClub GetServicioById(int id)
        {
            return _context.ServicioClubs.Single(servicio => servicio.Id == id);
        }

    }
}