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
    public class TorneoSqlRepository : ITorneoSqlRepository
    {
        private readonly clubsdbContext _context;

        public TorneoSqlRepository()
        {
            _context = new clubsdbContext();
        }

        public async Task<IEnumerable<Torneo>> GetTorneos()
        {
            var query = _context.Torneos.Select(torneo => torneo);
            return await query.ToListAsync();
        }
        
        public async Task<Torneo> GetTorneoById(int id)
        {
            var query = _context.Torneos.FindAsync(id);
            return await query;
        }

        public async Task<IEnumerable<Torneo>> GetByFilter(Torneo torneo)
        {
            if (torneo == null)
                return new List<Torneo>();

            var query = _context.Torneos.Select(torneo => torneo);

            if (!string.IsNullOrEmpty(torneo.Nombre))
                query = query.Where(x => x.Nombre.Contains(torneo.Nombre));

            if (!string.IsNullOrEmpty(torneo.Disciplina))
                query = query.Where(x => x.Disciplina.Contains(torneo.Disciplina));

            if (torneo.NumeroEquipos > 1)
                query = query.Where(x => x.NumeroEquipos == torneo.NumeroEquipos);

            if (torneo.DisponibilidadLugares > 1)
                query = query.Where(x => x.DisponibilidadLugares == torneo.DisponibilidadLugares);

            if (torneo.CostoInscripcion > 0)
                query = query.Where(x => x.CostoInscripcion == torneo.CostoInscripcion);

            if (torneo.NumeroRondas > 1)
                query = query.Where(x => x.NumeroRondas == torneo.NumeroRondas);

            if (!string.IsNullOrEmpty(torneo.TipoTorneo))
                query = query.Where(x => x.TipoTorneo.Contains(torneo.TipoTorneo));

            if (!string.IsNullOrEmpty(torneo.Resultado))
                query = query.Where(x => x.Resultado.Contains(torneo.Resultado));

            if (!string.IsNullOrEmpty(torneo.Estatus))
                query = query.Where(x => x.Estatus.Contains(torneo.Estatus));

            return await query.ToListAsync();
        }

        public async Task<int> CreateTorneo(Torneo torneo)
        {
            if(torneo == null)
                throw new ArgumentNullException("No se pudo registrar el torneo a falta de informacion");
            try {
                torneo.FechaRegistro = DateTime.Now;
                var entity = torneo;
                await _context.AddAsync(entity);
                var rows = await _context.SaveChangesAsync();

                if(rows <= 0)
                    throw new Exception("Ocurri� un fallo al intentar registrar el torneo, verifica la informaci�n ingresada");
                return entity.Id;
            }
            catch(Exception e)
            {
                throw new Exception("No se pudo registrar el torneo a falta de informacion");
            }
        }


        public async Task<bool> UpdateTorneo(int id, Torneo torneo)
        {
            if(id <= 0 || torneo == null)
                throw new ArgumentNullException("La actualizacion no se pudo realizar a falta de informacion");
            var entity = await GetTorneoById(id);

            entity.Nombre = torneo.Nombre;
            entity.Disciplina = torneo.Disciplina;
            entity.NumeroEquipos = torneo.NumeroEquipos;
            entity.DisponibilidadLugares = torneo.DisponibilidadLugares;
            entity.CostoInscripcion = torneo.CostoInscripcion;
            entity.Bases = torneo.Bases;
            entity.NumeroRondas = torneo.NumeroRondas;
            entity.TipoTorneo = torneo.TipoTorneo;
            entity.Resultado = torneo.Resultado;
            entity.Estatus = torneo.Estatus;

            _context.Update(entity);

            var rows = await _context.SaveChangesAsync();

            return rows > 0;
        }
    }
}