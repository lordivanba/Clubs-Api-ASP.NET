using clubs_api.Domain.Dtos;
using clubs_api.Domain.Entities;
using System.Collections.Generic;

namespace clubs_api.Domain.Interfaces
{
    public interface IServicioClubService
    {
        ServicioClubDto ObjectToDto(ServicioClub servicio);
        IEnumerable<ServicioClubDto> ObjectsToDtos(IEnumerable<ServicioClub> servicios);
        ServicioClub DtoToObject(ServicioClubFilterDto dto);
        bool ValidateCreate(ServicioClub servicio);
        bool ValidateUpdate(ServicioClub servicio);
    }
}
