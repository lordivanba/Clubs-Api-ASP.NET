using System.Collections.Generic;
using System.Linq;
using clubs_api.Domain.Dtos;
using clubs_api.Domain.Entities;
using clubs_api.Domain.Interfaces;

namespace clubs_api.Application.Services
{
    public class ClubService  :  IClubService
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

        public Club DtoToObject(ClubFilterDto dto) 
        {
            if (string.IsNullOrEmpty(dto.Nombre) && string.IsNullOrEmpty(dto.Direccion) && string.IsNullOrEmpty(dto.Telefono))
                return null;

            var club = new Club
            {
                Id = 0,
                Nombre = dto.Nombre,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono
            };

            return club;
        }

        public bool ValidateCreate(Club club)
        {
            if (string.IsNullOrEmpty(club.Nombre))
                return false;
            if (string.IsNullOrEmpty(club.Direccion))
                return false;
            if (string.IsNullOrEmpty(club.Telefono))
                return false;
            return true;   
        }
        public bool ValidateUpdate(Club club)
        {
            if (club.Id <= 0)
                return false;
            if (string.IsNullOrEmpty(club.Nombre))
                return false;
            if (string.IsNullOrEmpty(club.Direccion))
                return false;
            if (string.IsNullOrEmpty(club.Telefono))
                return false;
            return true;
        }
    }
}