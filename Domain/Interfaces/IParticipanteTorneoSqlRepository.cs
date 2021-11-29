using System.Collections.Generic;
using System.Threading.Tasks;
using clubs_api.Domain.Entities;

namespace clubs_api.Domain.Interfaces
{
    public interface IParticipanteTorneoSqlRepository
    {
        Task<IEnumerable<ParticipanteTorneo>> GetParticipantes();
        Task<ParticipanteTorneo> GetParticipanteById(int id);
        Task<IEnumerable<ParticipanteTorneo>> GetParticipanteByTorneoId(int id);
        Task<IEnumerable<ParticipanteTorneo>> GetParticipanteByClubId(int id);
        Task<int> CreateParticipante(ParticipanteTorneo participante);
    }
}