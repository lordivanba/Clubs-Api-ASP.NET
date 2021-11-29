using clubs_api.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace clubs_api.Domain.Interfaces
{
    public interface IServicioClubSqlRepository
    {
        Task<IEnumerable<ServicioClub>> GetServicios();
        Task<ServicioClub> GetServicioById(int id);
        Task<IEnumerable<ServicioClub>> GetServicioByClubId(int id);
        Task<IEnumerable<ServicioClub>> GetByFilter(ServicioClub servicio);
        Task<int> CreateServicio(ServicioClub servicio);
        Task<bool> UpdateServicio(int id, ServicioClub servicio);
    }
}
