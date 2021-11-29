using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clubs_api.Domain.Entities;
using clubs_api.Domain.Interfaces;
using clubs_api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace clubs_api.Infrastructure.Repositories
{
    public class ParticipanteTorneoSqlRepository : IParticipanteTorneoSqlRepository
    {
        private readonly clubsdbContext _context;
        public ParticipanteTorneoSqlRepository(clubsdbContext context)
        {
            _context = new clubsdbContext();
        }
        
        public async Task<IEnumerable<ParticipanteTorneo>> GetParticipantes()
        {
            var query = _context.ParticipanteTorneos.Include(x => x.Torneo).Include(x => x.Club);
            return await query.ToListAsync();
        }

        public async Task<ParticipanteTorneo> GetParticipanteById(int id)
        {
            var query = _context.ParticipanteTorneos.Include(x => x.Club).Include(x => x.Torneo).FirstOrDefaultAsync(x => x.Id == id);
            return await query;
        }   

        public async Task<IEnumerable<ParticipanteTorneo>> GetParticipanteByTorneoId(int id)
        {
            var query = _context.ParticipanteTorneos.Include(x => x.Torneo).Include(x => x.Club).Where(x => x.TorneoId == id).ToListAsync();
            return await query;
        }

        public async Task<IEnumerable<ParticipanteTorneo>> GetParticipanteByClubId(int id)
        {
            var query = _context.ParticipanteTorneos.Include(x => x.Torneo).Include(x => x.Club).Where(x => x.ClubId == id).ToListAsync();
            return await query;
        }

        public async Task<int> CreateParticipante(ParticipanteTorneo participante)
        {
            if (participante == null)
                throw new ArgumentNullException("No se pudo registrar el participante a falta de informacion");
            try
            {
                var entity = participante;
                await _context.AddAsync(entity);
                var rows = await _context.SaveChangesAsync();

                if (rows <= 0)
                    throw new Exception("Ocurrió un fallo al intentar registrar el participante, verifica la información ingresada");
                return entity.Id;
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo registrar el participante a falta de informacion");
            }
        }

    }
}