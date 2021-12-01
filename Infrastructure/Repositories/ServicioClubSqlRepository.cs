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
    public class ServicioClubSqlRepository : IServicioClubSqlRepository
    {
        private readonly clubsdbContext _context;
        
        public ServicioClubSqlRepository()
        {
            _context = new clubsdbContext();
        }

        public async Task<IEnumerable<ServicioClub>> GetServicios()
        {
            // var query = _context.ServicioClubs.Select(ServicioClub => ServicioClub);
            var query = _context.ServicioClubs.Include(x => x.Club);

            return await query.ToListAsync();
        }

        public async Task<ServicioClub> GetServicioById(int id)
        {
            var query = _context.ServicioClubs.Include(x => x.Club).FirstOrDefaultAsync(x => x.Id == id);

            return await query;
        }

        public async Task<IEnumerable<ServicioClub>> GetServicioByClubId(int id)
        {
            var query = _context.ServicioClubs.Include(x => x.Club).Where(x => x.ClubId == id).ToListAsync();

            return await query;
        }

        public async Task<IEnumerable<ServicioClub>> GetByFilter(ServicioClub servicio)
        {
            if (servicio == null)
                return new List<ServicioClub>();

            var query = _context.ServicioClubs.Include(servicio => servicio.Club).Select(x => x);

            if (!string.IsNullOrEmpty(servicio.Disciplina))
                query = query.Where(x => x.Disciplina.Contains(servicio.Disciplina));

            if (!string.IsNullOrEmpty(servicio.Horario))
                query = query.Where(x => x.Horario.Contains(servicio.Horario));

            if (servicio.PersonasPermitidas !> 1)
                query = query.Where(x => x.PersonasPermitidas == servicio.PersonasPermitidas);

            if (servicio.RequiereEquipoEspecial! < 0 && servicio.RequiereEquipoEspecial! > 1)
                query = query.Where(x => x.RequiereEquipoEspecial == servicio.RequiereEquipoEspecial);

            if (servicio.CapacidadesDiferentes! < 0 && servicio.CapacidadesDiferentes! > 1)
                query = query.Where(x => x.CapacidadesDiferentes == servicio.CapacidadesDiferentes);

            return await query.ToListAsync();
        }

        public async Task<int> CreateServicio(ServicioClub servicio)
        {
            if (servicio == null)
                throw new ArgumentNullException("No se pudo registrar el servicio a falta de informacion");
            try
            {
                servicio.FechaRegistro = DateTime.Now;
                var entity = servicio;
                await _context.AddAsync(entity);
                var rows = await _context.SaveChangesAsync();

                if (rows <= 0)
                    throw new Exception("Ocurri� un fallo al intentar registrar el servicio, verifica la informaci�n ingresada");
                return entity.Id;
            }
            catch (Exception ex) {
                throw new Exception("No se pudo registrar el servicio a falta de informacion");
            }
        }

        public async Task<bool> UpdateServicio(int id, ServicioClub servicio)
        {
            if (id <= 0 || servicio == null)
                throw new ArgumentNullException("La actualizacion no se pudo realizar a falta de informacion");
            var entity = await GetServicioById(id);

            entity.Disciplina = servicio.Disciplina;
            entity.Horario = servicio.Horario;
            entity.PersonasPermitidas = servicio.PersonasPermitidas;
            entity.RequiereEquipoEspecial = servicio.RequiereEquipoEspecial;
            entity.CapacidadesDiferentes = servicio.CapacidadesDiferentes;

            _context.Update(entity);

            var rows = await _context.SaveChangesAsync();

            return rows > 0;
        }
    }
}