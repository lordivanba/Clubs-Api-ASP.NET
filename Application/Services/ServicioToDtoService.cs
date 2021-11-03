using System.Collections.Generic;
using System.Linq;
using clubs_api.Domain.Dtos;
using clubs_api.Domain.Entities;

namespace clubs_api.Application.Services
{
    public class ServicioToDtoService
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
    }
}