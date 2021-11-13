using clubs_api.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace clubs_api.Domain.Interfaces
{
    //Agregar servicio en startup
    public interface IClubSqlRepository
    {
        Task<IEnumerable<Club>> GetClubs();

        Task<Club> GetClubById(int id);
    }
}
