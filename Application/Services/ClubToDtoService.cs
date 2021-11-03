using System.Collections.Generic;
using System.Linq;
using clubs_api.Domain.Dtos;
using clubs_api.Domain.Entities;

namespace clubs_api.Application.Services
{
    public class ClubToDtoService
    {
        public ClubDto ObjectToDto(Club club)
        {
            return new ClubDto(
                Id: club.Id,
                Nombre: club.Nombre,
                Direccion: club.Direccion,
                Telefono: club.Telefono,
                FechaRegistro: club.FechaRegistro
            );
        }

        public IEnumerable<ClubDto> ObjectsToDtos(IEnumerable<Club> clubs)
        {
            return clubs.Select(club => new ClubDto(
                Id: club.Id,
                Nombre: club.Nombre,
                Direccion: club.Direccion,
                Telefono: club.Telefono,
                FechaRegistro: club.FechaRegistro
            ));
        }
    }
}