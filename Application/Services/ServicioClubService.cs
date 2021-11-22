using System.Collections.Generic;
using System.Linq;
using clubs_api.Domain.Dtos;
using clubs_api.Domain.Entities;
using clubs_api.Domain.Interfaces;

namespace clubs_api.Application.Services
{
    public class ServicioClubService : IServicioClubService
    {
        public ServicioClubDto ObjectToDto(ServicioClub servicio){
            return new ServicioClubDto(
                Id: servicio.Id,
                Disciplina: servicio.Disciplina,
                Horario: servicio.Horario,
                PersonasPermitidas: servicio.PersonasPermitidas,
                RequiereEquipoEspecial: servicio.RequiereEquipoEspecial,
                CapacidadesDiferentes: servicio.CapacidadesDiferentes,
                FechaRegistro: servicio.FechaRegistro
            );
        }
        
        public IEnumerable<ServicioClubDto> ObjectsToDtos(IEnumerable<ServicioClub> servicios){
            return servicios.Select(servicio => new ServicioClubDto(
                Id: servicio.Id,
                Disciplina: servicio.Disciplina,
                Horario: servicio.Horario,
                PersonasPermitidas: servicio.PersonasPermitidas,
                RequiereEquipoEspecial: servicio.RequiereEquipoEspecial,
                CapacidadesDiferentes: servicio.CapacidadesDiferentes,
                FechaRegistro: servicio.FechaRegistro
            ));
        }

        public ServicioClub DtoToObject(ServicioClubFilterDto dto)
        {
            //if (string.IsNullOrEmpty(dto.Disciplina) && string.IsNullOrEmpty(dto.Horario))
               // return null;

            var servicio = new ServicioClub
            {
                Id = 0,
                Disciplina = dto.Disciplina,
                Horario = dto.Horario,
                PersonasPermitidas =  dto.PersonasPermitidas,
                RequiereEquipoEspecial = dto.RequiereEquipoEspecial,
                CapacidadesDiferentes = dto.CapacidadesDiferentes
            };

            return servicio;
        }

        public bool ValidateCreate(ServicioClub servicio)
        {
            if (string.IsNullOrEmpty(servicio.Disciplina))
                return false;
            if (string.IsNullOrEmpty(servicio.Horario))
                return false;
            if (servicio.PersonasPermitidas < 1)
                return false;
            return true;
        }

        public bool ValidateUpdate(ServicioClub servicio)
        {
            if (servicio.Id <= 0)
                return false;
            if (string.IsNullOrEmpty(servicio.Disciplina))
                return false;
            if (string.IsNullOrEmpty(servicio.Horario))
                return false;
            if (servicio.PersonasPermitidas < 1)
                return false;
            return true;
        }
    }
}