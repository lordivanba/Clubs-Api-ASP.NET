using clubs_api.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace clubs_api.Domain.Interfaces
{
    //Agregar servicio en startup
    public interface ITorneoSqlRepository
    {
        Task<IEnumerable<Torneo>> GetTorneos();
        Task<Torneo> GetTorneoById(int id);
    }
}
