using clubs_api.Domain.Dtos;
using clubs_api.Domain.Entities;
using System.Collections.Generic;

namespace clubs_api.Domain.Interfaces
{
    public interface ITorneoService
    {
        TorneoDto ObjectToDto(Torneo torneo);
        IEnumerable<TorneoDto> ObjectsToDtos(IEnumerable<Torneo> torneos);
        Torneo DtoToObject(TorneoFilterDto dto);
        bool ValidateCreate(Torneo torneo);
        bool ValidateUpdate(Torneo torneo);
    }
}
