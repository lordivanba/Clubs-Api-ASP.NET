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
    public class ClubSqlRepository : IClubSqlRepository
    {
        private readonly clubsdbContext _context;

        public ClubSqlRepository()
        {
            _context = new clubsdbContext();
        }
        public async Task<IEnumerable<Club>> GetClubs()
        {
            var query = _context.Clubs.Select(club => club);
            return await query.ToListAsync();
        }

        public async Task<Club> GetClubById(int id)
        {
            var query = _context.Clubs.FindAsync(id);
            return await query;
        }

        public async Task<IEnumerable<Club>> GetByFilter(Club club)
        {
            if (club == null)
                return new List<Club>();
            var query = _context.Clubs.Select(club => club);

            if (!string.IsNullOrEmpty(club.Nombre))
                query = query.Where(x => x.Nombre.Contains(club.Nombre));

            if (!string.IsNullOrEmpty(club.Direccion))
                query = query.Where(x => x.Direccion.Contains(club.Direccion));

            if (!string.IsNullOrEmpty(club.Telefono))
                query = query.Where(x => x.Telefono.Contains(club.Telefono));

            return await query.ToListAsync();
        }

        public async Task<int> CreateClub(Club club)
        {
            if (club == null)
                throw new ArgumentNullException("No se pudo registrar el club a falta de informacion");
            try{
                club.FechaRegistro = DateTime.Now;
                var entity = club;
                await _context.AddAsync(entity);
                var rows = await _context.SaveChangesAsync();

                if (rows <= 0)
                    throw new Exception("Ocurri� un fallo al intentar registrar el club, verifica la informaci�n ingresada");
                return entity.Id;
            }
            catch (DbUpdateException  exEf)
            {
                throw new Exception("No se pudo registrar el club a falta de informacion");
            }
        }

        public async Task<bool> UpdateClub(int id, Club club)
        {
            if (id <= 0 || club == null)
                throw new ArgumentNullException("La actualizacion no se pudo realizar a falta de informacion");
            var entity = await GetClubById(id);

            entity.Nombre = club.Nombre;
            entity.Direccion = club.Direccion;
            entity.Telefono = club.Telefono;

            _context.Update(entity);

            var rows = await _context.SaveChangesAsync();

            return rows > 0;
        }
    }
}